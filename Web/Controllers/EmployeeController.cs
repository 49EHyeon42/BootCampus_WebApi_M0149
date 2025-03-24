using Contract.Biz;
using Contract.Reqeusts;
using Contract.Responses;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes.ExceptionFilters.Controllers;

namespace Web.Controllers
{
    [Route("api/employees")]
    [ApiController]
    [EmployeeControllerExceptionFilter]
    public class EmployeeController(UserStorage userStorage, IEmployeeService employeeService) : ControllerBase
    {
        private readonly UserStorage _userStorage = userStorage;
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet]
        public async Task<ActionResult<List<RsFindEmployee>>> FindAllEmployee()
        {
            var userName = _userStorage.GetName();

            var employeeDtos = await _employeeService.FindAllEmployeesAsync();

            return Ok(employeeDtos.Select(dto => new RsFindEmployee
            {
                Id = dto.Id,
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber
            }).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] RqSaveEmployee body)
        {
            var userName = _userStorage.GetName();

            await _employeeService.SaveEmployeeAsync(body.Name, body.Age, body.Address, body.PhoneNumber);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RsFindEmployee>> FindEmployee(int id)
        {
            var userName = _userStorage.GetName();

            var employeeDto = await _employeeService.FindEmployeeByIdAsync(id);

            return Ok(new RsFindEmployee
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Address = employeeDto.Address,
                Age = employeeDto.Age,
                PhoneNumber = employeeDto.PhoneNumber
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] RqUpdateEmployee body)
        {
            var userName = _userStorage.GetName();

            await _employeeService.UpdateEmployeeByIdAsync(id, body.Name, body.Age, body.Address, body.PhoneNumber);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var userName = _userStorage.GetName();

            await _employeeService.DeleteEmployeeByIdAsync(id);

            return Ok();
        }
    }
}
