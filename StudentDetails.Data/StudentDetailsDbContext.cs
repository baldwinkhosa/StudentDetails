using StudentDetails.Domain.Model;
using System.Data.Entity;


namespace StudentDetails.Data
{
    public class StudentDetailsDbContext :  DbContext
    {
        public StudentDetailsDbContext() : base("StudentDB")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StudentDetailsDbContext>());
        }

        public DbSet<Student> Students;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>();

        }

    }
}
