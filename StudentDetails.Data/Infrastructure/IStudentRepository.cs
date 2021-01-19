using StudentDetails.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StudentDetails.Data.Infrastructure
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetStudentFromUserName(Expression<Func<Student, bool>> where);
    }
}
