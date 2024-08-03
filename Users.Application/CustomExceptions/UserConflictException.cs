namespace Users.Application.CustomExceptions
{
    public class UserConflictException : Exception
    {
        public UserConflictException()
        {
        }

        public UserConflictException(string message)
            : base(message)
        {
        }

        public UserConflictException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
