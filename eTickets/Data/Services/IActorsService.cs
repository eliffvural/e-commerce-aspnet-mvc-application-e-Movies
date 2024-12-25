using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IActorsService
    {
        IEnumerable<Actor> GetAll();
    }
}
