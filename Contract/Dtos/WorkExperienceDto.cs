namespace Contract.Dtos
{
    public class WorkExperienceDto : BaseDtos
    {
        /// <summary>경력사항 식별자</summary>
        public required int Id { get; init; }
        /// <summary>직원 식별자</summary>
        public required int EmployeeId { get; init; }
        /// <summary>경력사항 입사일</summary>
        public required DateTime HireDate { get; init; }
        /// <summary>경력사항 퇴사일</summary>
        public DateTime? LeaveDate { get; init; }
        /// <summary>경력사항 설명</summary>
        public string? Description { get; init; }
    }
}
