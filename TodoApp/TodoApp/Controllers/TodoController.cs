using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDbContext context;

        public TodoController(AppDbContext context)
        {
            this.context = context;
        }


        //Search to option
        public async Task<IActionResult> Index(string SearchQuery)
        {
            var task = string.IsNullOrEmpty(SearchQuery)
                ? await context.TodoItems.ToListAsync()
                : await context.TodoItems
                .Where(x => EF.Functions.Like(x.Title, $"%{SearchQuery}%"))
                .ToListAsync();

            ViewData["SearchQuery"] = SearchQuery;


            //var todoItems = await context.TodoItems.ToListAsync();
            return View(task);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title")] Todoitem todoitem)
        {
            todoitem.IsCompleted = false;
            if (ModelState.IsValid)
            {
                context.TodoItems.Add(todoitem);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoitem);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todoitem = await context.TodoItems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }
            return View(todoitem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IsCompleted")] Todoitem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Update(todoItem);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(todoItem);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todoitem = await context.TodoItems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }
            return View(todoitem);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                context.TodoItems.Remove(todoItem);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
