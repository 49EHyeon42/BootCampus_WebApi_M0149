using Common;
using Contract.Reqeust;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes.ExceptionFilters.Controllers;

namespace Web.Controllers
{
    [Route("api/sign")]
    [ApiController]
    [SignControllerExceptionFilter]
    public class SignController(CurrentUserStorage currentUserStorage) : ControllerBase
    {
        private readonly CurrentUserStorage _currentUserStorage = currentUserStorage;

        [HttpPost]
        public IActionResult SignInUser(RqSignInUser request)
        {
            _currentUserStorage.SaveUserInfo(new UserInfo
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
            _currentUserStorage.DeleteUserInfo();

            return Ok();
        }
    }
}
