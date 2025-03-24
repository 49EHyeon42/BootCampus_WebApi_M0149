namespace Common.Exceptions
{
    public class UserAlreadySignInException : BaseException
    {
        public UserAlreadySignInException() : base(404, "USER_ALREADY_SIGN_IN") { }
    }
}
