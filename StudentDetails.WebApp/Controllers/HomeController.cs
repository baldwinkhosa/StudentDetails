using AutoMapper;
using StudentDetails.Domain.Model;
using StudentDetails.ServicePlatform.ExternalContracts;
using StudentDetails.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace StudentDetails.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _StudentService;
        // GET: Home

        public HomeController(IStudentService StudentService)
        {
            _StudentService = StudentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var studentList = _StudentService.SelectAllStudents().ToList();
            return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase xmlFile)
        {
            if (xmlFile.ContentType.Equals("application/xml") || xmlFile.ContentType.Equals("text/xml"))
            {
                var xmlPath = Server.MapPath("~/FileUpload" + xmlFile.FileName);
                xmlFile.SaveAs(xmlPath);
                XDocument xDoc = XDocument.Load(xmlPath);

                List<StudentModel> studentList = xDoc.Descendants("student").Select
                    (student => new StudentModel
                    {
                        Id = Convert.ToInt32(student.Element("Id").Value),
                        Name = student.Element("Name").Value,
                        LastName = student.Element("LastName").Value,
                        CellNumber = student.Element("CellNumber").Value,
                        EmailAddress = student.Element("EmailAddress").Value,
                        DOB = student.Element("DOB").Value,
                        Gender = student.Element("Gender").Value,
                        Exported = student.Element("Exported").Value,
                        CreatedDate = student.Element("CreatedDate").Value

                    }).ToList();

                foreach (var stud in studentList)
                {
                    var result = _StudentService.SelectStudentById(stud.Id);

                    if (result != null)
                    {
                        result.Name = stud.Name;
                        result.LastName = stud.LastName;
                        result.CellNumber = stud.CreatedDate;
                        result.EmailAddress = stud.EmailAddress;
                        result.DOB = Convert.ToDateTime(stud.DOB);
                        result.Gender = stud.Gender;
                        result.Exported = bool.Parse(stud.Exported);
                        _StudentService.UpdateStudent(result);
                    }
                    else
                    {
                        Mapper.CreateMap<StudentModel, Student>();
                        var model = Mapper.Map<Student>(stud);
                        _StudentService.CreateStudent(model);
                    }
                }
                ViewBag.Success = "File uploaded successfully..";
            }
            else
            {
                ViewBag.Error = "Invalid file(Upload xml file only)";
            }
            return View("Index");
        }
    }
}