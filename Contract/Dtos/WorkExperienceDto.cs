namespace Contract.Dtos
{
    public class WorkExperienceDto : BaseDtos
    {
        public required int Id { get; init; }
        public required int EmployeeId { get; init; }
        public required DateTime HireDate { get; init; }
        public DateTime? LeaveDate { get; init; }
        public string? Description { get; init; }
    }
}
