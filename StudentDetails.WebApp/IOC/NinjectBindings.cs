using Ninject.Modules;
using StudentDetails.BusinessLibrary.Services;
using StudentDetails.Data.Infrastructure;
using StudentDetails.Data.Repository;
using StudentDetails.ServicePlatform.ExternalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDetails.WebApp.IOC
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