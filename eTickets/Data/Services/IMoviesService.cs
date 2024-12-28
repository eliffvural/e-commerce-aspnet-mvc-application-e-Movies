using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMoviesService: IEntityBaseRepository<Movie>
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        Task<string?> GetMovieByIdAsync(int id);
    }
}
