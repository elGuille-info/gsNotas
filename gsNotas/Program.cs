
using System;
using System.Drawing;
using System.Windows.Forms;

namespace gsNotas
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Usar este código SOLAMENTE si nunca se quiere que haya más de una instancia.

            // No permitir más de una instancia en ejecución. (19/oct/22 08.12)
            System.Threading.Mutex mut = new System.Threading.Mutex(false, Application.ProductName);
            bool running = !mut.WaitOne(0, false);
            if (running)
            {
                Application.ExitThread();
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal());
        }

        /// <summary>
        /// El ancho y alto según el diseñador.
        /// </summary>
        /// <param name="width">Se multiplica por 1.666 (10/6)</param>
        /// <param name="height">Se multiplica por 1.923 (25/13)</param>
        /// <remarks>En .NET Framework es 6,13 en .NET 6 es 10,25</remarks>
        public static Size SizeNet(int width, int height)
        {
            return new Size((int)(width * 1.6666), (int)(height * 1.923));
        }

        public static Point PointNet(int width, int height)
        {
            return new Point((int)(width * 1.6666), (int)(height * 1.923));
        }

    }
}