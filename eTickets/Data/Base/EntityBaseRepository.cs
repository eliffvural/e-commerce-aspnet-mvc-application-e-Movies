
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {

        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public  async Task AddAsync(T entity)=> await _context.Set<T>().AddAsync(entity);
        

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Set<T>().FirstOrDefault(n => n.Id == id);
            EntityEntry entiryEntry = _context.Entry<T>(entity);
            entiryEntry.State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>  await _context.Set<T>().ToListAsync();
           
        

        public async Task<T> GetByIdAsync(int id)=> await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
      









        public async  Task UpdateAsync(int id, T entity)
        {
           EntityEntry entiryEntry = _context.Entry<T>(entity);
            entiryEntry.State = EntityState.Modified;

        }
    }
}
