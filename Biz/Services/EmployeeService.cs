using Biz.Configs;
using Common.Exceptions;
using Contract.Dac;
using System.Diagnostics;
using Contract.Dtos;
using Contract.Biz;

namespace Biz.Services
{
    public class EmployeeService(DatabaseConfig databaseConfig, IEmployeeDao employeeDao, IWorkExperienceDao workExperienceDao) : IEmployeeService
    {
        private readonly DatabaseConfig _databaseConfig = databaseConfig;
        private readonly IEmployeeDao _employeeDao = employeeDao;
        private readonly IWorkExperienceDao _workExperienceDao = workExperienceDao;

        public void SaveEmployee(string name, int age, string address, string phoneNumber)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                _employeeDao.InsertEmployee(connection, transaction, name, age, address, phoneNumber);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"EmployeeService:SaveEmployee: {exception.Message}");

                transaction.Rollback();

                throw;
            }
        }

        public List<EmployeeDto> FindAllEmployees()
        {
            using var connection = _databaseConfig.GetConnection();

            return _employeeDao.SelectAllEmployee(connection);
        }

        public EmployeeDto FindEmployeeById(int id)
        {
            using var connection = _databaseConfig.GetConnection();

            return _employeeDao.SelectEmployeeById(connection, id)
                ?? throw new EmployeeNotFoundException();
        }

        public void UpdateEmployeeById(int id, string name, int age, string address, string phoneNumber)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            if (_employeeDao.SelectEmployeeById(connection, id) is null)
            {
                throw new EmployeeNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                _employeeDao.UpdateEmployeeById(connection, transaction, id, name, age, address, phoneNumber);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"EmployeeService:UpdateEmployeeById: {exception.Message}");

                transaction.Rollback();

                throw;
            }
        }

        public void DeleteEmployeeById(int id)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            if (_employeeDao.SelectEmployeeById(connection, id) is null)
            {
                throw new EmployeeNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.DeleteWorkExperienceById(connection, transaction, id);
                _employeeDao.DeleteEmployeeById(connection, transaction, id);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"EmployeeService:DeleteEmployeeById: {exception.Message}");

                transaction.Rollback();

                throw;
            }
        }
    }
}
