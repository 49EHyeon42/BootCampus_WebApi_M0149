using Contract.Dtos;

namespace Contract.Biz
{
    public interface IWorkExperienceService
    {
        Task SaveWorkExperienceByEmployeeIdAsync(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);
        Task<List<WorkExperienceDto>> FindAllWorkExperienceByEmployeeIdAsync(int employeeId);
        Task UpdateWorkExperienceByIdAndEmployeeIdAsync(int id, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);
        Task DeleteWorkExperienceByIdAndEmployeeIdAsync(int id, int employeeId);
    }
}
