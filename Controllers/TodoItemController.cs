using Microsoft.AspNetCore.Mvc;
using todoList.DataAccessLayer;
using todoList.Models;

namespace todoList.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TodoItemController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TodoItem> objTodoItemsList = _db.TodoItems;
            return View(objTodoItemsList);
        }

        // GET
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            if (item.DueDate <= DateTime.Now)
            {
                ModelState.AddModelError("DueDate", "Due date must be later than the current date.");
                return View(item);
            }

            _db.TodoItems.Add(item);
            _db.SaveChanges();
            TempData["success"] = "Todo Item was Created Successfully.";
            return RedirectToAction("Index");
        }

        // POST
        [HttpPost]

        // TODO: Implement CSRF protection for these calls
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsDone(int Id)
        {
            if (Id < 1)
            {
                return NotFound();
            }

            var foundTodoItem = await _db.TodoItems.FindAsync(Id);
            if (foundTodoItem == null)
            {
                return NotFound();
            }

            if (foundTodoItem.IsComplete)
            {
                return Ok();
            }

            foundTodoItem.IsComplete = true;

            _db.SaveChanges();

            return Ok();
        }

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var foundTodoItem = await _db.TodoItems.FindAsync(id);

            if (foundTodoItem == null)
            {
                return NotFound();
            }

            return View(foundTodoItem);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            var foundTodoItem = await _db.TodoItems.FindAsync(item.Id);
            if (foundTodoItem == null)
            {
                TempData["error"] = "Todo Item is not found with this id: " + item.Id;
                return RedirectToAction("Index");
            }

            if (item.DueDate <= DateTime.Now)
            {
                ModelState.AddModelError("DueDate", "Due date must be later than the current date.");
                return View(item);
            }

            foundTodoItem.Title = item.Title;
            foundTodoItem.DueDate = item.DueDate;
            foundTodoItem.DifficultyLevel = item.DifficultyLevel;
            foundTodoItem.IsComplete = item.IsComplete;
            _db.SaveChanges();
            TempData["success"] = "Todo Item was Editted Successfully.";
            return RedirectToAction("Index");
        }

        // GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var foundTodoItem = await _db.TodoItems.FindAsync(id);

            if (foundTodoItem == null)
            {
                return NotFound();
            }

            return View(foundTodoItem);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var foundTodoItem = await _db.TodoItems.FindAsync(id);

            if (foundTodoItem == null)
            {
                return NotFound();
            }

            _db.TodoItems.Remove(foundTodoItem);
            Task<int> deleteFromDbState = _db.SaveChangesAsync();
            TempData["success"] = "Todo Item was Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}
