using System.Data;
using Contract.Dtos;
using Dapper;

namespace Dac.Daos
{
    public class EmployeeDao
    {
        public void InsertEmployee(IDbConnection connection, IDbTransaction transaction, string name, int age, string address, string phoneNumber)
        {
            string query = @"
                INSERT INTO Employee (Name, Age, Address, PhoneNumber)
                VALUES (@Name, @Age, @Address, @PhoneNumber);
            ";

            connection.Execute(query, new
            {
                Name = name,
                Age = age,
                Address = address,
                PhoneNumber = phoneNumber
            }, transaction);
        }

        public EmployeeDto? SelectEmployeeById(IDbConnection connection, int id)
        {
            string query = @"
                SELECT *
                FROM Employee
                WHERE Id = @Id;
            ";

            return connection.QueryFirstOrDefault<EmployeeDto>(query, new
            {
                Id = id
            });
        }

        public List<EmployeeDto> SelectAllEmployee(IDbConnection connection)
        {
            string query = @"
                SELECT *
                FROM Employee;
            ";

            return connection.Query<EmployeeDto>(query).AsList();
        }

        public void UpdateEmployeeById(IDbConnection connection, IDbTransaction transaction, int id, string name, int age, string address, string phoneNumber)
        {
            string query = @"
                UPDATE Employee
                SET Name = @Name, Age = @Age, Address = @Address,
                    PhoneNumber = @PhoneNumber, LastModifiedDate = @LastModifiedDate
                WHERE Id = @Id;
            ";

            connection.Execute(query, new
            {
                Id = id,
                Name = name,
                Age = age,
                Address = address,
                PhoneNumber = phoneNumber,
                LastModifiedDate = DateTime.Now
            }, transaction);
        }

        public void DeleteEmployeeById(IDbConnection connection, IDbTransaction transaction, int id)
        {
            string query = @"
                DELETE FROM Employee
                WHERE Id = @Id;
            ";

            connection.Execute(query, new
            {
                Id = id
            }, transaction);
        }
    }
}
