using Contract.Dtos;

namespace Contract.Biz
{
    public interface IEmployeeService
    {
        Task SaveEmployeeAsync(string name, int age, string address, string phoneNumber);
        Task<List<EmployeeDto>> FindAllEmployeesAsync();
        Task<EmployeeDto> FindEmployeeByIdAsync(int id);
        Task UpdateEmployeeByIdAsync(int id, string name, int age, string address, string phoneNumber);
        Task DeleteEmployeeByIdAsync(int id);
    }
}
