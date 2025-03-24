namespace Common.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base(404, "USER_NOT_FOUND") { }
    }
}
