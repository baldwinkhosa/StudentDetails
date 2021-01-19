using Ninject.Modules;
using StudentDetails.BusinessLibrary.Services;
using StudentDetails.Data.Infrastructure;
using StudentDetails.Data.Repository;
using StudentDetails.ServicePlatform.ExternalContracts;

namespace StudentDetails.ClientApp.IOC
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDatabaseFactory>().To<DatabaseFactory>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IStudentService>().To<StudentService>();
        }
    }
}
