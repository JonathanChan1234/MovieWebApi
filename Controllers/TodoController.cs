using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetApi.Data;
using NetApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace NetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly MovieContext _context;

        public TodoController(ILogger<TodoController> logger, MovieContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoAsync()
        {
            var todoItems = await _context.TodoItems.OrderBy(todo => todo.id).ToListAsync();
            return todoItems;
        }

        [HttpGet("{todoId}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int todoId)
        {
            var todoItem = await _context.TodoItems.FindAsync(todoId);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        [Route("getTodoTest")]
        public ActionResult<TodoItem> GetTodoTest([FromBody] object content)
        {
            Console.WriteLine($"New content is {content.ToString()}");
            var json = JsonConvert.SerializeObject(content);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            foreach (KeyValuePair<string, string> item in dictionary)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTodoItem", new { todoId = todoItem.id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.id)
            {
                return BadRequest();
            }
            _context.Entry(todoItem).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _context.Entry(todoItem).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return todoItem;
        }

        public bool TodoItemExist(int id)
        {
            return _context.TodoItems.Any(todo => todo.id == id);
        }
    }
}
