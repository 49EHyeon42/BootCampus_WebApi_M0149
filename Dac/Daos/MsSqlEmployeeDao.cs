using System.Data;
using Contract.Dac;
using Contract.Dtos;
using Dapper;

namespace Dac.Daos
{

    public class MsSqlEmployeeDao : IEmployeeDao
    {
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

        public async Task<List<EmployeeDto>> SelectAllEmployeeAsync(IDbConnection connection)
        {
            string query = @"
                SELECT *
                FROM Employee;
            ";

            var result = await connection.QueryAsync<EmployeeDto>(query);

            return result.AsList();
        }

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
