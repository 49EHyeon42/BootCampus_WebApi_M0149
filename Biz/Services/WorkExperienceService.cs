using Biz.Configs;
using Common.Exceptions;
using Contract.Biz;
using Contract.Dac;
using Contract.Dtos;
using Microsoft.Extensions.Logging;

namespace Biz.Services
{
    public class WorkExperienceService(ILogger<WorkExperienceService> logger, IDatabaseConfig databaseConfig, IEmployeeDao employeeDao, IWorkExperienceDao workExperienceDao) : IWorkExperienceService
    {
        private readonly ILogger<WorkExperienceService> _logger = logger;
        private readonly IDatabaseConfig _databaseConfig = databaseConfig;
        private readonly IEmployeeDao _employeeDao = employeeDao;
        private readonly IWorkExperienceDao _workExperienceDao = workExperienceDao;

        public async Task SaveWorkExperienceByEmployeeIdAsync(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            if (await _employeeDao.SelectEmployeeByIdAsync(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await _workExperienceDao.InsertWorkExperienceAsync(connection, transaction, employeeId, hireDate, leaveDate, description);

                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("WorkExperienceService:SaveWorkExperienceByEmployeeIdAsync: {ExceptionMessage}", exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<List<WorkExperienceDto>> FindAllWorkExperienceByEmployeeIdAsync(int employeeId)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            if (await _employeeDao.SelectEmployeeByIdAsync(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

            return await _workExperienceDao.SelectAllWorkExperienceByEmployeeIdAsync(connection, employeeId);
        }

        public async Task UpdateWorkExperienceByIdAndEmployeeIdAsync(int id, int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            if (await _workExperienceDao.SelectWorkExperienceByIdAndEmployeeIdAsync(connection, id, employeeId) is null)
            {
                throw new WorkExperienceNotFoundException();
            }

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await _workExperienceDao.UpdateWorkExperienceByIdAsync(connection, transaction, id, hireDate, leaveDate, description);

                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("WorkExperienceService:UpdateWorkExperienceByIdAndEmployeeIdAsync: {ExceptionMessage}", exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task DeleteWorkExperienceByIdAndEmployeeIdAsync(int id, int employeeId)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            if (await _workExperienceDao.SelectWorkExperienceByIdAndEmployeeIdAsync(connection, id, employeeId) is null)
            {
                throw new WorkExperienceNotFoundException();
            }

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await _workExperienceDao.DeleteWorkExperienceByIdAsync(connection, transaction, id);

                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("WorkExperienceService:DeleteWorkExperienceByIdAndEmployeeIdAsync: {ExceptionMessage}", exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}
