using System.Data;
using Contract.Dtos;

namespace Contract.Dac
{
    public interface IWorkExperienceDao
    {
        /// <summary>경력사항 저장 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <param name="hireDate">경력사항 입사일</param>
        /// <param name="leaveDate">경력사항 퇴사일</param>
        /// <param name="description">경력사항 설명</param>
        /// <returns><see cref="Task"/></returns>
        Task InsertWorkExperienceAsync(IDbConnection connection, IDbTransaction transaction, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description);

        /// <summary>경력사항 식별자, 직원 식별자를 통해 경력사항 조회 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="id">경력사항 식별자</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task{WorkExperienceDto?}"/></returns>
        Task<WorkExperienceDto?> SelectWorkExperienceByIdAndEmployeeIdAsync(IDbConnection connection, int id, int employeeId);

        /// <summary>직원 식별자를 통해 경력사항 목록 조회 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task{List{WorkExperienceDto}}"/></returns>
        Task<List<WorkExperienceDto>> SelectAllWorkExperienceByEmployeeIdAsync(IDbConnection connection, int employeeId);

        /// <summary>경력사항 식별자를 통해 경력사항 갱신 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">경력사항 식별자</param>
        /// <param name="hireDate">경력사항 입사일</param>
        /// <param name="leaveDate">경력사항 퇴사일</param>
        /// <param name="description">경력사항 설명</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateWorkExperienceByIdAsync(IDbConnection connection, IDbTransaction transaction, int id, DateTime hireDate, DateTime? leaveDate, string? description);

        /// <summary>경력사항 식별자를 통해 경력사항 삭제 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">경력사항 식별자</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteWorkExperienceByIdAsync(IDbConnection connection, IDbTransaction transaction, int id);

        /// <summary>직원 식별자를 통해 경력사항 삭제 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="employeeId">직원 식별자</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteWorkExperienceByEmployeeIdAsync(IDbConnection connection, IDbTransaction transaction, int employeeId);
    }
}
