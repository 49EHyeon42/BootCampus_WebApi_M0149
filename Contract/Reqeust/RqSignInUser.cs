using System.ComponentModel.DataAnnotations;

namespace Contract.Reqeust
{
    public class RqSignInUser
    {
        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "2-32 글자만 가능합니다.")]
        public required string Name { get; init; }

        [Required]
        [RegularExpression("^(M|F)$", ErrorMessage = "'M' 또는 'F'만 가능합니다.")]
        public required string Sex { get; init; }
        [Required]
        // NOTE: custom attribute 필요
        public required DateTime BirthDate { get; init; }
    }
}
