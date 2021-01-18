using StudentDetails.Domain.Model;
using System.Collections.Generic;

namespace StudentDetails.ServicePlatform.ExternalContracts
{
    public interface IStudentService
    {
        Student SelectStudentById(int studentId);
        IEnumerable<Student> SelectAllStudents();
        int CreateStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        Student Exists(Student student);
    }
}
