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
    }
}
