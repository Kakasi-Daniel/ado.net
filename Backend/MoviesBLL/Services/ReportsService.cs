using MoviesDAL.Repositories.Interfaces;
using System;
using System.Data.SqlClient;

namespace MoviesBLL.Services
{
    public class ReportsService
    {
       

        IMoviesRepository MoviesRepository { get; }

        public ReportsService(IMoviesRepository moviesRepository)
        {
            MoviesRepository = moviesRepository;
           
        }

        public string GetReport()
        {
            return "Alight";
        }
    }
}
