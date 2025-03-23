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
        public ActionResult<List<RsFindEmployee>> FindAllEmployee()
        {
            var userName = _userStorage.GetName();

            var employeeDtos = _employeeService.FindAllEmployees();

            // TODO: 로그

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
        public IActionResult SaveEmployee(RqSaveEmployee body)
        {
            var userName = _userStorage.GetName();

            _employeeService.SaveEmployee(body.Name, body.Age, body.Address, body.PhoneNumber);

            // TODO: 로그

            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<RsFindEmployee> FindEmployee(int id)
        {
            var userName = _userStorage.GetName();

            var employeeDto = _employeeService.FindEmployeeById(id);

            // TODO: 로그

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
        public IActionResult UpdateEmployee(int id, RqUpdateEmployee body)
        {
            var userName = _userStorage.GetName();

            _employeeService.UpdateEmployeeById(id, body.Name, body.Age, body.Address, body.PhoneNumber);

            // TODO: 로그

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var userName = _userStorage.GetName();

            _employeeService.DeleteEmployeeById(id);

            // TODO: 로그

            return Ok();
        }
    }
}
