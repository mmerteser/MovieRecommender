using MovieRecommender.Application.Constants;

namespace MovieRecommender.Application.Utilities.Result
{
    public sealed class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true, Messages.Success, 200)
        {
        }

        public SuccessDataResult(T data, int status) : base(data, true, Messages.Success, status)
        {
        }

        public SuccessDataResult(T data, string message) : base(data, true, message, 200)
        {
        }

        public SuccessDataResult(T data, string message, int status) : base(data, true, message, status)
        {
        }
    }
}
