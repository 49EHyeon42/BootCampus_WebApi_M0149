using System.Data;
using Contract.Dtos;

namespace Contract.Dac
{
    public interface IEmployeeDao
    {
        /// <summary>직원 저장 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns>
        Task InsertEmployeeAsync(IDbConnection connection, IDbTransaction transaction, string name, int age, string address, string phoneNumber);

        /// <summary>직원 식별자를 통해 직원 조회 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <param name="connection">데이터베이스 연결</param>
        /// <returns><see cref="Task{EmployeeDto?}"/></returns>
        Task<EmployeeDto?> SelectEmployeeByIdAsync(IDbConnection connection, int id);

        /// <summary>직원 목록 조회 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <returns><see cref="Task{List{EmployeeDto}}"/></returns>
        Task<List<EmployeeDto>> SelectAllEmployeeAsync(IDbConnection connection);

        /// <summary>직원 식별자를 통해 직원 갱신 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">직원 식별자</param>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateEmployeeByIdAsync(IDbConnection connection, IDbTransaction transaction, int id, string name, int age, string address, string phoneNumber);

        /// <summary>직원 식별자를 통해 직원 삭제 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteEmployeeByIdAsync(IDbConnection connection, IDbTransaction transaction, int id);
    }
}
