using System.Diagnostics;
using Biz.Configs;
using Common.Exceptions;
using Contract.Dac;
using Contract.Responses;

namespace Biz.Services
{
    public class WorkExperienceService(DatabaseConfig databaseConfig, IEmployeeDao employeeDao, IWorkExperienceDao workExperienceDao)
    {
        private readonly DatabaseConfig _databaseConfig = databaseConfig;
        private readonly IEmployeeDao _employeeDao = employeeDao;
        private readonly IWorkExperienceDao _workExperienceDao = workExperienceDao;

        public void SaveWorkExperienceByEmployeeId(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            var _ = _employeeDao.SelectEmployeeById(connection, employeeId)
                ?? throw new EmployeeNotFoundException();

            try
            {
                _workExperienceDao.InsertWorkExperience(connection, transaction, employeeId, hireDate, leaveDate, description);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"WorkExperienceService:SaveWorkExperienceByEmployeeId: {exception.Message}");

                transaction.Rollback();

                throw;
            }
        }

        public IEnumerable<RsFindWorkExperience> FindAllWorkExperienceByEmployeeId(int employeeId)
        {
            using var connection = _databaseConfig.GetConnection();

            var _ = _employeeDao.SelectEmployeeById(connection, employeeId)
                ?? throw new EmployeeNotFoundException();

            return _workExperienceDao.SelectAllWorkExperienceByEmployeeId(connection, employeeId).Select(dto => new RsFindWorkExperience
            {
                Id = dto.Id,
                EmployeeId = employeeId,
                HireDate = dto.HireDate,
                LeaveDate = dto.LeaveDate,
                Description = dto.Description
            });
        }

        public void UpdateWorkExperienceById(int id, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.UpdateWorkExperienceById(connection, transaction, id, hireDate, leaveDate, description);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"WorkExperienceService:UpdateWorkExperienceById: {exception.Message}");

                transaction.Rollback();

                throw;
            }
        }

        public void DeleteWorkExperienceById(int id)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.DeleteWorkExperienceById(connection, transaction, id);

                transaction.Commit();
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"WorkExperienceService:DeleteWorkExperienceById: {exception.Message}");

                transaction.Rollback();

                throw;
            }
        }
    }
}
