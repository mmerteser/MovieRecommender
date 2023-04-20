using MovieRecommender.Application.Constants;

namespace MovieRecommender.Application.Utilities.Result
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true, Messages.Success, 200)
        {
        }

        public SuccessResult(string message) : base(true, message, 200)
        {
        }


        public SuccessResult(int status) : base(true, Messages.Success, status)
        {
        }


        public SuccessResult(string message, int status) : base(true, message, status)
        {
        }
    }
}
