using System.Data;
using Contract.Dac;
using Contract.Dtos;
using Dapper;

namespace Dac.Daos
{
    public class MsSqlWorkExperienceDao : IWorkExperienceDao
    {
        /// <summary>경력사항 저장 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <param name="hireDate">경력사항 입사일</param>
        /// <param name="leaveDate">경력사항 퇴사일</param>
        /// <param name="description">경력사항 설명</param>
        /// <returns><see cref="Task"/></returns>
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

        /// <summary>경력사항 식별자, 직원 식별자를 통해 경력사항 조회 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="id">경력사항 식별자</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task{WorkExperienceDto?}"/></returns>
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

        /// <summary>직원 식별자를 통해 경력사항 목록 조회 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task{List{WorkExperienceDto}}"/></returns>
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

        /// <summary>경력사항 식별자를 통해 경력사항 갱신 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">경력사항 식별자</param>
        /// <param name="hireDate">경력사항 입사일</param>
        /// <param name="leaveDate">경력사항 퇴사일</param>
        /// <param name="description">경력사항 설명</param>
        /// <returns><see cref="Task"/></returns>
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

        /// <summary>경력사항 식별자를 통해 경력사항 삭제 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">경력사항 식별자</param>
        /// <returns><see cref="Task"/></returns>
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

        /// <summary>직원 식별자를 통해 경력사항 삭제 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task"/></returns>
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
