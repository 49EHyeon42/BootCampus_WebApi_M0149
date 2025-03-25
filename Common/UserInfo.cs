namespace Common
{
    public class UserInfo
    {
        /// <summary>
        /// 사용자 이름
        /// </summary>
        public required string Name { get; init; }
        /// <summary>
        /// 사용자 성별
        /// </summary>
        public required string Sex { get; init; }
        /// <summary>
        /// 사용자 생년월일
        /// </summary>
        public required DateTime BirthDate { get; init; }
    }
}
