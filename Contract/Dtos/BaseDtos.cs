namespace Contract.Dtos
{
    public class BaseDtos
    {
        /// <summary>
        /// 생성일시
        /// </summary>
        public required DateTime CreatedDate { get; init; }
        /// <summary>
        /// 마지막 수정일시
        /// </summary>
        public DateTime? LastModifiedDate { get; init; }
    }
}
