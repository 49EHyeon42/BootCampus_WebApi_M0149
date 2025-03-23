using Biz.Configs;
using Contract.Responses;
using Common.Exceptions;
using Contract.Dac;
using System.Diagnostics;

namespace Biz.Services
{
    // TODO: Response 걷어내기

    public class EmployeeService(DatabaseConfig databaseConfig, IEmployeeDao employeeDao, IWorkExperienceDao workExperienceDao)
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

        public List<RsFindEmployee> FindAllEmployees()
        {
            using var connection = _databaseConfig.GetConnection();

            return _employeeDao.SelectAllEmployee(connection).Select(dto => new RsFindEmployee
            {
                Id = dto.Id,
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                CreatedDate = dto.CreatedDate,
                LastModifiedDate = dto.LastModifiedDate
            }).ToList();
        }

        public RsFindEmployee FindEmployeeById(int id)
        {
            using var connection = _databaseConfig.GetConnection();

            var employeeDto = _employeeDao.SelectEmployeeById(connection, id)
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
            using var connection = _databaseConfig.GetConnection();
            connection.Open();

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
