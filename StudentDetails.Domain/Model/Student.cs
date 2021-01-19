using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDetails.Domain.Model
{
    [Table("Student", Schema = "dbo")]
    public class Student
    {
        public Student()
        {
            CreatedDate = DateTime.Now;
        }
        [Key]
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
