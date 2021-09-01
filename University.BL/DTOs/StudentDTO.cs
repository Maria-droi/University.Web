using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class StudentDTO{

        [Display(Name = "ID")]
        [Required(ErrorMessage = "EL CAMPO ID ES REQUERIDO")]
        public int ID { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "EL CAMPO LastName ES REQUERIDO")]
        public string LastName { get; set; }

        [Display(Name = "FirstMidName")]
        [Required(ErrorMessage = "EL CAMPO FirstMidName ES REQUERIDO")]
        public string FirstMidName { get; set; }

        [Display(Name = "EnrollmentDate")]
        [Required(ErrorMessage = "EL CAMPO EnrollmentDate ES REQUERIDO")]

        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", LastName, FirstMidName);
            }
        }
    }
}
