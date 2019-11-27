using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AperoBoxApi.Models;
using AperoBoxApi.Context;

namespace AperoBoxApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilisateurController : ControllerBase
    {
        public UtilisateurController()
        {
            ;
        }

        [HttpGet]
        public IEnumerable<Utilisateur> Get()
        {
            //Afficher tout les utilisateurs
            using (AperoBoxApi_dbContext context = new AperoBoxApi_dbContext())
            {
                var utilisateurs = context.Utilisateur
                    .ToList();
                    
                return utilisateurs;
            }
        }


        [HttpPost]
        public void Post([FromBody]Utilisateur utilisateur)
        {
            /*var date = new DateTime(2010, 10, 10);
            var newStudent = new Student{Birthdate = date, Fullname = "NewStudent", Remark = "testAjout"};
            
            var newCourse = new Course{Description = "testNewCours"};

            var newStudentCourse = new StudentCourse{Note = 20, Student = newStudent, Course = newCourse};

            var context = new firstBdContext();
            //context.Student.Add(newStudent);
            //context.Course.Add(newCourse);
            //context.StudentCourse.Add(newStudentCourse);
            context.AddRange(newStudent, newCourse, newStudentCourse);
            context.SaveChanges();*/
        }
    }
}
