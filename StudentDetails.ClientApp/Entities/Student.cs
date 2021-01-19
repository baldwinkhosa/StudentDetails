using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetails.ClientApp.Entities
{
   public class StudentModel
   {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CellNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
    }
}
