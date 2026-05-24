using System;
using System.Windows.Forms;

namespace RutaCorta
{
    // Punto de entrada del programa. Aquí solo se arranca la aplicación
    // y se abre el formulario principal del simulador.
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Se muestra la ventana donde está todo el simulador.
            Application.Run(new Simulador());
        }
    }
}
