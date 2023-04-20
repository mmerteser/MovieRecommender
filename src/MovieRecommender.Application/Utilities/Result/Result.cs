using MovieRecommender.Application.Utilities.Result;

namespace MovieRecommender.Application.Utilities.Result
{
    public class Result : IResult
    {
        public Result(bool success, string message, int status) : this(success, message)
        {
            Status = status;
        }
        public Result(bool success, int status) : this(success)
        {
            Status = status;
        }
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
        public int Status { get; }
    }
}
