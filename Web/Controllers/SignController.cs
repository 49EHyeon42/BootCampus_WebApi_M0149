using Common;
using Contract.Reqeusts;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes.ExceptionFilters.Controllers;

namespace Web.Controllers
{
    [Route("api/sign")]
    [ApiController]
    [SignControllerExceptionFilter]
    public class SignController(UserStorage userStorage) : ControllerBase
    {
        private readonly UserStorage _userStorage = userStorage;

        /// <summary>사용자 등록 메서드</summary>
        /// <param name="body">사용자 등록 요청 본문</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        public IActionResult SignInUser(RqSignInUser body)
        {
            _userStorage.SaveUserInfo(new UserInfo
            {
                Name = body.Name,
                Sex = body.Sex,
                BirthDate = body.BirthDate
            });

            return Ok();
        }

        /// <summary>사용자 해제 메서드</summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpDelete]
        public IActionResult SignOutUser()
        {
            _userStorage.DeleteUserInfo();

            return Ok();
        }
    }
}
