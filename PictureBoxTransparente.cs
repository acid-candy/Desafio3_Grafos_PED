using System.Drawing;
using System.Windows.Forms;

namespace RutaCorta
{
    // PictureBox modificado para que los PNG se vean con transparencia.
    // Lo usé para evitar que imágenes como el torogoz o el maquilishuat
    // aparezcan con un cuadro blanco encima del diseño.
    public class PictureBoxTransparente : PictureBox
    {
        public PictureBoxTransparente()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parametros = base.CreateParams;

                // WS_EX_TRANSPARENT ayuda a que se pinte primero lo que está debajo.
                parametros.ExStyle |= 0x20;
                return parametros;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // No se pinta fondo para mantener la transparencia del PNG.
        }
    }
}
