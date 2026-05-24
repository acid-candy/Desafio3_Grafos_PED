using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RutaCorta
{
    // Cada objeto de esta clase es un municipio dentro del grafo.
    // Además de guardar sus conexiones, también contiene datos para dibujarse en el mapa.
    public class CVertice
    {
        public string Valor;
        public string Sigla;
        public List<CArco> ListaAdyacencia;

        // Estos campos se usan en los recorridos y en Dijkstra.
        public int distancianodo;
        public bool Visitado;
        public CVertice Padre;
        public bool pesoasignado;

        // Tamaño del círculo que representa al municipio en el mapa.
        private static readonly int size = 14;

        private Size dimensiones;
        private Color color_nodo;
        private Color color_fuente;
        private Point posicion;
        private int radio;

        public Color Color
        {
            get { return color_nodo; }
            set { color_nodo = value; }
        }

        public Color FontColor
        {
            get { return color_fuente; }
            set { color_fuente = value; }
        }

        public Point Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public Size Dimensiones
        {
            get { return dimensiones; }
            set
            {
                dimensiones = value;
                radio = value.Width / 2;
            }
        }

        public CVertice(string valor, string sigla)
        {
            Valor = valor;
            Sigla = sigla;
            ListaAdyacencia = new List<CArco>();

            // Color inicial de los nodos antes de ejecutar cualquier algoritmo.
            Color = Color.FromArgb(0, 87, 183);
            FontColor = Color.White;
            Dimensiones = new Size(size, size);

            // Estado inicial para los algoritmos.
            Visitado = false;
            Padre = null;
            distancianodo = int.MaxValue;
            pesoasignado = false;
        }

        public CVertice() : this(string.Empty, string.Empty)
        {
            // Constructor vacío usado por compatibilidad con el proyecto base.
        }

        public void DibujarVertice(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle areaNodo = new Rectangle(
                posicion.X - radio,
                posicion.Y - radio,
                dimensiones.Width,
                dimensiones.Height);

            // Primero se dibuja el círculo y luego la sigla del municipio.
            using (SolidBrush brochaNodo = new SolidBrush(color_nodo))
                g.FillEllipse(brochaNodo, areaNodo);

            using (Pen contorno = new Pen(Color.Black, 1.0f))
                g.DrawEllipse(contorno, areaNodo);

            using (Font fuenteSigla = new Font("Segoe UI", 5.2f, FontStyle.Bold))
            using (SolidBrush brochaTexto = new SolidBrush(color_fuente))
            using (StringFormat formato = new StringFormat())
            {
                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;
                g.DrawString(Sigla, fuenteSigla, brochaTexto, areaNodo, formato);
            }

            // El nombre completo ya no queda fijo; se muestra al pasar el mouse.
        }

        private void DibujarEtiquetaMunicipio(Graphics g)
        {
            // Método conservado por si se desea volver a mostrar etiquetas fijas.
            using (Font fuente = new Font("Segoe UI", 7, FontStyle.Bold))
            {
                SizeF medida = g.MeasureString(Valor, fuente);

                RectangleF rectEtiqueta = new RectangleF(
                    posicion.X + radio - 2,
                    posicion.Y + radio - 2,
                    medida.Width + 6,
                    medida.Height + 3);

                using (SolidBrush fondo = new SolidBrush(Color.FromArgb(220, Color.White)))
                    g.FillRectangle(fondo, rectEtiqueta);

                using (Pen borde = new Pen(Color.FromArgb(120, Color.Black), 1))
                    g.DrawRectangle(borde, Rectangle.Round(rectEtiqueta));

                using (SolidBrush texto = new SolidBrush(Color.Black))
                    g.DrawString(Valor, fuente, texto, rectEtiqueta.X + 3, rectEtiqueta.Y + 1);
            }
        }

        public bool DetectarPunto(Point p)
        {
            // Sirve para saber si el mouse está encima del nodo.
            using (GraphicsPath posicionNodo = new GraphicsPath())
            {
                posicionNodo.AddEllipse(new Rectangle(
                    posicion.X - radio,
                    posicion.Y - radio,
                    dimensiones.Width,
                    dimensiones.Height));

                return posicionNodo.IsVisible(p);
            }
        }

        public override string ToString()
        {
            return Valor;
        }
    }
}
