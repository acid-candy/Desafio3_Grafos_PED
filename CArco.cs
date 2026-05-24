using System.Drawing;

namespace RutaCorta
{
    // Esta clase representa un camino entre dos municipios.
    // El peso se maneja como la distancia en kilómetros.
    public class CArco
    {
        public int peso;
        public float grosor_flecha;
        public Color color;
        public CVertice nDestino;

        public CArco(CVertice destino) : this(destino, 1)
        {
            // Constructor rápido por si se necesita crear un camino sin indicar peso.
        }

        public CArco(CVertice destino, int peso)
        {
            // Se guarda hacia qué municipio apunta el camino y cuánto cuesta recorrerlo.
            this.nDestino = destino;
            this.peso = peso;

            // Valores normales para dibujar los caminos cuando no están resaltados.
            this.grosor_flecha = 2;
            this.color = Color.FromArgb(65, 65, 65);
        }
    }
}
