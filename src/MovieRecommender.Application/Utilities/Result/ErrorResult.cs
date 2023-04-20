using MovieRecommender.Application.Constants;

namespace MovieRecommender.Application.Utilities.Result
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false, Messages.Error, 400)
        {
        }

        public ErrorResult(int status) : base(false, Messages.Error, status)
        {
        }

        public ErrorResult(string message) : base(false, message, 400)
        {
        }

        public ErrorResult(string message, int status) : base(false, message, status)
        {
        }
    }
}
