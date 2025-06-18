// Models/Teacher.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "Teacher name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(50, ErrorMessage = "Subject cannot be longer than 50 characters.")]
        public string Subject { get; set; }

        [DataType(DataType.Date)] // Hint for UI frameworks
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; } // When the teacher was hired
    }
}