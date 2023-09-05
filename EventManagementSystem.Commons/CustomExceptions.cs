using System.Diagnostics.CodeAnalysis;

namespace EventManagementSystem.Commons
{
    public class BaseException : System.Exception
    {
        public ErrorCode ErrorCode { get; private set; }

        public BaseException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
        public BaseException(ErrorCode errorCode, string msg) : base(msg)
        {
            ErrorCode = errorCode;
        }
        public BaseException(ErrorCode code, string msg, Exception innerException)
            : base(msg, innerException)
        {
            ErrorCode = code;
        }
        public BaseException(ErrorCode code, string format, params object[] args)
            : base(string.Format(format, args))
        {
            ErrorCode = code;
        }
    }

    public class ItemNotFoundException : BaseException
    {
        public ItemNotFoundException(ErrorCode errorCode) : base(errorCode) { }

        public ItemNotFoundException(ErrorCode errorCode, string msg) : base(errorCode, msg) { }
        public ItemNotFoundException(ErrorCode errorCode, string item, object id)
            : base(errorCode, $"Entity {item} with id {id} not found") { }

        public static void ThrowIfNull([NotNull] object? argument, string item)
        {
            if (argument == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    $"Entity {item} was not found"
                );
        }
         public static void ThrowIfNull<T>(T argument) where T : class
        {
            if (argument == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    $"Entity {typeof(T).ToString()} was not found"
                );
        }
    }

    public class UserFriendlyException : BaseException
    {
        public UserFriendlyException(ErrorCode code)
            : base(code, "An error occurred in the program") { }

        public UserFriendlyException(ErrorCode errorCode, string msg) : base(errorCode, msg) { }

        public UserFriendlyException(ErrorCode code, string msg, Exception innerException)
            : base(code, msg, innerException) { }
        public UserFriendlyException(ErrorCode code, string format, params object[] args)
            : base(code, format, args) { }
    }

    public class BadGatewayException : BaseException
    {
        public BadGatewayException(ErrorCode code, string gatewayError)
            : base(code, $"Upstream gateway encountered error: {gatewayError}") { }

        public BadGatewayException(ErrorCode code, string msg, Exception innerException)
            : base(code, msg, innerException) { }
    }
}
