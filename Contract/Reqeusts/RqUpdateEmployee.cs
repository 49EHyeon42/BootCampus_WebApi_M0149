namespace Contract.Reqeusts
{
    public class RqUpdateEmployee
    {
        /// <summary>직원 이름</summary>
        public required string Name { get; init; }
        /// <summary> 직원 나이</summary>
        public required int Age { get; init; }
        /// <summary>직원 주소</summary>
        public required string Address { get; init; }
        /// <summary>직원 전화번호</summary>
        public required string PhoneNumber { get; init; }
    }
}
