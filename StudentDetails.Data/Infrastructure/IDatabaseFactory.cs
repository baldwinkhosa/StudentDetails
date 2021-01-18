using System;

namespace StudentDetails.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        StudentDetailsDbContext GetDbContext();
    }
}
