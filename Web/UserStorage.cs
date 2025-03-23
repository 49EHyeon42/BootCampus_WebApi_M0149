using Common;
using Common.Exceptions;

namespace Web
{
    public class UserStorage
    {
        private UserInfo? _userInfo;

        public void SaveUserInfo(UserInfo userInfo)
        {
            if (_userInfo is not null)
            {
                throw new UserAlreadySignInException();
            }

            _userInfo = userInfo;
        }

        public void DeleteUserInfo()
        {
            if (_userInfo is null)
            {
                throw new UserNotFoundException();
            }

            _userInfo = null;
        }

        public string GetName()
        {
            if (_userInfo is null)
            {
                throw new UserNotFoundException();
            }

            return _userInfo.Name;
        }
    }
}
