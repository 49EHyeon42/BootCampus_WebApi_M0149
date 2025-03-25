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

        /// <summary>직원 저장 비동기 메서드</summary>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns> 
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
                _logger.LogInformation("EmployeeService:SaveEmployee: {ExceptionMessage}", exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }

        /// <summary>직원 식별자를 통해 직원 조회 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task{EmployeeDto}"/></returns>
        /// <exception cref="EmployeeNotFoundException"></exception>
        public async Task<EmployeeDto> FindEmployeeByIdAsync(int id)
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            return await _employeeDao.SelectEmployeeByIdAsync(connection, id)
                ?? throw new EmployeeNotFoundException();
        }

        /// <summary>직원 목록 조회 비동기 메서드</summary>
        /// <returns><see cref="Task{List{EmployeeDto}}"/></returns>
        public async Task<List<EmployeeDto>> FindAllEmployeesAsync()
        {
            await using var connection = _databaseConfig.GetDbConnection();
            await connection.OpenAsync();

            return await _employeeDao.SelectAllEmployeeAsync(connection);
        }

        /// <summary>직원 식별자를 통해 직원 갱신 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="EmployeeNotFoundException"></exception>
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
                _logger.LogInformation("EmployeeService:UpdateEmployeeById: {ExceptionMessage}", exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }

        /// <summary>직원 식별자를 통해 직원 삭제 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="EmployeeNotFoundException"></exception>
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
                _logger.LogInformation("EmployeeService:DeleteEmployeeById: {ExceptionMessage}", exception.Message);

                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}
