using API_React_CRUD.Data;
using API_React_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_React_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if(_employeeContext.Employee == null)
            {
                return NotFound();
            }
            return await _employeeContext.Employee.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if(_employeeContext.Employee == null)
            {
                return NotFound();
            }
            var employee =  await _employeeContext.Employee.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
          // await _employeeContext.AddAsync(employee);
          _employeeContext.Add(employee);
          await _employeeContext.SaveChangesAsync();
          return CreatedAtAction(nameof(AddEmployee),new {id = employee.ID}, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditEmployee(int id, Employee employee)
        {
            if(id != employee.ID)
            {
                return BadRequest();
            }

            _employeeContext.Employee.Entry(employee).State = EntityState.Modified;
            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if(_employeeContext.Employee == null)
            {
                return NotFound();
            }
            var employee = await _employeeContext.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeContext.Employee.Remove(employee);  
            await _employeeContext.SaveChangesAsync();
            return Ok();
        }
    }
}
 