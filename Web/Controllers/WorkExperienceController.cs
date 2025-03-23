using Biz.Services;
using Contract.Reqeusts;
using Contract.Responses;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes.ExceptionFilters.Controllers;

namespace Web.Controllers
{
    [Route("api/employees/{employeeId}/work_experiences")]
    [ApiController]
    [WorkExperienceControllerExceptionFilter]
    public class WorkExperienceController(UserStorage userStorage, WorkExperienceService workExperienceService) : ControllerBase
    {
        private readonly UserStorage _userStorage = userStorage;
        private readonly WorkExperienceService _workExperienceService = workExperienceService;

        [HttpGet]
        public ActionResult<List<RsFindWorkExperience>> FindAllWorkExperience(int employeeId)
        {
            var userName = _userStorage.GetName();

            var workExperienceDtos = _workExperienceService.FindAllWorkExperienceByEmployeeId(employeeId);

            // TODO: 로그

            return Ok(workExperienceDtos.Select(dto => new RsFindWorkExperience
            {
                Id = dto.Id,
                HireDate = dto.HireDate,
                LeaveDate = dto.LeaveDate,
                Description = dto.Description
            }).ToList());
        }

        [HttpPost]
        public IActionResult SaveWorkExperience(int employeeId, RqSaveWorkExperience body)
        {
            var userName = _userStorage.GetName();

            _workExperienceService.SaveWorkExperienceByEmployeeId(employeeId, body.HireDate, body.LeaveDate, body.Description);

            // TODO: 로그

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateWorkExperience(int employeeId, int id, RqUpdateWorkExperience body)
        {
            var userName = _userStorage.GetName();

            _workExperienceService.UpdateWorkExperienceByIdAndEmployeeId(employeeId, id, body.HireDate, body.LeaveDate, body.Description);

            // TODO: 로그

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkExperience(int employeeId, int id)
        {
            var userName = _userStorage.GetName();

            _workExperienceService.DeleteWorkExperienceByIdAndEmployeeId(employeeId, id);

            // TODO: 로그

            return Ok();
        }
    }
}
