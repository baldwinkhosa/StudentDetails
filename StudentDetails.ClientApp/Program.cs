using Ninject;
using SimpleInjector;
using StudentDetails.ClientApp.IOC;
using StudentDetails.Data.Infrastructure;
using StudentDetails.Data.Repository;
using System;
using SimpleInjector.Diagnostics;
using System.Windows.Forms;
using StudentDetails.ServicePlatform.ExternalContracts;
using StudentDetails.BusinessLibrary.Services;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var container = Bootstrap();
            Application.Run(container.GetInstance<Form1>());

        }
        private static Container Bootstrap()
        {
            var container = new Container();

            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Singleton);
            container.Register<IUnitOfWork, UnitOfWork>();
            container.Register<IStudentService, StudentService>();

            AutoRegisterWindowsForms(container);
            container.Verify();

            return container;
        }

        private static void AutoRegisterWindowsForms(Container container)
        {
            var types = container.GetTypesToRegister<Form>(typeof(Program).Assembly);

            foreach (var type in types)
            {
                var registration = Lifestyle.Transient.CreateRegistration(type, container);
                registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent,"Forms should be disposed by app code; not by the container.");

                container.AddRegistration(type, registration);
            }
        }
    }
}
