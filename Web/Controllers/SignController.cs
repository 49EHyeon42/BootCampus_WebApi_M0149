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
        /// <param name="request">사용자 등록 요청 본문</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        public IActionResult SignInUser(RqSignInUser request)
        {
            _userStorage.SaveUserInfo(new UserInfo
            {
                Name = request.Name,
                Sex = request.Sex,
                BirthDate = request.BirthDate,
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
