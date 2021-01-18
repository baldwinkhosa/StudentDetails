using System;

namespace StudentDetails.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        void Commit();
    }
}
