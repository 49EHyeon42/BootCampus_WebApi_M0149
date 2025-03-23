using System.Diagnostics;
using Biz.Configs;
using Common.Exceptions;
using Contract.Biz;
using Contract.Dac;
using Contract.Dtos;

namespace Biz.Services
{
    public class WorkExperienceService(DatabaseConfig databaseConfig, IEmployeeDao employeeDao, IWorkExperienceDao workExperienceDao) : IWorkExperienceService
    {
        private readonly DatabaseConfig _databaseConfig = databaseConfig;
        private readonly IEmployeeDao _employeeDao = employeeDao;
        private readonly IWorkExperienceDao _workExperienceDao = workExperienceDao;

        public void SaveWorkExperienceByEmployeeId(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            if (_employeeDao.SelectEmployeeById(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

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

        public List<WorkExperienceDto> FindAllWorkExperienceByEmployeeId(int employeeId)
        {
            using var connection = _databaseConfig.GetConnection();

            if (_employeeDao.SelectEmployeeById(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

            return _workExperienceDao.SelectAllWorkExperienceByEmployeeId(connection, employeeId);
        }

        public void UpdateWorkExperienceByEmployeeIdAndId(int employeeId, int id, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            if (_employeeDao.SelectEmployeeById(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                // TODO: 수정
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

        public void DeleteWorkExperienceByEmployeeIdAndId(int employeeId, int id)
        {
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

            if (_employeeDao.SelectEmployeeById(connection, employeeId) is null)
            {
                throw new EmployeeNotFoundException();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                // TODO: 수정
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
