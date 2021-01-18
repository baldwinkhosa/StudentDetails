using Ninject;
using StudentDetails.ClientApp.IOC;
using System;
using System.Windows.Forms;

namespace StudentDetails.ClientApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StandardKernel _kernel = new StandardKernel();
            _kernel.Load(new NinjectBindings());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new  Form1());
        }
    }
}
