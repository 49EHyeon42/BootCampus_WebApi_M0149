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

        public void SaveWorkExperienceByEmployeeId(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            using var connection = _databaseConfig.GetDbConnection();
            connection.Open();

            if (_employeeDao.SelectEmployeeById(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.InsertWorkExperience(connection, transaction, employeeId, hireDate, leaveDate, description);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("WorkExperienceService:SaveWorkExperienceByEmployeeId: {ExceptionMessage}", exception.Message);

                transaction.Rollback();

                throw;
            }
        }

        public List<WorkExperienceDto> FindAllWorkExperienceByEmployeeId(int employeeId)
        {
            using var connection = _databaseConfig.GetDbConnection();

            if (_employeeDao.SelectEmployeeById(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

            return _workExperienceDao.SelectAllWorkExperienceByEmployeeId(connection, employeeId);
        }

        public void UpdateWorkExperienceByIdAndEmployeeId(int employeeId, int id, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            using var connection = _databaseConfig.GetDbConnection();
            connection.Open();

            if (_workExperienceDao.SelectWorkExperienceByIdAndEmployeeId(connection, id, employeeId) is null)
            {
                throw new WorkExperienceNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.UpdateWorkExperienceById(connection, transaction, id, hireDate, leaveDate, description);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("WorkExperienceService:UpdateWorkExperienceByIdAndEmployeeId: {ExceptionMessage}", exception.Message);

                transaction.Rollback();

                throw;
            }
        }

        public void DeleteWorkExperienceByIdAndEmployeeId(int employeeId, int id)
        {
            using var connection = _databaseConfig.GetDbConnection();
            connection.Open();

            if (_workExperienceDao.SelectWorkExperienceByIdAndEmployeeId(connection, id, employeeId) is null)
            {
                throw new WorkExperienceNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.DeleteWorkExperienceById(connection, transaction, id);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("WorkExperienceService:DeleteWorkExperienceByIdAndEmployeeId: {ExceptionMessage}", exception.Message);

                transaction.Rollback();

                throw;
            }
        }
    }
}
