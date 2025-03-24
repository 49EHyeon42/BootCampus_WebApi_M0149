using System.Data;
using Contract.Dtos;

namespace Contract.Dac
{
    public interface IEmployeeDao
    {
        Task InsertEmployeeAsync(IDbConnection connection, IDbTransaction transaction, string name, int age, string address, string phoneNumber);
        Task<EmployeeDto?> SelectEmployeeByIdAsync(IDbConnection connection, int id);
        Task<List<EmployeeDto>> SelectAllEmployeeAsync(IDbConnection connection);
        Task UpdateEmployeeByIdAsync(IDbConnection connection, IDbTransaction transaction, int id, string name, int age, string address, string phoneNumber);
        Task DeleteEmployeeByIdAsync(IDbConnection connection, IDbTransaction transaction, int id);
    }
}
