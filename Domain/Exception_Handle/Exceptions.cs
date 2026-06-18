namespace Domain.Exception_Handle
{
    public class Exceptions
    {
        public class NotFoundException(string message) : Exception(message) { }

        public class UnauthorizedException() : Exception("Invalid Operation") { }

        public class BadRequestException : Exception
        {
            public IEnumerable<string>? Errors { get; }
            public BadRequestException(string message, IEnumerable<string>? errors = null) : base(message)
            {
                Errors = errors;
            }
        }
    }
}