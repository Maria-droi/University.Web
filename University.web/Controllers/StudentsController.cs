using System;
using System.Linq;
using System.Web.Mvc;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;

namespace University.web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();

        // GET: Students

        [HttpGet]
        public ActionResult Index()
        {
            var query = context.Students.ToList();
            var students = query.Where(x => x.EnrollmentDate < DateTime.Now)
                                .Select(x => new StudentDTO
                                {
                                    ID = x.ID,
                                    LastName = x.LastName,
                                    FirstMidName = x.FirstMidName,
                                    EnrollmentDate = x.EnrollmentDate
                                }).ToList();

            ViewBag.Data = "Data de prueba";
            ViewBag.message = "Mensaje de error";

            return View(students);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentDTO students)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(students);

                if (students.EnrollmentDate > DateTime.Now)
                    throw new Exception("La fecha de matricula no puede ser mayor a la fecha actual");

                context.Students.Add(new Students
                {

                    FirstMidName = students.FirstMidName,
                    LastName = students.LastName,
                    EnrollmentDate = students.EnrollmentDate
                });

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(students);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var students = context.Students.Where(x => x.ID == id)
                                .Select(x => new StudentDTO
                                {
                                    ID = x.ID,
                                    LastName = x.LastName,
                                    FirstMidName = x.FirstMidName,
                                    EnrollmentDate = x.EnrollmentDate
                                }).FirstOrDefault();

            return View(students);
        }
        [HttpPost]
        public ActionResult Edit(StudentDTO students)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(students);

                if (students.EnrollmentDate > DateTime.Now)
                    throw new Exception("La fecha de matricula no puede ser mayor a la fecha actual");

                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var studentModel = context.Students.FirstOrDefault(x => x.ID == students.ID);

                //campos que se van a modificar
                //sobreescribo las propiedades del modelo de base de datos
                studentModel.LastName = students.LastName;
                studentModel.FirstMidName = students.FirstMidName;
                studentModel.EnrollmentDate = students.EnrollmentDate;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(students);
        }
    }
}

