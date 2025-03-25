using Common;
using Common.Exceptions;

namespace Web
{
    public class UserStorage
    {
        private UserInfo? _userInfo;

        /// <summary>사용자 저장 메서드</summary>
        /// <param name="userInfo"></param>
        public void SaveUserInfo(UserInfo userInfo)
        {
            if (_userInfo is not null)
            {
                throw new UserAlreadySignInException();
            }

            _userInfo = userInfo;
        }

        /// <summary>사용자 삭제 메서드</summary>
        public void DeleteUserInfo()
        {
            if (_userInfo is null)
            {
                throw new UserNotFoundException();
            }

            _userInfo = null;
        }

        /// <summary>사용자 이르 조회 메서드</summary>
        /// <returns><see cref="string"/></returns>
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
