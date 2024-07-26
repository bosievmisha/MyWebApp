using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;

namespace RunGroup.Repository
{
    public class ClubRepository : IRepository<Club>
    {
        private readonly ApplicationDbContext context;
        public ClubRepository(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public bool Add(Club club)
        {
            this.context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            this.context.Remove(club);
            return Save();
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Club club)
        {
            this.context.Update(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await this.context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await this.context.Clubs.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Club>> GetByCity(string city)
        {
            return await this.context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

    }
}
