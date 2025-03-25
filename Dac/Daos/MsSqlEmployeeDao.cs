using System.Data;
using Contract.Dac;
using Contract.Dtos;
using Dapper;

namespace Dac.Daos
{

    public class MsSqlEmployeeDao : IEmployeeDao
    {
        /// <summary>직원 저장 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns>
        public async Task InsertEmployeeAsync(IDbConnection connection, IDbTransaction transaction, string name, int age, string address, string phoneNumber)
        {
            string query = @"
                INSERT INTO Employee (Name, Age, Address, PhoneNumber)
                VALUES (@Name, @Age, @Address, @PhoneNumber);
            ";

            await connection.ExecuteAsync(query, new
            {
                Name = name,
                Age = age,
                Address = address,
                PhoneNumber = phoneNumber
            }, transaction);
        }

        /// <summary>직원 식별자를 통해 직원 조회 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task{EmployeeDto?}"/></returns>
        public async Task<EmployeeDto?> SelectEmployeeByIdAsync(IDbConnection connection, int id)
        {
            string query = @"
                SELECT *
                FROM Employee
                WHERE Id = @Id;
            ";

            return await connection.QueryFirstOrDefaultAsync<EmployeeDto>(query, new
            {
                Id = id
            });
        }

        /// <summary>직원 목록 조회 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <returns><see cref="Task{List{EmployeeDto}}"/></returns>
        public async Task<List<EmployeeDto>> SelectAllEmployeeAsync(IDbConnection connection)
        {
            string query = @"
                SELECT *
                FROM Employee;
            ";

            var result = await connection.QueryAsync<EmployeeDto>(query);

            return result.AsList();
        }

        /// <summary>직원 식별자를 통해 직원 갱신 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">직원 식별자</param>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns>
        public async Task UpdateEmployeeByIdAsync(IDbConnection connection, IDbTransaction transaction, int id, string name, int age, string address, string phoneNumber)
        {
            string query = @"
                UPDATE Employee
                SET Name = @Name, Age = @Age, Address = @Address,
                    PhoneNumber = @PhoneNumber, LastModifiedDate = @LastModifiedDate
                WHERE Id = @Id;
            ";

            await connection.ExecuteAsync(query, new
            {
                Id = id,
                Name = name,
                Age = age,
                Address = address,
                PhoneNumber = phoneNumber,
                LastModifiedDate = DateTime.Now
            }, transaction);
        }

        /// <summary>직원 식별자를 통해 직원 삭제 비동기 메서드</summary>
        /// <param name="connection">데이터베이스 연결</param>
        /// <param name="transaction">데이터베이스 트랜잭션</param>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task"/></returns>
        public async Task DeleteEmployeeByIdAsync(IDbConnection connection, IDbTransaction transaction, int id)
        {
            string query = @"
                DELETE FROM Employee
                WHERE Id = @Id;
            ";

            await connection.ExecuteAsync(query, new
            {
                Id = id
            }, transaction);
        }
    }
}
