using MovieRecommender.Application.Utilities.Result;

namespace MovieRecommender.Application.Utilities.Result
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get; }
    }
}
