using Contract.Dtos;

namespace Contract.Biz
{
    public interface IWorkExperienceService
    {
        public void SaveWorkExperienceByEmployeeId(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);
        public List<WorkExperienceDto> FindAllWorkExperienceByEmployeeId(int employeeId);
        public void UpdateWorkExperienceByIdAndEmployeeId(int employeeId, int id, DateTime hireDate, DateTime? leaveDate, string? description);
        public void DeleteWorkExperienceByIdAndEmployeeId(int employeeId, int id);
    }
}
