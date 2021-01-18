using System;

namespace StudentDetails.Domain.Model
{
    public class Student
    {
        public Student()
        {
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CellNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; private set; }
    }
}
