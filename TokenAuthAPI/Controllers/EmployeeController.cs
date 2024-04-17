using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthAPI.Data;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        [Authorize(Roles = ("User"))]
        public HttpResponseMessage GetEmployeeById(int id)
        {
           var user = dbContext.Employees.FirstOrDefault(e => e.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }


        [Authorize(Roles = ("Admin, SupperAdmin"))]
        [Route("api/Employee/GetSomeEmployees")]
        public HttpResponseMessage GetSomeEmployees()
        {
            var user = dbContext.Employees.Where(e=>e.Id <= 10);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }


        [Authorize(Roles = ("SupperAdmin"))]
        [Route("api/Employee/GetEmployees")]
        public HttpResponseMessage GetEmployees()
        {
            var user = dbContext.Employees.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

       
       //______________________________________________________________________________________
       
        
        [HttpPost]
        [Authorize(Roles = "Admin, SupperAdmin")]
        public HttpResponseMessage CreateEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, employee);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, SupperAdmin")]
        public HttpResponseMessage UpdateEmployee(int id, Employee updatedEmployee)
        {
            var existingEmployee = dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = updatedEmployee.FirstName;
                existingEmployee.LastName = updatedEmployee.LastName;
                // Update other properties as needed
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, SupperAdmin")]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            var employeeToDelete = dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete != null)
            {
                dbContext.Employees.Remove(employeeToDelete);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

    }
}











/*using System.Linq;
using System.Net;
using System.Web.Http;
using TokenAuthAPI.Data;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Roles = "User")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [Authorize(Roles = "Admin, SupperAdmin")]
        [Route("api/Employee/GetSomeEmployees")]
        public IHttpActionResult GetSomeEmployees()
        {
            var employees = _dbContext.Employees.Where(e => e.Id <= 10).ToList();
            return Ok(employees);
        }

        [Authorize(Roles = "SupperAdmin")]
        [Route("api/Employee/GetEmployees")]
        public IHttpActionResult GetEmployees()
        {
            var employees = _dbContext.Employees.ToList();
            return Ok(employees);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SupperAdmin")]
        public IHttpActionResult CreateEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, SupperAdmin")]
        public IHttpActionResult UpdateEmployee(int id, Employee updatedEmployee)
        {
            var existingEmployee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
                return NotFound();

            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.LastName = updatedEmployee.LastName;
            // Update other properties as needed
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, SupperAdmin")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employeeToDelete = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete == null)
                return NotFound();

            _dbContext.Employees.Remove(employeeToDelete);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
*/