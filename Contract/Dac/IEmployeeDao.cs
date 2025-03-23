using System.Data;
using Contract.Dtos;

namespace Contract.Dac
{
    public interface IEmployeeDao
    {
        public void InsertEmployee(IDbConnection connection, IDbTransaction transaction, string name, int age, string address, string phoneNumber);
        public EmployeeDto? SelectEmployeeById(IDbConnection connection, int id);
        public List<EmployeeDto> SelectAllEmployee(IDbConnection connection);
        public void UpdateEmployeeById(IDbConnection connection, IDbTransaction transaction, int id, string name, int age, string address, string phoneNumber);
        public void DeleteEmployeeById(IDbConnection connection, IDbTransaction transaction, int id);
    }
}
