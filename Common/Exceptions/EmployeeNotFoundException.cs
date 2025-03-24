namespace Common.Exceptions
{
    public class EmployeeNotFoundException : BaseException
    {
        public EmployeeNotFoundException() : base(404, "EMPLOYEE_NOT_FOUND") { }
    }
}
