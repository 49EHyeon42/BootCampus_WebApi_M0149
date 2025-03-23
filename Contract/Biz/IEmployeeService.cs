using Contract.Dtos;

namespace Contract.Biz
{
    public interface IEmployeeService
    {
        public void SaveEmployee(string name, int age, string address, string phoneNumber);
        public List<EmployeeDto> FindAllEmployees();
        public EmployeeDto FindEmployeeById(int id);
        public void UpdateEmployeeById(int id, string name, int age, string address, string phoneNumber);
        public void DeleteEmployeeById(int id);
    }
}
