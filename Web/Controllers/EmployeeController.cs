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
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        /// <summary>직원 목록 조회 비동기 메서드</summary>
        /// <returns><see cref="Task{ActionResult{List{RsFindEmployee}}}"/></returns>
        [HttpGet]
        public async Task<ActionResult<List<RsFindEmployee>>> FindAllEmployee()
        {
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

        /// <summary>직원 저장 비동기 메서드</summary>
        /// <param name="body">직원 저장 요청 본문</param>
        /// <returns><see cref="Task{IActionResult}"/></returns>
        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] RqSaveEmployee body)
        {
            await _employeeService.SaveEmployeeAsync(body.Name, body.Age, body.Address, body.PhoneNumber);

            return Ok();
        }

        /// <summary>직원 식별자를 통해 직원 조회 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task{ActionResult{RsFindEmployee}}"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RsFindEmployee>> FindEmployee(int id)
        {
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

        /// <summary>직원 식별자를 통해 직원 갱신 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <param name="body">직원 갱신 요청 본문</param>
        /// <returns><see cref="Task{IActionResult}"/></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] RqUpdateEmployee body)
        {
            await _employeeService.UpdateEmployeeByIdAsync(id, body.Name, body.Age, body.Address, body.PhoneNumber);

            return Ok();
        }

        /// <summary>직원 식별자를 통해 직원 삭제 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task{IActionResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeByIdAsync(id);

            return Ok();
        }
    }
}
