using StudentDetails.Data.Infrastructure;
using System;

namespace StudentDetails.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private StudentDetailsDbContext _dbContext;
        private readonly IDatabaseFactory _databaseFactory;
        private IStudentRepository _studentRepository;

        public StudentDetailsDbContext DbContext
        {
            get
            {
                return _dbContext ?? (_dbContext = _databaseFactory.GetDbContext());
            }
        }

        public UnitOfWork(IDatabaseFactory databaseFactory) => _databaseFactory = databaseFactory;

        public IStudentRepository StudentRepository
        {
            get
            {
                return _studentRepository ?? (_studentRepository = new StudentRepository(_databaseFactory));
            }
        }
        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
