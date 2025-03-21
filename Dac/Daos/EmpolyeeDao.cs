using Contract.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Dac.Daos
{
    public class EmpolyeeDao
    {
        public int InsertEmployee(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name, int age, string address, string phoneNumber)
        {
            string query = @"
                INSERT INTO Employee (Name, Age, Address, PhoneNumber)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Age, @Address, @PhoneNumber);
            ";

            int id = sqlConnection.QuerySingle<int>(query, new
            {
                Name = name,
                Age = age,
                Address = address,
                PhoneNumber = phoneNumber
            }, sqlTransaction);

            return id;
        }

        public EmployeeDto? SelectEmployeeById(SqlConnection sqlConnection,
            int id)
        {
            string query = @"
                SELECT *
                FROM Employee
                WHERE Id = @Id;
            ";

            return sqlConnection.QueryFirstOrDefault<EmployeeDto>(query, new
            {
                Id = id
            });
        }

        public IEnumerable<EmployeeDto> SelectAllEmployee(SqlConnection sqlConnection)
        {
            string query = @"
                SELECT *
                FROM Employee;
            ";

            return sqlConnection.Query<EmployeeDto>(query).AsList();
        }

        public void UpdateEmployeeById(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int id, string name, int age, string address, string phoneNumber)
        {
            string query = @"
                UPDATE Employee
                SET Name = @Name, Age = @Age, Address = @Address,
                    PhoneNumber = @PhoneNumber, LastModifiedDate = @LastModifiedDate
                WHERE Id = @Id;
            ";

            sqlConnection.Execute(query, new
            {
                Id = id,
                Name = name,
                Age = age,
                Address = address,
                PhoneNumber = phoneNumber,
                LastModifiedDate = DateTime.Now
            }, sqlTransaction);
        }

        public void DeleteEmployeeById(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int id)
        {
            string query = @"
                DELETE FROM Employee
                WHERE Id = @Id;
            ";

            sqlConnection.Execute(query, new
            {
                Id = id
            }, sqlTransaction);
        }
    }
}
