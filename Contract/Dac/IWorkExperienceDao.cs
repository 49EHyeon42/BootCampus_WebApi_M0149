using System.Data;
using Contract.Dtos;

namespace Contract.Dac
{
    public interface IWorkExperienceDao
    {
        Task InsertWorkExperienceAsync(IDbConnection connection, IDbTransaction transaction, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);
        Task<WorkExperienceDto?> SelectWorkExperienceByIdAndEmployeeIdAsync(IDbConnection connection, int id, int employeeId);
        Task<List<WorkExperienceDto>> SelectAllWorkExperienceByEmployeeIdAsync(IDbConnection connection, int employeeId);
        Task UpdateWorkExperienceByIdAsync(IDbConnection connection, IDbTransaction transaction, int id, DateTime hireDate, DateTime? leaveDate, string? description);
        Task DeleteWorkExperienceByIdAsync(IDbConnection connection, IDbTransaction transaction, int id);
        Task DeleteWorkExperienceByEmployeeIdAsync(IDbConnection connection, IDbTransaction transaction, int employeeId);
    }
}
