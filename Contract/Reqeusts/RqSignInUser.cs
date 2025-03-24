using System.ComponentModel.DataAnnotations;

namespace Contract.Reqeusts
{
    public class RqSignInUser
    {
        /// <summary>사용자 이름</summary>
        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "2-32 글자만 가능합니다.")]
        public required string Name { get; init; }

        /// <summary>사용자 성별</summary>
        [Required]
        [RegularExpression("^(M|F)$", ErrorMessage = "'M' 또는 'F'만 가능합니다.")]
        public required string Sex { get; init; }

        /// <summary>사용자 생년월일</summary>
        [Required]
        // NOTE: custom attribute 필요
        public required DateTime BirthDate { get; init; }
    }
}
