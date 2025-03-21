using Contract.Entity;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Dac
{
    public class WorkExperienceDac
    {
        public int InsertWorkExperience(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            string query = @"
                INSERT INTO WorkExperience (EmployeeId, HireDate, LeaveDate, Description)
                OUTPUT INSERTED.Id
                VALUES (@EmployeeId, @HireDate, @LeaveDate, @Description);
            ";

            int id = sqlConnection.QuerySingle<int>(query, new
            {
                EmployeeId = employeeId,
                HireDate = hireDate,
                LeaveDate = leaveDate,
                Description = description
            }, sqlTransaction);

            return id;
        }

        public IEnumerable<WorkExperienceEntity> SelectWorkExperienceByEmployeeId(SqlConnection sqlConnection,
            int employeeId)
        {
            string query = @"
                SELECT *
                FROM WorkExperience
                WHERE EmployeeId = @EmployeeId;
            ";

            return sqlConnection.Query<WorkExperienceEntity>(query, new
            {
                EmployeeId = employeeId
            }).AsList();
        }

        public void UpdateWorkExperienceById(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int id, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            string query = @"
                UPDATE WorkExperience
                SET HireDate = @HireDate, LeaveDate = @LeaveDate,
                    Description = @Description, LastModifiedDate = @LastModifiedDate
                WHERE Id = @Id;
            ";

            sqlConnection.Execute(query, new
            {
                Id = id,
                HireDate = hireDate,
                LeaveDate = leaveDate,
                Description = description,
                LastModifiedDate = DateTime.Now
            }, sqlTransaction);
        }

        public void DeleteWorkExperienceByEmployeeId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int employeeId)
        {
            string query = @"
                DELETE FROM WorkExperience
                WHERE EmployeeId = @EmployeeId;
            ";

            sqlConnection.Execute(query, new
            {
                EmployeeId = employeeId
            }, sqlTransaction);
        }
    }
}
