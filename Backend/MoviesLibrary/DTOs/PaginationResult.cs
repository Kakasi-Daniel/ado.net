using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.DTOs
{
    public class PaginationResult
    {
        public List<MovieOut> Data { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }

        public PaginationResult(List<MovieOut> data,int pages,int page)
        {
            Data = data;
            Pages = pages;
            Page = page;
        }
    }
}
