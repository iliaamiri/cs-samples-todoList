using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using todoList.DataAccessLayer;
using todoList.Models;

namespace todoList.Controllers
{
    public class TodoItemsTempController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoItemsTempController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TodoItemsTemp
        public async Task<IActionResult> Index()
        {
              return _context.TodoItems != null ? 
                          View(await _context.TodoItems.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TodoItems'  is null.");
        }

        // GET: TodoItemsTemp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItemsTemp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItemsTemp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DueDate,IsComplete,CreatedDate,DifficultyLevel")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItemsTemp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItemsTemp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DueDate,IsComplete,CreatedDate,DifficultyLevel")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItemsTemp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItemsTemp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TodoItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TodoItems'  is null.");
            }
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
          return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
