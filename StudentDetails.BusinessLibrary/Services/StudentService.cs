using StudentDetails.Data.Infrastructure;
using StudentDetails.Domain.Model;
using StudentDetails.ServicePlatform.ExternalContracts;
using System;
using System.Collections.Generic;

namespace StudentDetails.BusinessLibrary.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public int CreateStudent(Student student)
        {
            try
            {
                if(student != null)
                {
                    bool userExists = _unitOfWork.StudentRepository.Exists(u => u.Name == student.Name && u.LastName == student.LastName && u.DOB == student.DOB);

                    if (userExists)
                    {
                        return 0;
                    }
                }

                _unitOfWork.StudentRepository.Insert(student);
                _unitOfWork.Commit();
            }
            catch(Exception)
            {
                throw;
            }
            
            return student.Id;
        }

        public void DeleteStudent(int studentId)
        {
            try
            {
                var stud = SelectStudentById(studentId);

                if (stud != null)
                {
                    _unitOfWork.StudentRepository.Delete(stud);
                }
            }
            catch (Exception)
            {
                throw;
            }  
        }

        public Student Exists(Student student)
        {
            try
            {
                bool validUser = _unitOfWork.StudentRepository.Exists(u => u.Name == student.Name && u.LastName == student.LastName && u.DOB == student.DOB);
                return validUser ? _unitOfWork.StudentRepository.GetUserFromUserName(u => u.Name == student.Name && u.LastName == student.LastName && u.DOB == student.DOB) : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Student> SelectAllStudents()
        {
            return _unitOfWork.StudentRepository.GetAll();
        }

        public Student SelectStudentById(int studentId)
        {
            try
            {
                return _unitOfWork.StudentRepository.Single(studentId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                if (student != null)
                {
                    _unitOfWork.StudentRepository.Update(student);
                    _unitOfWork.Commit();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
