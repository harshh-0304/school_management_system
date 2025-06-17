using System;
using System.ComponentModel.DataAnnotations; // Add this using directive for [Required] and [Range]

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; } // Primary Key (EF Core recognizes 'Id' as PK by convention)

        [Required(ErrorMessage = "Student name is required.")] // Enforces that Name cannot be null/empty
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")] // Sets max length
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")] // Example range for age
        public int Age { get; set; }

        [Required(ErrorMessage = "Class is required.")]
        [StringLength(50, ErrorMessage = "Class cannot be longer than 50 characters.")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)] // Hints to UI frameworks for date pickers
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Formats for display/edit
        public DateTime DateOfBirth { get; set; }
    }
}