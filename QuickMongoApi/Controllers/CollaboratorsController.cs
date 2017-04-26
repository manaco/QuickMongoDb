using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using QuickMongoApi.Models;
using System.Web.Http.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using QuickMongoApi.DAL;

namespace QuickMongoApi.Controllers
{
    /// <summary>
    /// Controller for Collaborator / Employees
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CollaboratorsController : ApiController
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly MongoContext _context = new MongoContext();
        // GET: Collaboratorss
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IHttpActionResult> Get()
        {
            var collection = _context.Employees;
            var collaborators = new List<Collaborator>();

            await collection.Find(new BsonDocument()).ForEachAsync(d => collaborators.Add(new Collaborator
            {
                Name = d.Firstname,
                Contact = d.Email
            }));

            if (collaborators.Any())
            {
                return Ok(collaborators);
            }

            return NotFound();
        }
        /// <summary>
        /// Posts the specified collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        public IHttpActionResult Post(Collaborator collaborator)
        {
            var collection = _context.Employees;

            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Email = collaborator.Contact,
                    Firstname = collaborator.Name
                };
                collection.InsertOne(employee);
                return Ok(employee.Id);
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Puts the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IHttpActionResult> Put(string email, Collaborator collaborator)
        {

            if (ModelState.IsValid)
            {
                var collection = _context.Employees;
                var filter = Builders<Employee>.Filter.Eq("Email",email);
                var update = Builders<Employee>.Update.Set("Firstname", collaborator.Name);
                update.AddToSet("Email", collaborator.Contact);
                update.CurrentDate("lastModified");
                var result = await collection.UpdateOneAsync(filter,update);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Deletes the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IHttpActionResult> Delete(string email)
        {
            try
            {
                var collection = _context.Employees;
                var filter = Builders<Employee>.Filter.Eq("Email", email);
                return Ok(await collection.DeleteManyAsync(filter));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
           
        }
    }
}