using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/controller")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDBContext _fullStackDBContext;
        public EmployeesController(FullStackDBContext fullStackDBContext)
        {
            _fullStackDBContext = fullStackDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDBContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _fullStackDBContext.Employees.AddAsync(employeeRequest);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null) {
                return NotFound(); }
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateEmployee([FromRoute] Guid id,Employee updateEmployeeReq)
        {
            var employee = await _fullStackDBContext.Employees.FindAsync(id);
            if (employee == null) {
                return NotFound();
            }
            employee.Name=updateEmployeeReq.Name;
            employee.Email=updateEmployeeReq.Email;
            employee.Phone=updateEmployeeReq.Phone;
            employee.Salary = updateEmployeeReq.Salary;
            employee.Department = updateEmployeeReq.Department;
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDBContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _fullStackDBContext.Employees.Remove(employee);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(employee);
        }


    }
}
