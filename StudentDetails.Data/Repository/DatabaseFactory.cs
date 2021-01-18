using StudentDetails.Data.Infrastructure;
using System;

namespace StudentDetails.Data.Repository
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private StudentDetailsDbContext _dbContext;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public StudentDetailsDbContext GetDbContext()
        {
            return _dbContext ?? (_dbContext = new StudentDetailsDbContext());
        }
    }
}
