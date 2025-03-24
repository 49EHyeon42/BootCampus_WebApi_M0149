namespace Common.Exceptions
{
    public class WorkExperienceNotFoundException : BaseException
    {
        public WorkExperienceNotFoundException() : base(404, "WORK_EXPERIENCE_NOT_FOUND") { }
    }
}
