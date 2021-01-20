using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace StudentDetails.WebApp.Models
{
    [Serializable]
    [XmlRoot("Student")]
    public class StudentMetaData
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("LastName")]
        public string LastName { get; set; }
        [XmlElement("CellNumber")]
        public string CellNumber { get; set; }
        [XmlElement("EmailAddress")]
        public string EmailAddress { get; set; }
        [XmlElement("DOB")]
        public DateTime DOB { get; set; }
        [XmlElement("Gender")]
        public string Gender { get; set; }
        [XmlElement("Exported")]
        public bool Exported { get; set; }
        [XmlElement("CreatedDate")]
        public DateTime CreatedDate { get; private set; }
    }

    [MetadataType(typeof(StudentMetaData))]
    public partial class StudentModel
    {
        public string Name { get; internal set; }
        public int Id { get; internal set; }
        public string LastName { get; internal set; }
        public string CellNumber { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string DOB { get; internal set; }
        public string Gender { get; internal set; }
        public string Exported { get; internal set; }
        public string CreatedDate { get; internal set; }
    }
}