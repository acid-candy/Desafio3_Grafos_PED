using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RutaCorta
{
    // Clase principal del grafo. Aquí se guardan los municipios, los caminos y los algoritmos.
    public class CGrafo
    {
        public List<CVertice> nodos;

        // Al crear el grafo se inicia vacío; luego el formulario carga los municipios.
        public CGrafo()
        {
            nodos = new List<CVertice>();
        }

        // Cantidad de municipios cargados en el grafo.
        public int TotalVertices
        {
            get { return nodos.Count; }
        }

        // Borra todo para poder reconstruir el grafo desde cero.
        public void Limpiar()
        {
            nodos.Clear();
        }

        // Agrega un municipio a la lista de vértices.
        public void AgregarVertice(CVertice nuevonodo)
        {
            if (nuevonodo != null)
                nodos.Add(nuevonodo);
        }

        // Busca un municipio por nombre. Lo uso bastante para crear caminos y leer combos.
        public CVertice BuscarVertice(string valor)
        {
            return nodos.Find(v => string.Equals(v.Valor, valor, StringComparison.OrdinalIgnoreCase));
        }

        // Agrega una conexión dirigida. Después se usa dos veces para simular un camino de doble vía.
        public bool AgregarArco(CVertice origen, CVertice destino, int peso)
        {
            if (origen == null || destino == null)
                return false;

            if (ObtenerArco(origen, destino) != null)
                return false;

            origen.ListaAdyacencia.Add(new CArco(destino, peso));
            return true;
        }

        // Crea un camino entre dos municipios en ambos sentidos.
        public void AgregarCamino(string origen, string destino, int peso)
        {
            CVertice vOrigen = BuscarVertice(origen);
            CVertice vDestino = BuscarVertice(destino);

            if (vOrigen == null || vDestino == null)
                throw new Exception("No se pudo crear el camino entre " + origen + " y " + destino + ".");

            AgregarArco(vOrigen, vDestino, peso);
            AgregarArco(vDestino, vOrigen, peso);
        }

        // Devuelve la arista que conecta dos nodos, si existe.
        public CArco ObtenerArco(CVertice origen, CVertice destino)
        {
            if (origen == null || destino == null)
                return null;

            return origen.ListaAdyacencia.Find(a => a.nDestino == destino);
        }

        // Obtiene los kilómetros entre dos municipios conectados.
        public int PesoEntre(CVertice origen, CVertice destino)
        {
            CArco arco = ObtenerArco(origen, destino);
            return arco == null ? 0 : arco.peso;
        }

        // Limpia marcas temporales antes de ejecutar otro algoritmo.
        public void Desmarcar()
        {
            foreach (CVertice n in nodos)
            {
                n.Visitado = false;
                n.Padre = null;
                n.distancianodo = int.MaxValue;
                n.pesoasignado = false;
            }
        }

        // Devuelve el grafo a sus colores normales.
        public void ReestablecerGrafo()
        {
            Desmarcar();

            foreach (CVertice nodo in nodos)
            {
                nodo.Color = Color.FromArgb(0, 87, 183);
                nodo.FontColor = Color.White;

                foreach (CArco arco in nodo.ListaAdyacencia)
                {
                    arco.grosor_flecha = 2;
                    arco.color = Color.FromArgb(65, 65, 65);
                }
            }
        }

        // Resalta un camino en el mapa. Como el grafo es bidireccional, se actualizan ambos sentidos.
        public void ResaltarArista(CVertice origen, CVertice destino, Color color, float grosor)
        {
            CArco ida = ObtenerArco(origen, destino);

            CArco vuelta = ObtenerArco(destino, origen);

            if (ida != null)
            {
                ida.color = color;
                ida.grosor_flecha = grosor;
            }

            if (vuelta != null)
            {
                vuelta.color = color;
                vuelta.grosor_flecha = grosor;
            }
        }

        // BFS: usa una cola para visitar primero los vecinos más cercanos.
        public List<CVertice> RecorridoAnchura(CVertice inicio)
        {
            List<CVertice> orden = new List<CVertice>();

            if (inicio == null)
                return orden;

            Desmarcar();

            // En BFS la cola controla el orden por niveles.
            Queue<CVertice> cola = new Queue<CVertice>();

            inicio.Visitado = true;
            cola.Enqueue(inicio);

            while (cola.Count > 0)
            {
                CVertice actual = cola.Dequeue();

                orden.Add(actual);

                foreach (CArco arco in actual.ListaAdyacencia)
                {
                    if (!arco.nDestino.Visitado)
                    {
                        arco.nDestino.Visitado = true;
                        arco.nDestino.Padre = actual;
                        cola.Enqueue(arco.nDestino);
                    }
                }
            }

            return orden;
        }

        // DFS: se va por una rama hasta donde pueda y luego regresa.
        public List<CVertice> RecorridoProfundidad(CVertice inicio)
        {
            List<CVertice> orden = new List<CVertice>();

            if (inicio == null)
                return orden;

            Desmarcar();

            // Aquí empieza la exploración recursiva.
            RecorridoProfundidadRecursivo(inicio, orden);

            return orden;
        }

        // Parte recursiva del recorrido en profundidad.
        private void RecorridoProfundidadRecursivo(CVertice actual, List<CVertice> orden)
        {
            actual.Visitado = true;

            orden.Add(actual);

            foreach (CArco arco in actual.ListaAdyacencia)
            {
                if (!arco.nDestino.Visitado)
                {
                    arco.nDestino.Padre = actual;
                    RecorridoProfundidadRecursivo(arco.nDestino, orden);
                }
            }
        }

        // Dijkstra: calcula la ruta de menor costo tomando en cuenta los km de cada camino.
        public List<CVertice> Dijkstra(CVertice inicio, CVertice fin)
        {
            if (inicio == null || fin == null)
                return null;

            Desmarcar();

            // El origen siempre inicia con costo 0.
            inicio.distancianodo = 0;

            CVertice actual = inicio;

            while (actual != null)
            {
                foreach (CArco arco in actual.ListaAdyacencia)
                {
                    CVertice vecino = arco.nDestino;

                    if (actual.distancianodo == int.MaxValue)
                        continue;

                    // Se prueba si conviene llegar al vecino pasando por el nodo actual.
                    int nuevaDistancia = actual.distancianodo + arco.peso;

                    if (!vecino.pesoasignado && nuevaDistancia < vecino.distancianodo)
                    {
                        vecino.distancianodo = nuevaDistancia;
                        vecino.Padre = actual;
                    }
                }

                actual.pesoasignado = true;

                actual = null;
                int minDist = int.MaxValue;
                foreach (CVertice nodo in nodos)
                {
                    if (!nodo.pesoasignado && nodo.distancianodo < minDist)
                    {
                        minDist = nodo.distancianodo;
                        actual = nodo;
                    }
                }
            }

            if (fin.distancianodo == int.MaxValue)
                return null;

            // Al final se reconstruye la ruta siguiendo los padres hacia atrás.
            List<CVertice> ruta = new List<CVertice>();
            CVertice paso = fin;
            while (paso != null)
            {
                ruta.Insert(0, paso);
                paso = paso.Padre;
            }

            return ruta;
        }

        // Dibuja primero los caminos y después los municipios, para que los nodos queden encima.
        public void DibujarGrafo(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            HashSet<string> aristasDibujadas = new HashSet<string>();

            foreach (CVertice origen in nodos)
            {
                foreach (CArco arco in origen.ListaAdyacencia)
                {
                    string clave = ClaveArista(origen.Valor, arco.nDestino.Valor);

                    if (aristasDibujadas.Contains(clave))
                        continue;

                    DibujarArista(g, origen, arco.nDestino, arco);

                    aristasDibujadas.Add(clave);
                }
            }

            foreach (CVertice nodo in nodos)
                nodo.DibujarVertice(g);
        }

        // Dibuja una línea del grafo y coloca su distancia en km al centro.
        private void DibujarArista(Graphics g, CVertice origen, CVertice destino, CArco arco)
        {
            using (Pen lapiz = new Pen(arco.color, arco.grosor_flecha))
            {
                lapiz.StartCap = LineCap.Round;
                lapiz.EndCap = LineCap.Round;
                g.DrawLine(lapiz, origen.Posicion, destino.Posicion);
            }

            int medioX = (origen.Posicion.X + destino.Posicion.X) / 2;
            int medioY = (origen.Posicion.Y + destino.Posicion.Y) / 2;

            string textoPeso = arco.peso + " km";

            using (Font fuente = new Font("Segoe UI", 7, FontStyle.Bold))
            {
                SizeF medida = g.MeasureString(textoPeso, fuente);
                RectangleF rect = new RectangleF(
                    medioX - medida.Width / 2 - 2,
                    medioY - medida.Height / 2 - 2,
                    medida.Width + 4,
                    medida.Height + 3);

                using (SolidBrush fondo = new SolidBrush(Color.FromArgb(230, Color.White)))
                    g.FillRectangle(fondo, rect);

                using (SolidBrush texto = new SolidBrush(Color.FromArgb(10, 60, 150)))
                    g.DrawString(textoPeso, fuente, texto, rect.X + 2, rect.Y + 1);
            }
        }

        // Clave sencilla para no dibujar dos veces una misma carretera.
        private string ClaveArista(string a, string b)
        {
            return string.Compare(a, b, StringComparison.Ordinal) < 0 ? a + "|" + b : b + "|" + a;
        }
    }
}
