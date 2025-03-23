using System.Diagnostics;
using Dac.Daos;
using Microsoft.Data.SqlClient;
using Biz.Configs;
using Contract.Reqeusts;
using Contract.Responses;
using Common.Exceptions;

// TODO: sqlConnection -> connection, sqlTransaction -> transaction

namespace Biz.Services
{
    public class EmployeeService(DatabaseConfig databaseConfig,
        EmployeeDao employeeDao, WorkExperienceDao workExperienceDao)
    {
        private readonly string _dbConnectionString = databaseConfig.ConnectionString;
        private readonly EmployeeDao _employeeDao = employeeDao;
        private readonly WorkExperienceDao _workExperienceDao = workExperienceDao;

        // TODO: 여기 수정
        public void SaveEmployee(string name, int age, string address, string phoneNumber)
        {
            using var sqlConnection = new SqlConnection(_dbConnectionString);
            sqlConnection.Open();

            using var sqlTransaction = sqlConnection.BeginTransaction();

            try
            {
                int employeeId = _employeeDao.InsertEmployee(sqlConnection, sqlTransaction,
                    name, age, address, phoneNumber);

                sqlTransaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"EmployeeService:SaveEmployee: {exception.Message}");

                sqlTransaction.Rollback();

                throw;
            }
        }

        public IEnumerable<RsFindEmployee> FindAllEmployees()
        {
            using var sqlConnection = new SqlConnection(_dbConnectionString);

            var employeeDtos = _employeeDao.SelectAllEmployee(sqlConnection);

            return employeeDtos.Select(dto => new RsFindEmployee
            {
                Id = dto.Id,
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                CreatedDate = dto.CreatedDate,
                LastModifiedDate = dto.LastModifiedDate
            });
        }

        public RsFindEmployee FindEmployeeById(int id)
        {
            using var sqlConnection = new SqlConnection(_dbConnectionString);

            var employeeDto = _employeeDao.SelectEmployeeById(sqlConnection, id)
                ?? throw new EmployeeNotFoundException();

            return new RsFindEmployee
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                PhoneNumber = employeeDto.PhoneNumber,
                CreatedDate = employeeDto.CreatedDate,
                LastModifiedDate = employeeDto.LastModifiedDate
            };
        }

        public void UpdateEmployeeById(int id, string name, int age, string address, string phoneNumber)
        {
            using var sqlConnection = new SqlConnection(_dbConnectionString);
            sqlConnection.Open();

            using var sqlTransaction = sqlConnection.BeginTransaction();

            try
            {
                _employeeDao.UpdateEmployeeById(sqlConnection, sqlTransaction, id, name, age, address, phoneNumber);

                sqlTransaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"EmployeeService:UpdateEmployeeById: {exception.Message}");

                sqlTransaction.Rollback();

                throw;
            }
        }

        public void DeleteEmployeeById(int id)
        {
            using var sqlConnection = new SqlConnection(_dbConnectionString);
            sqlConnection.Open();

            using var sqlTransaction = sqlConnection.BeginTransaction();

            try
            {
                _workExperienceDao.DeleteWorkExperienceByEmployeeId(sqlConnection, sqlTransaction, id);
                _employeeDao.DeleteEmployeeById(sqlConnection, sqlTransaction, id);

                sqlTransaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"EmployeeService:DeleteEmployeeById: {exception.Message}");

                sqlTransaction.Rollback();

                throw;
            }
        }
    }
}
