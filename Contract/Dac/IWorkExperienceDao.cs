using System.Data;
using Contract.Dtos;

namespace Contract.Dac
{
    public interface IWorkExperienceDao
    {
        public void InsertWorkExperience(IDbConnection connection, IDbTransaction transaction, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);

        public WorkExperienceDto? SelectWorkExperienceByIdAndEmployeeId(IDbConnection connection, int id, int employeeId);

        public List<WorkExperienceDto> SelectAllWorkExperienceByEmployeeId(IDbConnection connection, int employeeId);

        public void UpdateWorkExperienceById(IDbConnection connection, IDbTransaction transaction, int id, DateTime hireDate, DateTime? leaveDate, string? description);

        public void DeleteWorkExperienceById(IDbConnection connection, IDbTransaction transaction, int id);

        public void DeleteWorkExperienceByEmployeeId(IDbConnection connection, IDbTransaction transaction, int employeeId);
    }
}
