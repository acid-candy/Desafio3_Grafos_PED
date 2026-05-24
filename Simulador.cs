using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RutaCorta
{
    // Formulario del simulador. Aquí se conecta la interfaz con el grafo y las animaciones.
    public partial class Simulador : Form
    {
        private CGrafo grafo;

        private Image imagenMapaSanVicente;

        private Image imagenTorogoz;

        private Image imagenMaquilishuat;

        // Este rectángulo define dónde se dibuja el mapa dentro de la pizarra.
        private readonly Rectangle rectMapa = new Rectangle(152, 12, 545, 655);

        private bool animando;

        private string rutaCompletaActual = string.Empty;

        private string recorridoCompletoActual = string.Empty;

        private string detalleCompletoActual = string.Empty;

        private float zoomMapa = 1.0f;

        private PointF desplazamientoMapa = new PointF(0, 0);

        private bool arrastrandoMapa;

        private Point ultimoPuntoArrastre;

        private CVertice municipioHover;

        private const float ZoomMinimo = 0.55f;

        private const float ZoomMaximo = 2.20f;

        private const float ZoomInicial = 0.82f;

        // Constructor del formulario: carga recursos, arma el grafo y deja lista la interfaz.
        public Simulador()
        {
            InitializeComponent();

            grafo = new CGrafo();

            CargarImagenes();

            picTorogoz.Visible = false;
            picMaquilishuat.Visible = false;

            CrearGrafoSanVicente();

            ActualizarCombos();

            ActualizarResumenGrafo();

            ConfigurarInteraccionMapa();
        }

        // Busca archivos de la carpeta Recursos, tanto desde Visual Studio como desde bin\\Debug.
        private string ObtenerRutaRecurso(string archivo)
        {
            string rutaEnSalida = Path.Combine(Application.StartupPath, "Recursos", archivo);

            string rutaEnProyecto = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "Recursos", archivo));

            if (File.Exists(rutaEnSalida))
                return rutaEnSalida;

            if (File.Exists(rutaEnProyecto))
                return rutaEnProyecto;

            return string.Empty;
        }

        // Carga las imágenes usadas en el diseño.
        private void CargarImagenes()
        {
            AsignarImagen(picBandera, "Bandera.png");

            AsignarImagen(picMapaMini, "San_Vicente_Banner.png");

            imagenTorogoz = CargarImagenRecurso("torogoz.png");

            imagenMaquilishuat = CargarImagenRecurso("maquilishuat.png");

            string rutaMapa = ObtenerRutaRecurso("San_vicente.png");
            if (!string.IsNullOrEmpty(rutaMapa))
                imagenMapaSanVicente = Image.FromFile(rutaMapa);
        }

        // Prepara el panel del mapa para zoom, arrastre y botones de control.
        private void ConfigurarInteraccionMapa()
        {
            Pizarra.TabStop = true;

            Pizarra.MouseLeave += Pizarra_MouseLeave;

            typeof(Panel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(Pizarra, true, null);

            CrearBotonZoom("+", new Point(16, 12), delegate { AplicarZoom(1.25f, new Point(Pizarra.Width / 2, Pizarra.Height / 2)); });
            CrearBotonZoom("-", new Point(58, 12), delegate { AplicarZoom(0.80f, new Point(Pizarra.Width / 2, Pizarra.Height / 2)); });
            CrearBotonZoom("Centrar", new Point(100, 12), delegate { ReiniciarVistaMapa(); });

            ReiniciarVistaMapa();
        }

        // Centra el mapa y calcula un zoom inicial que aproveche el espacio vertical.
        private void ReiniciarVistaMapa()
        {
            float margenHorizontal = 22f;
            float margenSuperior = 12f;
            float margenInferior = 20f;

            float zoomX = (Pizarra.Width - (margenHorizontal * 2f)) / rectMapa.Width;
            float zoomY = (Pizarra.Height - margenSuperior - margenInferior) / rectMapa.Height;

            zoomMapa = Math.Max(ZoomMinimo, Math.Min(ZoomMaximo, Math.Min(zoomX, zoomY)));

            float xVisible = (Pizarra.Width - (rectMapa.Width * zoomMapa)) / 2f;
            float yVisible = margenSuperior;

            desplazamientoMapa = new PointF(
                xVisible - (rectMapa.X * zoomMapa),
                yVisible - (rectMapa.Y * zoomMapa));

            Pizarra.Refresh();
        }

        // Crea los botones pequeños de zoom que van encima del mapa.
        private void CrearBotonZoom(string texto, Point posicion, EventHandler accion)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Location = posicion;
            boton.Size = texto == "Centrar" ? new Size(82, 29) : new Size(36, 29);
            boton.FlatStyle = FlatStyle.Flat;
            boton.BackColor = texto == "+" ? Color.FromArgb(0, 87, 183) : Color.White;
            boton.ForeColor = texto == "+" ? Color.White : Color.FromArgb(0, 87, 183);
            boton.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            boton.TabStop = false;
            boton.Cursor = Cursors.Hand;
            boton.FlatAppearance.BorderColor = Color.FromArgb(0, 87, 183);
            boton.Click += accion;

            Pizarra.Controls.Add(boton);
            boton.BringToFront();
        }

        // Aplica zoom manteniendo como referencia el punto indicado.
        private void AplicarZoom(float factor, Point puntoPantalla)
        {
            float zoomAnterior = zoomMapa;

            float nuevoZoom = Math.Max(ZoomMinimo, Math.Min(ZoomMaximo, zoomMapa * factor));

            if (Math.Abs(nuevoZoom - zoomMapa) < 0.001f)
                return;

            float mundoX = (puntoPantalla.X - desplazamientoMapa.X) / zoomAnterior;
            float mundoY = (puntoPantalla.Y - desplazamientoMapa.Y) / zoomAnterior;

            zoomMapa = nuevoZoom;

            desplazamientoMapa = new PointF(
                puntoPantalla.X - (mundoX * zoomMapa),
                puntoPantalla.Y - (mundoY * zoomMapa));

            Pizarra.Refresh();
        }

        // Carga una imagen y la clona para no dejar bloqueado el archivo.
        private Image CargarImagenRecurso(string archivo)
        {
            string ruta = ObtenerRutaRecurso(archivo);

            if (string.IsNullOrEmpty(ruta))
                return null;

            using (Image original = Image.FromFile(ruta))
            {
                return new Bitmap(original);
            }
        }

        // Coloca una imagen en un PictureBox si el archivo existe.
        private void AsignarImagen(PictureBox pictureBox, string archivo)
        {
            string ruta = ObtenerRutaRecurso(archivo);

            if (!string.IsNullOrEmpty(ruta))
            {
                using (Image original = Image.FromFile(ruta))
                {
                    pictureBox.Image = new Bitmap(original);
                }
            }
        }

        // Aquí se cargan los municipios y caminos usados en el simulador.
        private void CrearGrafoSanVicente()
        {
            grafo.Limpiar();

            // Los puntos X/Y fueron ajustados manualmente sobre el mapa.
            AgregarMunicipio("San Lorenzo", "SL", 265, 142);
            AgregarMunicipio("San Sebastián", "SS", 271, 72);
            AgregarMunicipio("Santa Clara", "SC", 462, 100);
            AgregarMunicipio("Santo Domingo", "SD", 219, 124);
            AgregarMunicipio("San Ildefonso", "SI", 596, 150);
            AgregarMunicipio("San Esteban Catarina", "SEC", 358, 86);
            AgregarMunicipio("Apastepeque", "AP", 426, 181);
            AgregarMunicipio("San Cayetano Istepeque", "SCI", 267, 186);
            AgregarMunicipio("Guadalupe", "GU", 201, 228);
            AgregarMunicipio("Verapaz", "VE", 196, 179);
            AgregarMunicipio("Tepetitán", "TE", 238, 184);
            AgregarMunicipio("San Vicente", "SV", 392, 271);
            AgregarMunicipio("Tecoluca", "TC", 330, 446);

            // Cada camino se guarda con su distancia en km.
            grafo.AgregarCamino("San Lorenzo", "San Sebastián", 7);
            // Cada camino se guarda con su distancia en km.
            grafo.AgregarCamino("San Lorenzo", "San Ildefonso", 10);
            grafo.AgregarCamino("San Sebastián", "San Esteban Catarina", 8);
            grafo.AgregarCamino("San Esteban Catarina", "San Ildefonso", 10);
            grafo.AgregarCamino("San Esteban Catarina", "Apastepeque", 9);
            grafo.AgregarCamino("San Ildefonso", "Verapaz", 14);
            grafo.AgregarCamino("Verapaz", "Tepetitán", 5);
            grafo.AgregarCamino("Tepetitán", "Guadalupe", 4);
            grafo.AgregarCamino("Guadalupe", "San Cayetano Istepeque", 5);
            grafo.AgregarCamino("San Cayetano Istepeque", "Apastepeque", 6);
            grafo.AgregarCamino("Apastepeque", "Santa Clara", 12);
            grafo.AgregarCamino("Santa Clara", "Santo Domingo", 9);
            grafo.AgregarCamino("Apastepeque", "Santo Domingo", 15);
            grafo.AgregarCamino("Apastepeque", "San Vicente", 13);
            grafo.AgregarCamino("San Cayetano Istepeque", "San Vicente", 7);
            grafo.AgregarCamino("Guadalupe", "San Vicente", 6);
            grafo.AgregarCamino("Tepetitán", "San Vicente", 10);
            grafo.AgregarCamino("San Ildefonso", "San Vicente", 22);
            grafo.AgregarCamino("Santo Domingo", "San Vicente", 17);
            grafo.AgregarCamino("San Vicente", "Tecoluca", 28);

            grafo.ReestablecerGrafo();
        }

        // Agrega un municipio con su posición sobre el mapa.
        private void AgregarMunicipio(string nombre, string sigla, int x, int y)
        {
            CVertice municipio = new CVertice(nombre, sigla);

            municipio.Posicion = new Point(x, y);

            grafo.AgregarVertice(municipio);
        }

        // Rellena los ComboBox con los municipios del grafo.
        private void ActualizarCombos()
        {
            CBOrigenDijk.Items.Clear();
            CBDestinoDijk.Items.Clear();
            CBNodoPartida.Items.Clear();

            foreach (CVertice nodo in grafo.nodos)
            {
                CBOrigenDijk.Items.Add(nodo.Valor);
                CBDestinoDijk.Items.Add(nodo.Valor);
                CBNodoPartida.Items.Add(nodo.Valor);
            }

            CBOrigenDijk.SelectedIndex = -1;
            CBDestinoDijk.SelectedIndex = -1;
            CBNodoPartida.SelectedIndex = -1;
        }

        // Muestra cuántos municipios y caminos hay cargados.
        private void ActualizarResumenGrafo()
        {
            int totalArcos = 0;
            foreach (CVertice nodo in grafo.nodos)
                totalArcos += nodo.ListaAdyacencia.Count;

            lblResumenGrafo.Text = "Municipios: " + grafo.TotalVertices + "   Caminos: " + (totalArcos / 2);
        }

        // Dibuja los bordes redondeados azules de los paneles.
        private void PanelBordeAzul_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            if (panel == null)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle rect = new Rectangle(1, 1, panel.Width - 3, panel.Height - 3);

            using (GraphicsPath path = CrearRectanguloRedondeado(rect, 14))
            using (Pen pen = new Pen(Color.FromArgb(0, 80, 190), 2))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        // Función auxiliar para dibujar rectángulos con esquinas redondeadas.
        private GraphicsPath CrearRectanguloRedondeado(Rectangle rect, int radio)
        {
            int diametro = radio * 2;

            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, diametro, diametro, 180, 90);

            path.AddArc(rect.Right - diametro, rect.Y, diametro, diametro, 270, 90);

            path.AddArc(rect.Right - diametro, rect.Bottom - diametro, diametro, diametro, 0, 90);

            path.AddArc(rect.X, rect.Bottom - diametro, diametro, diametro, 90, 90);

            path.CloseFigure();

            return path;
        }

        // Dibuja las decoraciones dentro del panel sin tapar controles.
        private void DibujarDecoracionesPizarra(Graphics g)
        {
            if (imagenTorogoz != null)
            {
                Rectangle rectTorogoz = new Rectangle(18, 52, 74, 51);
                g.DrawImage(imagenTorogoz, rectTorogoz);
            }

            if (imagenMaquilishuat != null)
            {
                Rectangle rectMaquilishuat = new Rectangle(Pizarra.Width - 104, Pizarra.Height - 92, 82, 72);
                g.DrawImage(imagenMaquilishuat, rectMaquilishuat);
            }
        }

        // Muestra el nombre del municipio cuando el mouse pasa por encima del nodo.
        private void DibujarEtiquetaHover(Graphics g)
        {
            if (municipioHover == null)
                return;

            float xPantalla = (municipioHover.Posicion.X * zoomMapa) + desplazamientoMapa.X;
            float yPantalla = (municipioHover.Posicion.Y * zoomMapa) + desplazamientoMapa.Y;

            using (Font fuente = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (StringFormat formato = new StringFormat())
            {
                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                SizeF medida = g.MeasureString(municipioHover.Valor, fuente);
                float ancho = medida.Width + 20f;
                float alto = medida.Height + 12f;

                float x = xPantalla - (ancho / 2f);
                float y = yPantalla + 18f;
                if (y + alto > Pizarra.Height - 28)
                    y = yPantalla - alto - 22f;

                if (x < 12) x = 12;
                if (x + ancho > Pizarra.Width - 12) x = Pizarra.Width - ancho - 12;

                RectangleF rect = new RectangleF(x, y, ancho, alto);
                RectangleF sombra = new RectangleF(x + 2, y + 3, ancho, alto);

                using (GraphicsPath rutaSombra = CrearRectanguloRedondeado(Rectangle.Round(sombra), 10))
                using (SolidBrush brochaSombra = new SolidBrush(Color.FromArgb(60, Color.Black)))
                {
                    g.FillPath(brochaSombra, rutaSombra);
                }

                using (GraphicsPath ruta = CrearRectanguloRedondeado(Rectangle.Round(rect), 10))
                using (SolidBrush fondo = new SolidBrush(Color.FromArgb(245, 255, 255, 255)))
                using (Pen borde = new Pen(Color.FromArgb(0, 87, 183), 1.6f))
                {
                    g.FillPath(fondo, ruta);
                    g.DrawPath(borde, ruta);
                }

                PointF punta = new PointF(xPantalla, yPantalla + (y > yPantalla ? 9f : -9f));
                PointF a = new PointF(xPantalla - 8f, y > yPantalla ? y : y + alto);
                PointF b = new PointF(xPantalla + 8f, y > yPantalla ? y : y + alto);
                using (SolidBrush fondoTriangulo = new SolidBrush(Color.FromArgb(245, 255, 255, 255)))
                using (Pen bordeTriangulo = new Pen(Color.FromArgb(0, 87, 183), 1.2f))
                {
                    PointF[] tri = new[] { a, punta, b };
                    g.FillPolygon(fondoTriangulo, tri);
                    g.DrawPolygon(bordeTriangulo, tri);
                }

                using (SolidBrush texto = new SolidBrush(Color.FromArgb(20, 50, 110)))
                {
                    g.DrawString(municipioHover.Valor, fuente, texto, rect, formato);
                }
            }
        }

        // Redibuja el mapa, los caminos, los nodos y las decoraciones.
        private void Pizarra_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            e.Graphics.Clear(Color.White);

            Rectangle rectBorde = new Rectangle(1, 1, Pizarra.Width - 3, Pizarra.Height - 3);
            using (GraphicsPath borde = CrearRectanguloRedondeado(rectBorde, 18))
            using (Pen pen = new Pen(Color.FromArgb(0, 80, 190), 4))
            {
                e.Graphics.DrawPath(pen, borde);
            }

            GraphicsState estadoOriginal = e.Graphics.Save();
            Rectangle rectRecorte = new Rectangle(6, 6, Pizarra.Width - 12, Pizarra.Height - 12);
            e.Graphics.SetClip(rectRecorte);

            e.Graphics.TranslateTransform(desplazamientoMapa.X, desplazamientoMapa.Y);
            e.Graphics.ScaleTransform(zoomMapa, zoomMapa);

            if (imagenMapaSanVicente != null)
                e.Graphics.DrawImage(imagenMapaSanVicente, rectMapa);

            grafo.DibujarGrafo(e.Graphics);

            e.Graphics.Restore(estadoOriginal);

            DibujarDecoracionesPizarra(e.Graphics);

            DibujarEtiquetaHover(e.Graphics);

            using (Font fuente = new Font("Segoe UI", 8, FontStyle.Bold))
            using (SolidBrush fondo = new SolidBrush(Color.FromArgb(220, Color.White)))
            using (SolidBrush texto = new SolidBrush(Color.FromArgb(0, 60, 150)))
            {
                string ayuda = "Zoom: + / - o rueda | Arrastrar: mover | Centrar: vista inicial";
                SizeF medida = e.Graphics.MeasureString(ayuda, fuente);
                RectangleF rectAyuda = new RectangleF(14, Pizarra.Height - medida.Height - 18, medida.Width + 12, medida.Height + 6);
                e.Graphics.FillRectangle(fondo, rectAyuda);
                e.Graphics.DrawString(ayuda, fuente, texto, rectAyuda.X + 6, rectAyuda.Y + 3);
            }
        }

        // Convierte coordenadas de pantalla a coordenadas reales del grafo.
        private Point ConvertirAPuntoLogico(Point puntoPantalla)
        {
            int x = (int)Math.Round((puntoPantalla.X - desplazamientoMapa.X) / zoomMapa);
            int y = (int)Math.Round((puntoPantalla.Y - desplazamientoMapa.Y) / zoomMapa);
            return new Point(x, y);
        }

        // Revisa si el mouse está encima de un municipio.
        private void ActualizarTooltipMunicipio(Point puntoPantalla)
        {
            Point puntoLogico = ConvertirAPuntoLogico(puntoPantalla);

            CVertice municipioAnterior = municipioHover;
            municipioHover = null;

            foreach (CVertice nodo in grafo.nodos)
            {
                if (nodo.DetectarPunto(puntoLogico))
                {
                    municipioHover = nodo;

                    if (!arrastrandoMapa)
                        Pizarra.Cursor = Cursors.Hand;

                    break;
                }
            }

            if (municipioHover == null && !arrastrandoMapa)
                Pizarra.Cursor = Cursors.Default;

            if (municipioAnterior != municipioHover)
                Pizarra.Refresh();
        }

        // Al salir del mapa se oculta la etiqueta del municipio.
        private void Pizarra_MouseLeave(object sender, EventArgs e)
        {
            if (municipioHover != null)
            {
                municipioHover = null;
                Pizarra.Refresh();
            }

            if (!arrastrandoMapa)
                Pizarra.Cursor = Cursors.Default;
        }

        // Da foco al panel para que funcione la rueda del mouse.
        private void Pizarra_MouseEnter(object sender, EventArgs e)
        {
            Pizarra.Focus();
        }

        // Empieza el arrastre del mapa.
        private void Pizarra_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastrandoMapa = true;
                ultimoPuntoArrastre = e.Location;
                Pizarra.Cursor = Cursors.Hand;
                Pizarra.Focus();
            }
        }

        // Si se arrastra, mueve el mapa; si no, revisa el nodo bajo el mouse.
        private void Pizarra_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrandoMapa)
            {
                desplazamientoMapa = new PointF(
                    desplazamientoMapa.X + (e.X - ultimoPuntoArrastre.X),
                    desplazamientoMapa.Y + (e.Y - ultimoPuntoArrastre.Y));

                ultimoPuntoArrastre = e.Location;
                Pizarra.Refresh();
                return;
            }

            ActualizarTooltipMunicipio(e.Location);
        }

        // Termina el arrastre del mapa.
        private void Pizarra_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrandoMapa = false;
            Pizarra.Cursor = Cursors.Default;

            ActualizarTooltipMunicipio(e.Location);
        }

        // Zoom con la rueda del mouse.
        private void Pizarra_MouseWheel(object sender, MouseEventArgs e)
        {
            float factor = e.Delta > 0 ? 1.12f : 0.89f;

            AplicarZoom(factor, e.Location);
        }

        // Botón de recorrido en anchura.
        private async void BtnAnch_Click(object sender, EventArgs e)
        {
            if (animando)
                return;

            CVertice inicio = ObtenerNodoSeleccionado(CBNodoPartida, "Debe seleccionar un municipio inicial para el recorrido en anchura.");

            if (inicio == null)
                return;

            // BFS devuelve el orden de visita usando una cola.
            List<CVertice> recorrido = grafo.RecorridoAnchura(inicio);

            await AnimarRecorrido(recorrido, "BFS / Anchura", Color.FromArgb(255, 214, 0));
        }

        // Botón de recorrido en profundidad.
        private async void BtnProf_Click(object sender, EventArgs e)
        {
            if (animando)
                return;

            CVertice inicio = ObtenerNodoSeleccionado(CBNodoPartida, "Debe seleccionar un municipio inicial para el recorrido en profundidad.");

            if (inicio == null)
                return;

            // DFS devuelve el orden de visita usando recursión.
            List<CVertice> recorrido = grafo.RecorridoProfundidad(inicio);

            await AnimarRecorrido(recorrido, "DFS / Profundidad", Color.FromArgb(255, 128, 0));
        }

        // Toma el municipio seleccionado en un ComboBox y valida que no esté vacío.
        private CVertice ObtenerNodoSeleccionado(ComboBox combo, string mensajeError)
        {
            if (combo.SelectedIndex == -1)
            {
                MessageBox.Show(mensajeError, "Simulador", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return grafo.BuscarVertice(combo.SelectedItem.ToString());
        }

        // Anima BFS o DFS pintando los nodos en el orden en que se visitan.
        private async Task AnimarRecorrido(List<CVertice> recorrido, string nombreAlgoritmo, Color colorNodo)
        {
            animando = true;

            CambiarEstadoBotones(false);

            grafo.ReestablecerGrafo();
            lstRecorrido.Items.Clear();
            lstRutasDijk.Items.Clear();
            rutaCompletaActual = nombreAlgoritmo + ": " + ConstruirTextoRuta(recorrido);
            recorridoCompletoActual = rutaCompletaActual;
            detalleCompletoActual = string.Empty;
            lblRutaRecorrido.Text = nombreAlgoritmo + ":";
            lblResultDijk.Text = "Visualizando " + nombreAlgoritmo + "...";
            Pizarra.Refresh();

            for (int i = 0; i < recorrido.Count; i++)
            {
                CVertice actual = recorrido[i];

                actual.Color = colorNodo;
                actual.FontColor = Color.Black;

                if (actual.Padre != null)
                    grafo.ResaltarArista(actual.Padre, actual, colorNodo, 4);

                lstRecorrido.Items.Add((i + 1).ToString("00") + ". " + actual.Valor);
                lstRecorrido.SelectedIndex = lstRecorrido.Items.Count - 1;
                ActualizarScrollHorizontal(lstRecorrido);

                lblRutaRecorrido.Text = nombreAlgoritmo + ": " + ConstruirTextoRuta(recorrido.GetRange(0, i + 1));

                Pizarra.Refresh();

                await Task.Delay(650);
            }

            lblResultDijk.Text = nombreAlgoritmo + " finalizado. Municipios visitados: " + recorrido.Count;
            lblRutaRecorrido.Text = rutaCompletaActual;
            detalleCompletoActual = ConstruirTextoItems(lstRecorrido);

            CambiarEstadoBotones(true);

            animando = false;
        }

        // Botón para buscar la ruta más corta entre origen y destino.
        private async void BtnDijkstra_Click(object sender, EventArgs e)
        {
            if (animando)
                return;

            CVertice origen = ObtenerNodoSeleccionado(CBOrigenDijk, "Debe seleccionar el municipio origen.");

            CVertice destino = ObtenerNodoSeleccionado(CBDestinoDijk, "Debe seleccionar el municipio destino.");

            if (origen == null || destino == null)
                return;

            if (origen == destino)
            {
                MessageBox.Show("El origen y el destino no pueden ser el mismo municipio.", "Dijkstra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dijkstra calcula el camino con menor suma de kilómetros.
            List<CVertice> ruta = grafo.Dijkstra(origen, destino);

            if (ruta == null)
            {
                lblResultDijk.Text = "No existe ruta entre " + origen.Valor + " y " + destino.Valor + ".";
                rutaCompletaActual = lblResultDijk.Text;
                lstRutasDijk.Items.Clear();
                lstRutasDijk.Items.Add(lblResultDijk.Text);
                ActualizarScrollHorizontal(lstRutasDijk);
                detalleCompletoActual = ConstruirTextoItems(lstRutasDijk);
                Pizarra.Refresh();
                return;
            }

            await AnimarDijkstra(ruta, destino.distancianodo);
        }

        // Anima la ruta de Dijkstra mostrando el costo parcial de cada tramo.
        private async Task AnimarDijkstra(List<CVertice> ruta, int distanciaTotal)
        {
            animando = true;

            CambiarEstadoBotones(false);

            grafo.ReestablecerGrafo();
            lstRutasDijk.Items.Clear();
            lstRecorrido.Items.Clear();
            rutaCompletaActual = "Ruta más corta: " + ConstruirTextoRuta(ruta) + " | Distancia total: " + distanciaTotal + " km";
            detalleCompletoActual = string.Empty;
            lblRutaRecorrido.Text = "Ruta más corta:";
            lblResultDijk.Text = "Calculando ruta...";
            Pizarra.Refresh();

            int costoParcial = 0;

            for (int i = 0; i < ruta.Count; i++)
            {
                ruta[i].Color = Color.FromArgb(255, 168, 0);
                ruta[i].FontColor = Color.Black;

                if (i > 0)
                {
                    CVertice anterior = ruta[i - 1];
                    CVertice actual = ruta[i];

                    int pesoPaso = grafo.PesoEntre(anterior, actual);
                    costoParcial += pesoPaso;

                    grafo.ResaltarArista(anterior, actual, Color.FromArgb(255, 168, 0), 5);

                    lstRutasDijk.Items.Add(anterior.Valor + " -> " + actual.Valor + " | +" + pesoPaso + " km | parcial: " + costoParcial + " km");
                    lstRutasDijk.SelectedIndex = lstRutasDijk.Items.Count - 1;
                    ActualizarScrollHorizontal(lstRutasDijk);
                }
                else
                {
                    lstRutasDijk.Items.Add("Inicio: " + ruta[i].Valor + " | parcial: 0 km");
                    ActualizarScrollHorizontal(lstRutasDijk);
                }

                lblRutaRecorrido.Text = "Ruta más corta: " + ConstruirTextoRuta(ruta.GetRange(0, i + 1));
                lblResultDijk.Text = "Costo parcial: " + costoParcial + " km";

                Pizarra.Refresh();

                await Task.Delay(700);
            }

            lblResultDijk.Text = "Ruta: " + ConstruirTextoRuta(ruta) + " | Distancia total: " + distanciaTotal + " km";
            lstRutasDijk.Items.Add("TOTAL: " + distanciaTotal + " km");
            ActualizarScrollHorizontal(lstRutasDijk);
            detalleCompletoActual = ConstruirTextoItems(lstRutasDijk);

            CambiarEstadoBotones(true);

            animando = false;
        }

        // Muestra las rutas mínimas desde un solo origen hacia los demás municipios.
        private void BtnRutasOrigen_Click(object sender, EventArgs e)
        {
            if (animando)
                return;

            CVertice origen = ObtenerNodoSeleccionado(CBOrigenDijk, "Debe seleccionar un municipio origen.");

            if (origen == null)
                return;

            grafo.ReestablecerGrafo();
            lstRutasDijk.Items.Clear();
            lstRecorrido.Items.Clear();
            rutaCompletaActual = "Rutas mínimas desde: " + origen.Valor;
            detalleCompletoActual = string.Empty;

            foreach (CVertice destino in grafo.nodos)
            {
                if (destino == origen)
                    continue;

                // Dijkstra calcula el camino con menor suma de kilómetros.
            List<CVertice> ruta = grafo.Dijkstra(origen, destino);

                if (ruta != null)
                    lstRutasDijk.Items.Add(ConstruirTextoRuta(ruta) + " | " + destino.distancianodo + " km");
            }

            if (lstRutasDijk.Items.Count == 0)
                lstRutasDijk.Items.Add("No hay municipios alcanzables desde " + origen.Valor + ".");

            ActualizarScrollHorizontal(lstRutasDijk);
            detalleCompletoActual = ConstruirTextoItems(lstRutasDijk);

            grafo.ReestablecerGrafo();
            origen.Color = Color.FromArgb(255, 214, 0);
            origen.FontColor = Color.Black;
            Pizarra.Refresh();

            lblResultDijk.Text = "Rutas mínimas desde: " + origen.Valor;
            lblRutaRecorrido.Text = "Rutas listadas en el panel inferior.";
        }

        // Limpia colores, listas y textos sin borrar el grafo.
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            grafo.ReestablecerGrafo();

            lstRecorrido.Items.Clear();
            lstRutasDijk.Items.Clear();
            lblRutaRecorrido.Text = "Recorrido:";
            lblResultDijk.Text = "Seleccione origen y destino.";
            rutaCompletaActual = string.Empty;
            recorridoCompletoActual = string.Empty;
            detalleCompletoActual = string.Empty;
            ActualizarScrollHorizontal(lstRecorrido);
            ActualizarScrollHorizontal(lstRutasDijk);

            Pizarra.Refresh();
        }

        // Recarga el grafo y regresa la vista al estado inicial.
        private void BtnReiniciar_Click(object sender, EventArgs e)
        {
            CrearGrafoSanVicente();

            ActualizarCombos();
            ActualizarResumenGrafo();

            lstRecorrido.Items.Clear();
            lstRutasDijk.Items.Clear();
            lblRutaRecorrido.Text = "Recorrido:";
            lblResultDijk.Text = "Grafo reiniciado.";
            rutaCompletaActual = string.Empty;
            recorridoCompletoActual = string.Empty;
            detalleCompletoActual = string.Empty;
            ActualizarScrollHorizontal(lstRecorrido);
            ActualizarScrollHorizontal(lstRutasDijk);

            ReiniciarVistaMapa();

            Pizarra.Refresh();
        }

        // Bloquea o desbloquea botones mientras hay una animación.
        private void CambiarEstadoBotones(bool habilitado)
        {
            BtnAnch.Enabled = habilitado;
            BtnProf.Enabled = habilitado;

            BtnDijkstra.Enabled = habilitado;
            BtnRutasOrigen.Enabled = habilitado;

            BtnLimpiar.Enabled = habilitado;
            BtnReiniciar.Enabled = habilitado;
            BtnVerRutaCompleta.Enabled = habilitado;
            BtnVerRecorridoCompleto.Enabled = habilitado;
            BtnVerDetalleCompleto.Enabled = habilitado;
        }

        // Abre el texto completo de la última ruta corta.
        private void BtnVerRutaCompleta_Click(object sender, EventArgs e)
        {
            string texto = string.IsNullOrWhiteSpace(rutaCompletaActual)
                ? "Aún no hay una ruta o recorrido para mostrar."
                : rutaCompletaActual;

            MessageBox.Show(texto, "Ruta completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Abre el texto completo del último BFS o DFS.
        private void BtnVerRecorridoCompleto_Click(object sender, EventArgs e)
        {
            string texto = string.IsNullOrWhiteSpace(recorridoCompletoActual)
                ? ConstruirTextoItems(lstRecorrido)
                : recorridoCompletoActual;

            if (string.IsNullOrWhiteSpace(texto))
                texto = "Aún no hay un recorrido para mostrar.";

            MessageBox.Show(texto, "Recorrido completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Abre todo el detalle del panel inferior.
        private void BtnVerDetalleCompleto_Click(object sender, EventArgs e)
        {
            string texto = string.IsNullOrWhiteSpace(detalleCompletoActual)
                ? ConstruirTextoItems(lstRutasDijk)
                : detalleCompletoActual;

            if (string.IsNullOrWhiteSpace(texto))
                texto = "Aún no hay resultados para mostrar.";

            MessageBox.Show(texto, "Detalle completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Ajusta el scroll horizontal para listas con texto largo.
        private void ActualizarScrollHorizontal(ListBox lista)
        {
            if (lista.Items.Count == 0)
            {
                lista.HorizontalExtent = 0;
                return;
            }

            int anchoMayor = 0;

            using (Graphics g = lista.CreateGraphics())
            {
                foreach (object item in lista.Items)
                {
                    int ancho = (int)Math.Ceiling(g.MeasureString(item.ToString(), lista.Font).Width);
                    if (ancho > anchoMayor)
                        anchoMayor = ancho;
                }
            }

            lista.HorizontalExtent = anchoMayor + 25;
        }

        // Une los elementos de una lista para mostrarlos en un mensaje.
        private string ConstruirTextoItems(ListBox lista)
        {
            List<string> lineas = new List<string>();
            foreach (object item in lista.Items)
                lineas.Add(item.ToString());

            return string.Join(Environment.NewLine, lineas.ToArray());
        }

        // Convierte una lista de municipios en texto con flechas.
        private string ConstruirTextoRuta(List<CVertice> ruta)
        {
            List<string> nombres = new List<string>();
            foreach (CVertice nodo in ruta)
                nombres.Add(nodo.Valor);

            return string.Join(" -> ", nombres.ToArray());
        }
    }
}
