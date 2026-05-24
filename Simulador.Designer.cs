namespace RutaCorta
{
    // Archivo generado por el diseñador de Windows Forms.
    // Por eso aquí solo se dejaron comentarios mínimos de ubicación de controles.
    partial class Simulador
    {
        /// <summary>
        /// Contenedor requerido por Windows Forms para administrar controles visuales.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Libera recursos usados por el formulario.
        /// </summary>
        /// <param name="disposing">Indica si se deben liberar recursos administrados.</param>
        protected override void Dispose(bool disposing)
        {
            // Libera el contenedor de componentes si existe.
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            // Ejecuta la limpieza base del formulario.
            base.Dispose(disposing);
        }

        #region Código generado y documentado para la interfaz del desafío

        /// <summary>
        /// Crea y posiciona todos los controles del mockup.
        /// </summary>
        private void InitializeComponent()
        {
            // Inicializa el contenedor de componentes.
            this.components = new System.ComponentModel.Container();

            // Crea los controles principales del encabezado.
            this.picBandera = new System.Windows.Forms.PictureBox();
            this.picMapaMini = new System.Windows.Forms.PictureBox();
            this.lblTituloPrincipal = new System.Windows.Forms.Label();
            this.lblTituloDepto = new System.Windows.Forms.Label();

            // Crea los controles decorativos del diseño.
            this.picTorogoz = new RutaCorta.PictureBoxTransparente();
            this.picMaquilishuat = new RutaCorta.PictureBoxTransparente();

            // Crea la pizarra donde se dibuja el mapa y el grafo.
            this.Pizarra = new System.Windows.Forms.Panel();

            // Crea los tres paneles laterales con borde azul, como en el mockup.
            this.panelRuta = new System.Windows.Forms.Panel();
            this.panelRecorridos = new System.Windows.Forms.Panel();
            this.panelResultados = new System.Windows.Forms.Panel();

            // Crea controles del panel de ruta más corta.
            this.lblPanelRutaTitulo = new System.Windows.Forms.Label();
            this.lblOrigenDijk = new System.Windows.Forms.Label();
            this.lblDestinoDijk = new System.Windows.Forms.Label();
            this.CBOrigenDijk = new System.Windows.Forms.ComboBox();
            this.CBDestinoDijk = new System.Windows.Forms.ComboBox();
            this.BtnDijkstra = new System.Windows.Forms.Button();
            this.BtnRutasOrigen = new System.Windows.Forms.Button();
            this.lblResultDijk = new System.Windows.Forms.Label();
            this.BtnVerRutaCompleta = new System.Windows.Forms.Button();

            // Crea controles del panel de recorridos BFS y DFS.
            this.lblPanelRecorridosTitulo = new System.Windows.Forms.Label();
            this.lblNodoPartida = new System.Windows.Forms.Label();
            this.CBNodoPartida = new System.Windows.Forms.ComboBox();
            this.BtnAnch = new System.Windows.Forms.Button();
            this.BtnProf = new System.Windows.Forms.Button();
            this.lstRecorrido = new System.Windows.Forms.ListBox();
            this.lblRutaRecorrido = new System.Windows.Forms.Label();
            this.BtnVerRecorridoCompleto = new System.Windows.Forms.Button();

            // Crea controles del panel inferior de resultados y mantenimiento.
            this.lblPanelResultadosTitulo = new System.Windows.Forms.Label();
            this.lstRutasDijk = new System.Windows.Forms.ListBox();
            this.BtnLimpiar = new System.Windows.Forms.Button();
            this.BtnReiniciar = new System.Windows.Forms.Button();
            this.lblResumenGrafo = new System.Windows.Forms.Label();
            this.BtnVerDetalleCompleto = new System.Windows.Forms.Button();

            // Suspende el layout mientras se configuran los controles.
            ((System.ComponentModel.ISupportInitialize)(this.picBandera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMapaMini)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTorogoz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMaquilishuat)).BeginInit();
            this.panelRuta.SuspendLayout();
            this.panelRecorridos.SuspendLayout();
            this.panelResultados.SuspendLayout();
            this.SuspendLayout();

            // 
            // picBandera
            // 
            // Muestra la bandera en la esquina superior izquierda del mockup.
            this.picBandera.BackColor = System.Drawing.Color.Transparent;
            this.picBandera.Location = new System.Drawing.Point(0, 0);
            this.picBandera.Name = "picBandera";
            this.picBandera.Size = new System.Drawing.Size(118, 145);
            this.picBandera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBandera.TabIndex = 0;
            this.picBandera.TabStop = false;

            // 
            // picMapaMini
            // 
            // Muestra el mapa pequeño ubicado en la parte superior derecha.
            this.picMapaMini.BackColor = System.Drawing.Color.Transparent;
            this.picMapaMini.Location = new System.Drawing.Point(1262, 5);
            this.picMapaMini.Name = "picMapaMini";
            this.picMapaMini.Size = new System.Drawing.Size(96, 120);
            this.picMapaMini.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMapaMini.TabIndex = 1;
            this.picMapaMini.TabStop = false;

            // 
            // lblTituloPrincipal
            // 
            // Título principal del simulador.
            this.lblTituloPrincipal.AutoSize = true;
            this.lblTituloPrincipal.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloPrincipal.Font = new System.Drawing.Font("Arial", 34F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloPrincipal.ForeColor = System.Drawing.Color.Black;
            this.lblTituloPrincipal.Location = new System.Drawing.Point(230, 2);
            this.lblTituloPrincipal.Name = "lblTituloPrincipal";
            this.lblTituloPrincipal.Size = new System.Drawing.Size(886, 63);
            this.lblTituloPrincipal.TabIndex = 2;
            this.lblTituloPrincipal.Text = "Simulador de distancia de municipios";

            // 
            // lblTituloDepto
            // 
            // Subtítulo caligráfico del departamento asignado.
            this.lblTituloDepto.AutoSize = true;
            this.lblTituloDepto.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloDepto.Font = new System.Drawing.Font("Brush Script MT", 58F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloDepto.ForeColor = System.Drawing.Color.Black;
            this.lblTituloDepto.Location = new System.Drawing.Point(505, 52);
            this.lblTituloDepto.Name = "lblTituloDepto";
            this.lblTituloDepto.Size = new System.Drawing.Size(455, 117);
            this.lblTituloDepto.TabIndex = 3;
            this.lblTituloDepto.Text = "San Vicente";

            // 
            // Pizarra
            // 
            // Panel principal donde se dibuja el mapa de San Vicente y el grafo.
            this.Pizarra.BackColor = System.Drawing.Color.White;
            this.Pizarra.Location = new System.Drawing.Point(460, 150);
            this.Pizarra.Name = "Pizarra";
            this.Pizarra.Size = new System.Drawing.Size(876, 575);
            this.Pizarra.TabStop = true;
            this.Pizarra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pizarra_MouseDown);
            this.Pizarra.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pizarra_MouseMove);
            this.Pizarra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Pizarra_MouseUp);
            this.Pizarra.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Pizarra_MouseWheel);
            this.Pizarra.MouseEnter += new System.EventHandler(this.Pizarra_MouseEnter);
            this.Pizarra.TabIndex = 4;
            this.Pizarra.Paint += new System.Windows.Forms.PaintEventHandler(this.Pizarra_Paint);

            // 
            // panelRuta
            // 
            // Primer rectángulo lateral: controles de Dijkstra.
            this.panelRuta.BackColor = System.Drawing.Color.White;
            this.panelRuta.Controls.Add(this.lblPanelRutaTitulo);
            this.panelRuta.Controls.Add(this.lblOrigenDijk);
            this.panelRuta.Controls.Add(this.lblDestinoDijk);
            this.panelRuta.Controls.Add(this.CBOrigenDijk);
            this.panelRuta.Controls.Add(this.CBDestinoDijk);
            this.panelRuta.Controls.Add(this.BtnDijkstra);
            this.panelRuta.Controls.Add(this.BtnRutasOrigen);
            this.panelRuta.Controls.Add(this.lblResultDijk);
            this.panelRuta.Controls.Add(this.BtnVerRutaCompleta);
            this.panelRuta.Location = new System.Drawing.Point(22, 150);
            this.panelRuta.Name = "panelRuta";
            this.panelRuta.Size = new System.Drawing.Size(411, 122);
            this.panelRuta.TabIndex = 5;
            this.panelRuta.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBordeAzul_Paint);

            // 
            // lblPanelRutaTitulo
            // 
            // Encabezado interno del panel de ruta más corta.
            this.lblPanelRutaTitulo.AutoSize = true;
            this.lblPanelRutaTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPanelRutaTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 80, 180);
            this.lblPanelRutaTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblPanelRutaTitulo.Name = "lblPanelRutaTitulo";
            this.lblPanelRutaTitulo.Size = new System.Drawing.Size(170, 19);
            this.lblPanelRutaTitulo.TabIndex = 0;
            this.lblPanelRutaTitulo.Text = "Ruta más corta (Dijkstra)";

            // 
            // lblOrigenDijk
            // 
            // Etiqueta del municipio origen.
            this.lblOrigenDijk.AutoSize = true;
            this.lblOrigenDijk.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblOrigenDijk.Location = new System.Drawing.Point(16, 37);
            this.lblOrigenDijk.Name = "lblOrigenDijk";
            this.lblOrigenDijk.Size = new System.Drawing.Size(48, 15);
            this.lblOrigenDijk.TabIndex = 1;
            this.lblOrigenDijk.Text = "Origen:";

            // 
            // lblDestinoDijk
            // 
            // Etiqueta del municipio destino.
            this.lblDestinoDijk.AutoSize = true;
            this.lblDestinoDijk.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDestinoDijk.Location = new System.Drawing.Point(16, 63);
            this.lblDestinoDijk.Name = "lblDestinoDijk";
            this.lblDestinoDijk.Size = new System.Drawing.Size(52, 15);
            this.lblDestinoDijk.TabIndex = 2;
            this.lblDestinoDijk.Text = "Destino:";

            // 
            // CBOrigenDijk
            // 
            // Combo para seleccionar el origen de Dijkstra.
            this.CBOrigenDijk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBOrigenDijk.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.CBOrigenDijk.FormattingEnabled = true;
            this.CBOrigenDijk.Location = new System.Drawing.Point(75, 34);
            this.CBOrigenDijk.Name = "CBOrigenDijk";
            this.CBOrigenDijk.Size = new System.Drawing.Size(160, 21);
            this.CBOrigenDijk.TabIndex = 3;

            // 
            // CBDestinoDijk
            // 
            // Combo para seleccionar el destino de Dijkstra.
            this.CBDestinoDijk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBDestinoDijk.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.CBDestinoDijk.FormattingEnabled = true;
            this.CBDestinoDijk.Location = new System.Drawing.Point(75, 60);
            this.CBDestinoDijk.Name = "CBDestinoDijk";
            this.CBDestinoDijk.Size = new System.Drawing.Size(160, 21);
            this.CBDestinoDijk.TabIndex = 4;

            // 
            // BtnDijkstra
            // 
            // Botón que calcula y anima una ruta mínima entre dos municipios.
            this.BtnDijkstra.BackColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnDijkstra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDijkstra.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDijkstra.ForeColor = System.Drawing.Color.White;
            this.BtnDijkstra.Location = new System.Drawing.Point(249, 31);
            this.BtnDijkstra.Name = "BtnDijkstra";
            this.BtnDijkstra.Size = new System.Drawing.Size(144, 25);
            this.BtnDijkstra.TabIndex = 5;
            this.BtnDijkstra.Text = "Buscar ruta";
            this.BtnDijkstra.UseVisualStyleBackColor = false;
            this.BtnDijkstra.Click += new System.EventHandler(this.BtnDijkstra_Click);

            // 
            // BtnRutasOrigen
            // 
            // Botón que lista la ruta mínima desde un origen hacia todos los alcanzables.
            this.BtnRutasOrigen.BackColor = System.Drawing.Color.White;
            this.BtnRutasOrigen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRutasOrigen.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.BtnRutasOrigen.ForeColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnRutasOrigen.Location = new System.Drawing.Point(249, 59);
            this.BtnRutasOrigen.Name = "BtnRutasOrigen";
            this.BtnRutasOrigen.Size = new System.Drawing.Size(144, 25);
            this.BtnRutasOrigen.TabIndex = 6;
            this.BtnRutasOrigen.Text = "Rutas desde origen";
            this.BtnRutasOrigen.UseVisualStyleBackColor = false;
            this.BtnRutasOrigen.Click += new System.EventHandler(this.BtnRutasOrigen_Click);

            // 
            // lblResultDijk
            // 
            // Resultado breve de Dijkstra.
            this.lblResultDijk.AutoEllipsis = true;
            this.lblResultDijk.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblResultDijk.ForeColor = System.Drawing.Color.Black;
            this.lblResultDijk.Location = new System.Drawing.Point(15, 91);
            this.lblResultDijk.Name = "lblResultDijk";
            this.lblResultDijk.Size = new System.Drawing.Size(220, 18);
            this.lblResultDijk.TabIndex = 7;
            this.lblResultDijk.Text = "Seleccione origen y destino.";

            // 
            // BtnVerRutaCompleta
            // 
            // Botón con ojito para mostrar la ruta completa cuando el texto queda recortado.
            this.BtnVerRutaCompleta.BackColor = System.Drawing.Color.White;
            this.BtnVerRutaCompleta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVerRutaCompleta.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.BtnVerRutaCompleta.ForeColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnVerRutaCompleta.Location = new System.Drawing.Point(249, 87);
            this.BtnVerRutaCompleta.Name = "BtnVerRutaCompleta";
            this.BtnVerRutaCompleta.Size = new System.Drawing.Size(144, 25);
            this.BtnVerRutaCompleta.TabIndex = 8;
            this.BtnVerRutaCompleta.Text = "👁 Ver todo";
            this.BtnVerRutaCompleta.UseVisualStyleBackColor = false;
            this.BtnVerRutaCompleta.Click += new System.EventHandler(this.BtnVerRutaCompleta_Click);

            // 
            // panelRecorridos
            // 
            // Segundo rectángulo lateral: recorridos BFS y DFS.
            this.panelRecorridos.BackColor = System.Drawing.Color.White;
            this.panelRecorridos.Controls.Add(this.lblPanelRecorridosTitulo);
            this.panelRecorridos.Controls.Add(this.lblNodoPartida);
            this.panelRecorridos.Controls.Add(this.CBNodoPartida);
            this.panelRecorridos.Controls.Add(this.BtnAnch);
            this.panelRecorridos.Controls.Add(this.BtnProf);
            this.panelRecorridos.Controls.Add(this.lstRecorrido);
            this.panelRecorridos.Controls.Add(this.lblRutaRecorrido);
            this.panelRecorridos.Controls.Add(this.BtnVerRecorridoCompleto);
            this.panelRecorridos.Location = new System.Drawing.Point(22, 288);
            this.panelRecorridos.Name = "panelRecorridos";
            this.panelRecorridos.Size = new System.Drawing.Size(411, 270);
            this.panelRecorridos.TabIndex = 6;
            this.panelRecorridos.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBordeAzul_Paint);

            // 
            // lblPanelRecorridosTitulo
            // 
            // Encabezado interno del panel de recorridos.
            this.lblPanelRecorridosTitulo.AutoSize = true;
            this.lblPanelRecorridosTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPanelRecorridosTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 80, 180);
            this.lblPanelRecorridosTitulo.Location = new System.Drawing.Point(16, 17);
            this.lblPanelRecorridosTitulo.Name = "lblPanelRecorridosTitulo";
            this.lblPanelRecorridosTitulo.Size = new System.Drawing.Size(150, 20);
            this.lblPanelRecorridosTitulo.TabIndex = 0;
            this.lblPanelRecorridosTitulo.Text = "Recorridos visuales";

            // 
            // lblNodoPartida
            // 
            // Etiqueta de nodo inicial para BFS y DFS.
            this.lblNodoPartida.AutoSize = true;
            this.lblNodoPartida.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNodoPartida.Location = new System.Drawing.Point(17, 54);
            this.lblNodoPartida.Name = "lblNodoPartida";
            this.lblNodoPartida.Size = new System.Drawing.Size(77, 15);
            this.lblNodoPartida.TabIndex = 1;
            this.lblNodoPartida.Text = "Nodo inicial:";

            // 
            // CBNodoPartida
            // 
            // Combo de selección para el nodo inicial de recorridos.
            this.CBNodoPartida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBNodoPartida.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.CBNodoPartida.FormattingEnabled = true;
            this.CBNodoPartida.Location = new System.Drawing.Point(103, 51);
            this.CBNodoPartida.Name = "CBNodoPartida";
            this.CBNodoPartida.Size = new System.Drawing.Size(185, 21);
            this.CBNodoPartida.TabIndex = 2;

            // 
            // BtnAnch
            // 
            // Botón del recorrido en anchura.
            this.BtnAnch.BackColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnAnch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAnch.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAnch.ForeColor = System.Drawing.Color.White;
            this.BtnAnch.Location = new System.Drawing.Point(303, 45);
            this.BtnAnch.Name = "BtnAnch";
            this.BtnAnch.Size = new System.Drawing.Size(86, 29);
            this.BtnAnch.TabIndex = 3;
            this.BtnAnch.Text = "Anchura";
            this.BtnAnch.UseVisualStyleBackColor = false;
            this.BtnAnch.Click += new System.EventHandler(this.BtnAnch_Click);

            // 
            // BtnProf
            // 
            // Botón del recorrido en profundidad.
            this.BtnProf.BackColor = System.Drawing.Color.White;
            this.BtnProf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnProf.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.BtnProf.ForeColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnProf.Location = new System.Drawing.Point(303, 79);
            this.BtnProf.Name = "BtnProf";
            this.BtnProf.Size = new System.Drawing.Size(86, 29);
            this.BtnProf.TabIndex = 4;
            this.BtnProf.Text = "Profundidad";
            this.BtnProf.UseVisualStyleBackColor = false;
            this.BtnProf.Click += new System.EventHandler(this.BtnProf_Click);

            // 
            // lstRecorrido
            // 
            // Lista donde se muestra el orden de visita de BFS o DFS.
            this.lstRecorrido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRecorrido.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstRecorrido.FormattingEnabled = true;
            this.lstRecorrido.HorizontalScrollbar = true;
            this.lstRecorrido.ItemHeight = 14;
            this.lstRecorrido.Location = new System.Drawing.Point(20, 88);
            this.lstRecorrido.Name = "lstRecorrido";
            this.lstRecorrido.Size = new System.Drawing.Size(250, 150);
            this.lstRecorrido.TabIndex = 5;

            // 
            // lblRutaRecorrido
            // 
            // Etiqueta que resume el recorrido completo.
            this.lblRutaRecorrido.AutoEllipsis = true;
            this.lblRutaRecorrido.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblRutaRecorrido.Location = new System.Drawing.Point(20, 244);
            this.lblRutaRecorrido.Name = "lblRutaRecorrido";
            this.lblRutaRecorrido.Size = new System.Drawing.Size(250, 18);
            this.lblRutaRecorrido.TabIndex = 6;
            this.lblRutaRecorrido.Text = "Recorrido:";

            // 
            // BtnVerRecorridoCompleto
            // 
            // Botón para mostrar el recorrido completo cuando la etiqueta inferior está recortada.
            this.BtnVerRecorridoCompleto.BackColor = System.Drawing.Color.White;
            this.BtnVerRecorridoCompleto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVerRecorridoCompleto.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Bold);
            this.BtnVerRecorridoCompleto.ForeColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnVerRecorridoCompleto.Location = new System.Drawing.Point(303, 113);
            this.BtnVerRecorridoCompleto.Name = "BtnVerRecorridoCompleto";
            this.BtnVerRecorridoCompleto.Size = new System.Drawing.Size(86, 29);
            this.BtnVerRecorridoCompleto.TabIndex = 7;
            this.BtnVerRecorridoCompleto.Text = "👁 Ver todo";
            this.BtnVerRecorridoCompleto.UseVisualStyleBackColor = false;
            this.BtnVerRecorridoCompleto.Click += new System.EventHandler(this.BtnVerRecorridoCompleto_Click);

            // 
            // panelResultados
            // 
            // Tercer rectángulo lateral: resultados detallados y botones de limpieza.
            this.panelResultados.BackColor = System.Drawing.Color.White;
            this.panelResultados.Controls.Add(this.lblPanelResultadosTitulo);
            this.panelResultados.Controls.Add(this.lstRutasDijk);
            this.panelResultados.Controls.Add(this.BtnLimpiar);
            this.panelResultados.Controls.Add(this.BtnReiniciar);
            this.panelResultados.Controls.Add(this.lblResumenGrafo);
            this.panelResultados.Controls.Add(this.BtnVerDetalleCompleto);
            this.panelResultados.Location = new System.Drawing.Point(22, 572);
            this.panelResultados.Name = "panelResultados";
            this.panelResultados.Size = new System.Drawing.Size(411, 162);
            this.panelResultados.TabIndex = 7;
            this.panelResultados.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBordeAzul_Paint);

            // 
            // lblPanelResultadosTitulo
            // 
            // Encabezado interno del panel inferior.
            this.lblPanelResultadosTitulo.AutoSize = true;
            this.lblPanelResultadosTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPanelResultadosTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 80, 180);
            this.lblPanelResultadosTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblPanelResultadosTitulo.Name = "lblPanelResultadosTitulo";
            this.lblPanelResultadosTitulo.Size = new System.Drawing.Size(229, 19);
            this.lblPanelResultadosTitulo.TabIndex = 0;
            this.lblPanelResultadosTitulo.Text = "Resultado y rutas desde un origen";

            // 
            // lstRutasDijk
            // 
            // Lista usada para costos parciales o rutas múltiples desde un origen.
            this.lstRutasDijk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRutasDijk.Font = new System.Drawing.Font("Consolas", 8F);
            this.lstRutasDijk.FormattingEnabled = true;
            this.lstRutasDijk.HorizontalScrollbar = true;
            this.lstRutasDijk.ItemHeight = 13;
            this.lstRutasDijk.Location = new System.Drawing.Point(19, 36);
            this.lstRutasDijk.Name = "lstRutasDijk";
            this.lstRutasDijk.Size = new System.Drawing.Size(255, 80);
            this.lstRutasDijk.TabIndex = 1;

            // 
            // BtnLimpiar
            // 
            // Botón para devolver el grafo a colores normales.
            this.BtnLimpiar.BackColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLimpiar.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.BtnLimpiar.ForeColor = System.Drawing.Color.White;
            this.BtnLimpiar.Location = new System.Drawing.Point(288, 36);
            this.BtnLimpiar.Name = "BtnLimpiar";
            this.BtnLimpiar.Size = new System.Drawing.Size(103, 28);
            this.BtnLimpiar.TabIndex = 2;
            this.BtnLimpiar.Text = "Limpiar colores";
            this.BtnLimpiar.UseVisualStyleBackColor = false;
            this.BtnLimpiar.Click += new System.EventHandler(this.BtnLimpiar_Click);

            // 
            // BtnReiniciar
            // 
            // Botón para recargar el grafo original de San Vicente.
            this.BtnReiniciar.BackColor = System.Drawing.Color.White;
            this.BtnReiniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnReiniciar.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.BtnReiniciar.ForeColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnReiniciar.Location = new System.Drawing.Point(288, 72);
            this.BtnReiniciar.Name = "BtnReiniciar";
            this.BtnReiniciar.Size = new System.Drawing.Size(103, 28);
            this.BtnReiniciar.TabIndex = 3;
            this.BtnReiniciar.Text = "Reiniciar grafo";
            this.BtnReiniciar.UseVisualStyleBackColor = false;
            this.BtnReiniciar.Click += new System.EventHandler(this.BtnReiniciar_Click);

            // 
            // lblResumenGrafo
            // 
            // Muestra cantidad de municipios y caminos cargados en una sola línea para que no se corte.
            this.lblResumenGrafo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblResumenGrafo.Location = new System.Drawing.Point(19, 128);
            this.lblResumenGrafo.Name = "lblResumenGrafo";
            this.lblResumenGrafo.Size = new System.Drawing.Size(260, 24);
            this.lblResumenGrafo.TabIndex = 4;
            this.lblResumenGrafo.Text = "Municipios: 0   Caminos: 0";

            // 
            // BtnVerDetalleCompleto
            // 
            // Botón para abrir el detalle completo de resultados cuando hay texto largo.
            this.BtnVerDetalleCompleto.BackColor = System.Drawing.Color.White;
            this.BtnVerDetalleCompleto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVerDetalleCompleto.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.BtnVerDetalleCompleto.ForeColor = System.Drawing.Color.FromArgb(0, 87, 183);
            this.BtnVerDetalleCompleto.Location = new System.Drawing.Point(288, 108);
            this.BtnVerDetalleCompleto.Name = "BtnVerDetalleCompleto";
            this.BtnVerDetalleCompleto.Size = new System.Drawing.Size(103, 30);
            this.BtnVerDetalleCompleto.TabIndex = 5;
            this.BtnVerDetalleCompleto.Text = "👁 Ver todo";
            this.BtnVerDetalleCompleto.UseVisualStyleBackColor = false;
            this.BtnVerDetalleCompleto.Click += new System.EventHandler(this.BtnVerDetalleCompleto_Click);

            // 
            // picTorogoz
            // 
            // Imagen del torogoz superpuesta entre paneles y mapa.
            this.picTorogoz.BackColor = System.Drawing.Color.Transparent;
            this.picTorogoz.Location = new System.Drawing.Point(400, 170);
            this.picTorogoz.Name = "picTorogoz";
            this.picTorogoz.Size = new System.Drawing.Size(190, 120);
            this.picTorogoz.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTorogoz.TabIndex = 8;
            this.picTorogoz.TabStop = false;
            this.picTorogoz.Visible = false;

            // 
            // picMaquilishuat
            // 
            // Imagen del maquilishuat en la parte inferior derecha.
            this.picMaquilishuat.BackColor = System.Drawing.Color.Transparent;
            this.picMaquilishuat.Location = new System.Drawing.Point(1178, 610);
            this.picMaquilishuat.Name = "picMaquilishuat";
            this.picMaquilishuat.Size = new System.Drawing.Size(185, 125);
            this.picMaquilishuat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMaquilishuat.TabIndex = 9;
            this.picMaquilishuat.TabStop = false;
            this.picMaquilishuat.Visible = false;

            // 
            // Simulador
            // 
            // Configuración general del formulario para que coincida con el mockup enviado.
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1368, 739);
            this.Controls.Add(this.picMaquilishuat);
            this.Controls.Add(this.picTorogoz);
            this.Controls.Add(this.panelResultados);
            this.Controls.Add(this.panelRecorridos);
            this.Controls.Add(this.panelRuta);
            this.Controls.Add(this.Pizarra);
            this.Controls.Add(this.lblTituloDepto);
            this.Controls.Add(this.lblTituloPrincipal);
            this.Controls.Add(this.picMapaMini);
            this.Controls.Add(this.picBandera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Simulador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulador de distancia de municipios - San Vicente";

            // Reanuda el layout después de configurar todos los controles.
            ((System.ComponentModel.ISupportInitialize)(this.picBandera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMapaMini)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTorogoz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMaquilishuat)).EndInit();
            this.panelRuta.ResumeLayout(false);
            this.panelRuta.PerformLayout();
            this.panelRecorridos.ResumeLayout(false);
            this.panelRecorridos.PerformLayout();
            this.panelResultados.ResumeLayout(false);
            this.panelResultados.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Controles visuales de encabezado y decoración.
        private System.Windows.Forms.PictureBox picBandera;
        private System.Windows.Forms.PictureBox picMapaMini;
        private System.Windows.Forms.Label lblTituloPrincipal;
        private System.Windows.Forms.Label lblTituloDepto;
        private RutaCorta.PictureBoxTransparente picTorogoz;
        private RutaCorta.PictureBoxTransparente picMaquilishuat;

        // Panel principal donde se dibuja el grafo.
        private System.Windows.Forms.Panel Pizarra;

        // Paneles laterales con borde azul.
        private System.Windows.Forms.Panel panelRuta;
        private System.Windows.Forms.Panel panelRecorridos;
        private System.Windows.Forms.Panel panelResultados;

        // Controles del panel de Dijkstra.
        private System.Windows.Forms.Label lblPanelRutaTitulo;
        private System.Windows.Forms.Label lblOrigenDijk;
        private System.Windows.Forms.Label lblDestinoDijk;
        private System.Windows.Forms.ComboBox CBOrigenDijk;
        private System.Windows.Forms.ComboBox CBDestinoDijk;
        private System.Windows.Forms.Button BtnDijkstra;
        private System.Windows.Forms.Button BtnRutasOrigen;
        private System.Windows.Forms.Label lblResultDijk;
        private System.Windows.Forms.Button BtnVerRutaCompleta;

        // Controles del panel de recorridos.
        private System.Windows.Forms.Label lblPanelRecorridosTitulo;
        private System.Windows.Forms.Label lblNodoPartida;
        private System.Windows.Forms.ComboBox CBNodoPartida;
        private System.Windows.Forms.Button BtnAnch;
        private System.Windows.Forms.Button BtnProf;
        private System.Windows.Forms.ListBox lstRecorrido;
        private System.Windows.Forms.Label lblRutaRecorrido;
        private System.Windows.Forms.Button BtnVerRecorridoCompleto;

        // Controles del panel inferior.
        private System.Windows.Forms.Label lblPanelResultadosTitulo;
        private System.Windows.Forms.ListBox lstRutasDijk;
        private System.Windows.Forms.Button BtnLimpiar;
        private System.Windows.Forms.Button BtnReiniciar;
        private System.Windows.Forms.Label lblResumenGrafo;
        private System.Windows.Forms.Button BtnVerDetalleCompleto;
    }
}
