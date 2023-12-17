using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIService.Data;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                List<Employee> list_emps = await _context.Employees.ToListAsync();
                return Ok(list_emps);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetOneEmployees([FromRoute] int id)
        {
            try
            {
                List<Employee> list_emp = await _context.Employees.Where(a => a.Id == id).ToListAsync();
                return Ok(list_emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOneEmployee([FromBody] Employee req_emp)
        {
            try
            {
                await _context.Employees.AddAsync(req_emp);
                await _context.SaveChangesAsync();
                return Ok(req_emp);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOneEmployee([FromBody] Employee req_emp)
        {
            try
            {
                Employee emp = _context.Employees.Where(a => a.Id == req_emp.Id).FirstOrDefault();
                if (emp != null)
                {
                    // emp.Id = req_emp.Id; // Auto Increment Field
                    emp.Name = req_emp.Name;
                    emp.Email = req_emp.Email;
                    emp.Phone = req_emp.Phone;
                    emp.Salary = req_emp.Salary;
                    emp.Department = req_emp.Department;
                    _context.Employees.Update(emp);
                    await _context.SaveChangesAsync();
                    return Ok(req_emp);
                } else
                {
                    return NotFound(req_emp.ToString());
                }
            } catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteOneEmployee([FromRoute] int id)
        {
            try
            {
                Employee emp = await _context.Employees.FindAsync(id);
                if (emp != null)
                {
                    _context.Employees.Remove(emp);
                    await _context.SaveChangesAsync();
                    return Ok(emp);
                }
                return BadRequest("Employee cound not be deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
