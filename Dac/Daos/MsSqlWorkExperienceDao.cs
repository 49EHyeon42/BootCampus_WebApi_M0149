using System.Data;
using Contract.Dac;
using Contract.Dtos;
using Dapper;

namespace Dac.Daos
{
    public class MsSqlWorkExperienceDao : IWorkExperienceDao
    {
        public async Task InsertWorkExperienceAsync(IDbConnection connection, IDbTransaction transaction, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            string query = @"
                INSERT INTO WorkExperience (EmployeeId, HireDate, LeaveDate, Description)
                VALUES (@EmployeeId, @HireDate, @LeaveDate, @Description);
            ";

            await connection.ExecuteAsync(query, new
            {
                EmployeeId = employeeId,
                HireDate = hireDate,
                LeaveDate = leaveDate,
                Description = description
            }, transaction);
        }

        public async Task<WorkExperienceDto?> SelectWorkExperienceByIdAndEmployeeIdAsync(IDbConnection connection, int id, int employeeId)
        {
            string query = @"
                SELECT *
                FROM WorkExperience
                WHERE Id = @Id AND EmployeeId = @EmployeeId;
            ";

            return await connection.QuerySingleOrDefaultAsync<WorkExperienceDto>(query, new
            {
                Id = id,
                EmployeeId = employeeId
            });
        }

        public async Task<List<WorkExperienceDto>> SelectAllWorkExperienceByEmployeeIdAsync(IDbConnection connection, int employeeId)
        {
            string query = @"
                SELECT *
                FROM WorkExperience
                WHERE EmployeeId = @EmployeeId;
            ";

            var result = await connection.QueryAsync<WorkExperienceDto>(query, new
            {
                EmployeeId = employeeId
            });

            return result.AsList();
        }

        public async Task UpdateWorkExperienceByIdAsync(IDbConnection connection, IDbTransaction transaction, int id, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            string query = @"
                UPDATE WorkExperience
                SET HireDate = @HireDate, LeaveDate = @LeaveDate,
                    Description = @Description, LastModifiedDate = @LastModifiedDate
                WHERE Id = @Id;
            ";

            await connection.ExecuteAsync(query, new
            {
                Id = id,
                HireDate = hireDate,
                LeaveDate = leaveDate,
                Description = description,
                LastModifiedDate = DateTime.Now
            }, transaction);
        }

        public async Task DeleteWorkExperienceByIdAsync(IDbConnection connection, IDbTransaction transaction, int id)
        {
            string query = @"
                DELETE FROM WorkExperience
                WHERE Id = @Id;
            ";

            await connection.ExecuteAsync(query, new
            {
                Id = id
            }, transaction);
        }

        public async Task DeleteWorkExperienceByEmployeeIdAsync(IDbConnection connection, IDbTransaction transaction, int employeeId)
        {
            string query = @"
                DELETE FROM WorkExperience
                WHERE EmployeeId = @EmployeeId;
            ";

            await connection.ExecuteAsync(query, new
            {
                EmployeeId = employeeId
            }, transaction);
        }
    }
}
