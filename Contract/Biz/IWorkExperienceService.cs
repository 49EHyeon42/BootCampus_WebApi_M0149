using Contract.Dtos;

namespace Contract.Biz
{
    public interface IWorkExperienceService
    {
        /// <summary>경력사항 저장 비동기 메서드</summary>
        /// <param name="employeeId">직원 식별자</param>
        /// <param name="hireDate">경력사항 입사일</param>
        /// <param name="leaveDate">경력사항 퇴사일</param>
        /// <param name="description">경력사항 설명</param>
        /// <returns><see cref="Task"/></returns>
        Task SaveWorkExperienceByEmployeeIdAsync(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);

        /// <summary>직원 식별자를 통해 경력사항 목록 조회 비동기 메서드</summary>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task{List{WorkExperienceDto}}"/></returns>
        Task<List<WorkExperienceDto>> FindAllWorkExperienceByEmployeeIdAsync(int employeeId);

        /// <summary>경력사항 식별자, 직원 식별자를 통해 경력사항 갱신 비동기 메서드</summary>
        /// <param name="id">경력사항 식별자</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <param name="hireDate">경력사항 입사일</param>
        /// <param name="leaveDate">경력사항 퇴사일</param>
        /// <param name="description">경력사항 설명</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateWorkExperienceByIdAndEmployeeIdAsync(int id, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);

        /// <summary>경력사항 식별자, 직원 식별자를 통해 경력사항 삭제 비동기 메서드</summary>
        /// <param name="id">경력사항 식별자</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteWorkExperienceByIdAndEmployeeIdAsync(int id, int employeeId);
    }
}
