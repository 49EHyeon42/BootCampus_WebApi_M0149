namespace Contract.Reqeusts
{
    public class RqUpdateWorkExperience
    {
        /// <summary>경력사항 입사일</summary>
        public required DateTime HireDate { get; init; }
        /// <summary>경력사항 퇴사일</summary>
        public DateTime? LeaveDate { get; init; }
        /// <summary>경력사항 설명</summary>
        public string? Description { get; init; }
    }
}
