using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Application.Utilities.Results
{
    public class Result
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Sucess";
    }

    public class GetOneResult<TEntity> : Result 
    {
        public TEntity Data { get; set; }
    }

    public class GetManyResult<TEntity> : Result
    {
        public IEnumerable<TEntity> Data { get; set; }
    }
}
