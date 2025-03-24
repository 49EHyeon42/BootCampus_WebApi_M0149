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

        [HttpDelete]
        public IActionResult SignOutUser()
        {
            _userStorage.DeleteUserInfo();

            return Ok();
        }
    }
}
