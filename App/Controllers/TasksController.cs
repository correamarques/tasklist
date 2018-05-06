using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using App.Models;

namespace App.Controllers
{
    public class TasksController : ApiController
    {
        private TaskDbContext db = new TaskDbContext();

        // GET: api/Tasks
        public IQueryable<Task> GetTasks()
        {
            return db.Tasks;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.Id)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;
            task.UpdatedAt = DateTime.Now;

            Task oldTask = db.Tasks.Find(id);
            // save deleted date
            if (!oldTask.Deleted && task.Deleted) { task.DeletedAt = DateTime.Now; }
            // save completed at
            if (!oldTask.Completed && task.Completed) { task.CompletedAt = DateTime.Now; }
            if (oldTask.Completed && !task.Completed) { task.CompletedAt = null; }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (task.Completed) { task.CompletedAt = DateTime.Now; }

            db.Tasks.Add(task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            //db.Tasks.Remove(task);
            task.Deleted = true;
            task.DeletedAt = DateTime.Now;
            db.SaveChanges();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.Id == id) > 0;
        }
    }
}