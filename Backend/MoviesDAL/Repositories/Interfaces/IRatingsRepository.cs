using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IRatingsRepository
    {
        Task<List<RatingModel>> GetRatingsAsync();
        Task<RatingModel> GetRatingByIdAsync(int id);
        Task<int> AddRatingAsync(RatingModel rating);
        Task DeleteRatingAsync(int id);
        Task UpdateRatingAsync(int id, RatingModel rating);

    }
}
