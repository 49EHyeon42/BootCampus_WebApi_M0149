using System.Diagnostics;
using Biz.Configs;
using Contract.Responses;
using Dac.Daos;
using Microsoft.Data.SqlClient;

namespace Biz.Services
{
    public class WorkExperienceService(DatabaseConfig databaseConfig, WorkExperienceDao workExperienceDao)
    {
        private readonly string _dbConnectionString = databaseConfig.ConnectionString;
        private readonly WorkExperienceDao _workExperienceDao = workExperienceDao;

        public void SaveWorkExperienceByEmployeeId(int employeeId, DateTime hireDate, DateTime? leaveDate, string? description)
        {
            using var connection = new SqlConnection(_dbConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                _workExperienceDao.InsertWorkExperience(connection, transaction,
                    employeeId, hireDate, leaveDate, description);

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
            using var connection = new SqlConnection(_dbConnectionString);

            // TODO: 메서드 복수형으로 변경 필요
            return _workExperienceDao.SelectWorkExperienceByEmployeeId(connection, employeeId).Select(dto => new RsFindWorkExperience
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
            using var connection = new SqlConnection(_dbConnectionString);
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
            using var connection = new SqlConnection(_dbConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                // TODO: 단일 삭제로 수정 필요, 직원 id가 아닌 경력사항 id로 삭제 필요
                _workExperienceDao.DeleteWorkExperienceByEmployeeId(connection, transaction, id);

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
