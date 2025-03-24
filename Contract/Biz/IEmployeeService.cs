using Contract.Dtos;

namespace Contract.Biz
{
    public interface IEmployeeService
    {
        /// <summary>직원 저장 비동기 메서드</summary>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns>
        Task SaveEmployeeAsync(string name, int age, string address, string phoneNumber);

        /// <summary>직원 목록 조회 비동기 메서드</summary>
        /// <returns><see cref="Task{List{EmployeeDto}}"/></returns>
        Task<List<EmployeeDto>> FindAllEmployeesAsync();

        /// <summary>직원 식별자를 통해 직원 조회 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task{EmployeeDto}"/></returns>
        Task<EmployeeDto> FindEmployeeByIdAsync(int id);

        /// <summary>직원 식별자를 통해 직원 갱신 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <param name="name">직원 이름</param>
        /// <param name="age">직원 나이</param>
        /// <param name="address">직원 주소</param>
        /// <param name="phoneNumber">직원 전화번호</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateEmployeeByIdAsync(int id, string name, int age, string address, string phoneNumber);

        /// <summary>직원 식별자를 통해 직원 삭제 비동기 메서드</summary>
        /// <param name="id">직원 식별자</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteEmployeeByIdAsync(int id);
    }
}
