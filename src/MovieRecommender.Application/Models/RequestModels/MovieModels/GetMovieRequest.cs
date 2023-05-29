using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Application.Models.RequestModels.MovieModels
{
    public class GetMovieRequest
    {
        public const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) || value.Equals(-1) ? maxPageSize : value;
            }
        }
    }
}
