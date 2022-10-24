using Microsoft.AspNetCore.Mvc;
using TaskSchedulingSystem.Context;
using TaskSchedulingSystem.Models;

namespace TaskSchedulingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ContextOrganizer _context;

        public TaskController(ContextOrganizer context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_context.Tasks.ToList());
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var task = _context.Tasks.Where(x => x.Title.ToUpper() == title.ToUpper());
            return Ok(task);
        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime date)
        {
            var task = _context.Tasks.Where(x => x.Date.Date == date.Date);
            return Ok(task);
        }

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(EnumStatusTask status)
        {
            var task = _context.Tasks.Where(x => x.Status == status);
            return Ok(task);
        }

        [HttpPost]
        public IActionResult Create(Task task)
        {
            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Erro = "Task date cannot be empty" });

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Task task)
        {
            var taskBank = _context.Tasks.Find(id);

            if (taskBank == null)
                return NotFound();

            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Erro = "Task date cannot be empty" });

            taskBank.Id = id;
            taskBank.Title = task.Title;
            taskBank.Description = task.Description;
            taskBank.Date = task.Date;
            taskBank.Status = task.Status;

            _context.Update(taskBank);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskBank = _context.Tasks.Find(id);

            if (taskBank == null)
                return NotFound();

            _context.Tasks.Remove(taskBank);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
