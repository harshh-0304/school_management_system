
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Make sure this is included for async operations

namespace SchoolManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Students (Index - List all students)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Class,DateOfBirth")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // --- NEW CODE STARTS HERE ---

        // GET: Students/Details/5
        // Shows details of a single student by ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return 404 if no ID is provided
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id); // Find the student by ID

            if (student == null)
            {
                return NotFound(); // Return 404 if student not found
            }

            return View(student); // Pass the student object to the Details view
        }

        // GET: Students/Edit/5
        // Displays the form to edit an existing student, pre-filled with current data
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id); // Find student by primary key
            if (student == null)
            {
                return NotFound();
            }
            return View(student); // Pass the student object to the Edit view
        }

        // POST: Students/Edit/5
        // Handles the form submission for editing a student
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents Cross-Site Request Forgery
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Class,DateOfBirth")] Student student)
        {
            // Check if the ID in the URL matches the ID in the submitted form data
            if (id != student.Id)
            {
                return NotFound(); // Mismatch suggests tampering or incorrect request
            }

            if (ModelState.IsValid) // Check server-side validation
            {
                try
                {
                    _context.Update(student); // Mark the entity as modified
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException) // Handles concurrency conflicts (e.g., another user edited simultaneously)
                {
                    if (!StudentExists(student.Id)) // Check if the student was deleted by another process
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Re-throw if it's a genuine concurrency issue
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the student list after successful edit
            }
            return View(student); // If validation fails, return to the form with errors
        }

        // GET: Students/Delete/5
        // Displays a confirmation page before deleting a student
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id); // Find the student by ID
            if (student == null)
            {
                return NotFound();
            }

            return View(student); // Pass the student object to the Delete view for confirmation
        }

        // POST: Students/Delete/5 (ActionName("Delete") disambiguates with the GET Delete)
        // Handles the actual deletion of the student after confirmation
        [HttpPost, ActionName("Delete")] // Use ActionName to allow two methods with the same name (Delete) but different HTTP verbs
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id); // Find the student by ID
            if (student != null)
            {
                _context.Students.Remove(student); // Mark the entity for deletion
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            // If student is null (already deleted by another user, etc.), just redirect to index.
            return RedirectToAction(nameof(Index)); // Redirect to the student list after deletion
        }

        // Helper method to check if a student exists (used by Edit POST action)
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        // --- NEW CODE ENDS HERE ---
    }
}