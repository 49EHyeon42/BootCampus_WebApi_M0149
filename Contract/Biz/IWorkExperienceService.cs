using Contract.Dtos;

namespace Contract.Biz
{
    public interface IWorkExperienceService
    {
        Task SaveWorkExperienceByEmployeeIdAsync(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);
        Task<List<WorkExperienceDto>> FindAllWorkExperienceByEmployeeIdAsync(int employeeId);
        Task UpdateWorkExperienceByIdAndEmployeeIdAsync(int employeeId, int id, DateTime hireDate, DateTime? leaveDate, string? description);
        Task DeleteWorkExperienceByIdAndEmployeeIdAsync(int employeeId, int id);
    }
}
