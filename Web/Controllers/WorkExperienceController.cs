using Contract.Biz;
using Contract.Reqeusts;
using Contract.Responses;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes.ExceptionFilters.Controllers;

namespace Web.Controllers
{
    [Route("api/employees/{employeeId}/work_experiences")]
    [ApiController]
    [WorkExperienceControllerExceptionFilter]
    public class WorkExperienceController(UserStorage userStorage, IWorkExperienceService workExperienceService) : ControllerBase
    {
        private readonly UserStorage _userStorage = userStorage;
        private readonly IWorkExperienceService _workExperienceService = workExperienceService;

        [HttpGet]
        public async Task<ActionResult<List<RsFindWorkExperience>>> FindAllWorkExperience(int employeeId)
        {
            var userName = _userStorage.GetName();

            var workExperienceDtos = await _workExperienceService.FindAllWorkExperienceByEmployeeIdAsync(employeeId);

            return Ok(workExperienceDtos.Select(dto => new RsFindWorkExperience
            {
                Id = dto.Id,
                HireDate = dto.HireDate,
                LeaveDate = dto.LeaveDate,
                Description = dto.Description
            }).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> SaveWorkExperience(int employeeId, [FromBody] RqSaveWorkExperience body)
        {
            var userName = _userStorage.GetName();

            await _workExperienceService.SaveWorkExperienceByEmployeeIdAsync(employeeId, body.HireDate, body.LeaveDate, body.Description);

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateWorkExperience(int employeeId, int id, [FromBody] RqUpdateWorkExperience body)
        {
            var userName = _userStorage.GetName();

            await _workExperienceService.UpdateWorkExperienceByIdAndEmployeeIdAsync(employeeId, id, body.HireDate, body.LeaveDate, body.Description);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkExperience(int employeeId, int id)
        {
            var userName = _userStorage.GetName();

            await _workExperienceService.DeleteWorkExperienceByIdAndEmployeeIdAsync(employeeId, id);

            return Ok();
        }
    }
}
