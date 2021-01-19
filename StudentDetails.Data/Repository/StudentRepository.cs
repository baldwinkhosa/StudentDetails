using StudentDetails.Data.Infrastructure;
using StudentDetails.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StudentDetails.Data.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) { }

        public Student GetStudentFromUserName(Expression<Func<Student, bool>> where)
        {
            return DbContext.Set<Student>().Single(where);
        }
    }
}
