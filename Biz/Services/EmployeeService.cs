using Biz.Configs;
using Common.Exceptions;
using Contract.Dac;
using Contract.Dtos;
using Contract.Biz;
using Microsoft.Extensions.Logging;

namespace Biz.Services
{
    public class EmployeeService(ILogger<EmployeeService> logger, IDatabaseConfig databaseConfig, IEmployeeDao employeeDao, IWorkExperienceDao workExperienceDao) : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger = logger;
        private readonly IDatabaseConfig _databaseConfig = databaseConfig;
        private readonly IEmployeeDao _employeeDao = employeeDao;
        private readonly IWorkExperienceDao _workExperienceDao = workExperienceDao;

        public void SaveEmployee(string name, int age, string address, string phoneNumber)
        {
            using var connection = _databaseConfig.GetDbConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                _employeeDao.InsertEmployee(connection, transaction, name, age, address, phoneNumber);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("EmployeeService:SaveEmployee: {ExceptionMessage}", exception.Message);

                transaction.Rollback();

                throw;
            }
        }

        public List<EmployeeDto> FindAllEmployees()
        {
            using var connection = _databaseConfig.GetDbConnection();

            return _employeeDao.SelectAllEmployee(connection);
        }

        public EmployeeDto FindEmployeeById(int id)
        {
            using var connection = _databaseConfig.GetDbConnection();

            return _employeeDao.SelectEmployeeById(connection, id)
                ?? throw new EmployeeNotFoundException();
        }

        public void UpdateEmployeeById(int id, string name, int age, string address, string phoneNumber)
        {
            using var connection = _databaseConfig.GetDbConnection();
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
                _logger.LogInformation("EmployeeService:UpdateEmployeeById: {ExceptionMessage}", exception.Message);

                transaction.Rollback();

                throw;
            }
        }

        public void DeleteEmployeeById(int id)
        {
            using var connection = _databaseConfig.GetDbConnection();
            connection.Open();

            if (_employeeDao.SelectEmployeeById(connection, id) is null)
            {
                throw new EmployeeNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.DeleteWorkExperienceByEmployeeId(connection, transaction, id);
                _employeeDao.DeleteEmployeeById(connection, transaction, id);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("EmployeeService:DeleteEmployeeById: {ExceptionMessage}", exception.Message);

                transaction.Rollback();

                throw;
            }
        }
    }
}
