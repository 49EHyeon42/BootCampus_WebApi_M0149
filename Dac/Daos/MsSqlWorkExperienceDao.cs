using System.Data;
using Contract.Dac;
using Contract.Dtos;
using Dapper;

namespace Dac.Daos
{
    public class MsSqlWorkExperienceDao : IWorkExperienceDao
    {
        public void InsertWorkExperience(IDbConnection connection, IDbTransaction transaction, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            string query = @"
                INSERT INTO WorkExperience (EmployeeId, HireDate, LeaveDate, Description)
                VALUES (@EmployeeId, @HireDate, @LeaveDate, @Description);
            ";

            connection.Execute(query, new
            {
                EmployeeId = employeeId,
                HireDate = hireDate,
                LeaveDate = leaveDate,
                Description = description
            }, transaction);
        }

        public List<WorkExperienceDto> SelectWorkExperienceByEmployeeId(IDbConnection connection, int employeeId)
        {
            string query = @"
                SELECT *
                FROM WorkExperience
                WHERE EmployeeId = @EmployeeId;
            ";

            return connection.Query<WorkExperienceDto>(query, new
            {
                EmployeeId = employeeId
            }).AsList();
        }

        public void UpdateWorkExperienceById(IDbConnection connection, IDbTransaction transaction, int id, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            string query = @"
                UPDATE WorkExperience
                SET HireDate = @HireDate, LeaveDate = @LeaveDate,
                    Description = @Description, LastModifiedDate = @LastModifiedDate
                WHERE Id = @Id;
            ";

            connection.Execute(query, new
            {
                Id = id,
                HireDate = hireDate,
                LeaveDate = leaveDate,
                Description = description,
                LastModifiedDate = DateTime.Now
            }, transaction);
        }

        public void DeleteWorkExperienceById(IDbConnection connection, IDbTransaction transaction, int id)
        {
            string query = @"
                DELETE FROM WorkExperience
                WHERE Id = @Id;
            ";

            connection.Execute(query, new
            {
                Id = id
            }, transaction);
        }
    }
}
