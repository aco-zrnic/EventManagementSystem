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
    }

    public class ItemNotFoundException : BaseException
    {
        public ItemNotFoundException(ErrorCode errorCode) : base(errorCode) { }

        public ItemNotFoundException(ErrorCode errorCode, string msg) : base(errorCode, msg) { }
        public ItemNotFoundException(ErrorCode errorCode, string item, object id)
            : base(errorCode, $"Entity {item} with id {id} not found") { }
    }
}
