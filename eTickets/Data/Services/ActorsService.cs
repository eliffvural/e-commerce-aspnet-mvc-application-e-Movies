using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {

        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context)
        {
            _context = context;
        }

        void IActorsService.Add(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();
        }

        void IActorsService.Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>>GetAll()
        {
            var result =await  _context.Actors.ToListAsync();
            return result;
        }

        Actor IActorsService.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Actor IActorsService.Update(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}
