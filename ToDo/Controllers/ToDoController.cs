using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Infrastructure;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;

        public ToDoController(ToDoContext context)
        {
            this.context = context;
        }

        //GET

        public async Task<ActionResult> Index()
        {
            IQueryable<Todo> items = from i in context.ToDo orderby i.Id select i;

            List<Todo> todo = await items.ToListAsync();

            return View(todo);
        }

        //GET create todo
        public IActionResult Create() => View();

        //POST Create todo
          [HttpPost]
          [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Todo item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                //display a success message after adding
                TempData["Success"] = "TODO item added successfully";

                //redirect back to index page
                return RedirectToAction("Index");

            }

            return View(item);
        }

        //GET edit todo

        public async Task<ActionResult> Edit(int id)
        {
            Todo item = await context.ToDo.FindAsync(id);
            if(item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //POST edit todo 


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Todo item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();
                //display a success message after editing
                TempData["Success"] = "TODO item updated successfully";

                //redirect back to index page
                return RedirectToAction("Index");

            }

            return View(item);
        }

        //GET delete todo

        public async Task<ActionResult> Delete(int id)
        {
            Todo item = await context.ToDo.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "TODO item does not exist";
            }else
            {
                context.ToDo.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "TODO item successfully deleted";
            }

            //redirect back to index page
            return RedirectToAction("Index");
        }
    }
}
