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

        public async Task SaveEmployeeAsync(string name, int age, string address, string phoneNumber)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await _employeeDao.InsertEmployeeAsync(connection, transaction, name, age, address, phoneNumber);

                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("{DateTime} EmployeeService:SaveEmployee: {ExceptionMessage}", DateTime.UtcNow, exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<List<EmployeeDto>> FindAllEmployeesAsync()
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            return await _employeeDao.SelectAllEmployeeAsync(connection);
        }

        public async Task<EmployeeDto> FindEmployeeByIdAsync(int id)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            return await _employeeDao.SelectEmployeeByIdAsync(connection, id)
                ?? throw new EmployeeNotFoundException();
        }

        public async Task UpdateEmployeeByIdAsync(int id, string name, int age, string address, string phoneNumber)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            if (await _employeeDao.SelectEmployeeByIdAsync(connection, id) is null)
            {
                throw new EmployeeNotFoundException();
            }

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await _employeeDao.UpdateEmployeeByIdAsync(connection, transaction, id, name, age, address, phoneNumber);

                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("{DateTime} EmployeeService:UpdateEmployeeById: {ExceptionMessage}", DateTime.UtcNow, exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task DeleteEmployeeByIdAsync(int id)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            if (await _employeeDao.SelectEmployeeByIdAsync(connection, id) is null)
            {
                throw new EmployeeNotFoundException();
            }

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await _workExperienceDao.DeleteWorkExperienceByEmployeeIdAsync(connection, transaction, id);
                await _employeeDao.DeleteEmployeeByIdAsync(connection, transaction, id);

                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("{DateTime} EmployeeService:DeleteEmployeeById: {ExceptionMessage}", DateTime.UtcNow, exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}
