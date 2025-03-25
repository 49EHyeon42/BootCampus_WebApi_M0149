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
    public class WorkExperienceController(IWorkExperienceService workExperienceService) : ControllerBase
    {
        private readonly IWorkExperienceService _workExperienceService = workExperienceService;

        /// <summary>직원 식별자를 통해 경력사항 목록 조회 비동기 메서드</summary>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task{ActionResult{List{RsFindWorkExperience}}}"/></returns>
        [HttpGet]
        public async Task<ActionResult<List<RsFindWorkExperience>>> FindAllWorkExperience(int employeeId)
        {
            var workExperienceDtos = await _workExperienceService.FindAllWorkExperienceByEmployeeIdAsync(employeeId);

            return Ok(workExperienceDtos.Select(dto => new RsFindWorkExperience
            {
                Id = dto.Id,
                HireDate = dto.HireDate,
                LeaveDate = dto.LeaveDate,
                Description = dto.Description
            }).ToList());
        }

        /// <summary>경력사항 저장 비동기 메서드</summary>
        /// <param name="body">경력사항 저장 요청 본문</param>
        /// <returns><see cref="Task{IActionResult}"/></returns>
        [HttpPost]
        public async Task<IActionResult> SaveWorkExperience(int employeeId, [FromBody] RqSaveWorkExperience body)
        {
            await _workExperienceService.SaveWorkExperienceByEmployeeIdAsync(employeeId, body.HireDate, body.LeaveDate, body.Description);

            return Ok();
        }

        /// <summary>직원 식별자, 경력사항 식별자를 통해 경력사항 갱신 비동기 메서드</summary>
        /// <param name="employeeId">직원 식별자</param>
        /// <param name="id">경력사항 식별자</param>
        /// <param name="body">경력사항 갱신 요청 본문</param>
        /// <returns><see cref="Task{IActionResult}"/></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateWorkExperience(int employeeId, int id, [FromBody] RqUpdateWorkExperience body)
        {
            await _workExperienceService.UpdateWorkExperienceByIdAndEmployeeIdAsync(id, employeeId, body.HireDate, body.LeaveDate, body.Description);

            return Ok();
        }

        /// <summary>직원 식별자, 경력사항 식별자를 통해 경력사항 삭제 비동기 메서드</summary>
        /// <param name="employeeId">직원 식별자</param>
        /// <param name="id">경력사항 식별자</param>
        /// <returns><see cref="Task{IActionResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkExperience(int employeeId, int id)
        {
            await _workExperienceService.DeleteWorkExperienceByIdAndEmployeeIdAsync(id, employeeId);

            return Ok();
        }
    }
}
