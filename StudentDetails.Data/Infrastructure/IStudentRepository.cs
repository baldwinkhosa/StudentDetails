using StudentDetails.Domain.Model;
using System;
using System.Linq.Expressions;

namespace StudentDetails.Data.Infrastructure
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetUserFromUserName(Expression<Func<Student, bool>> where);
    }
}
