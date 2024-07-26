using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;

namespace RunGroup.Repository
{
    public class RaceRepository: IRepository<Race>
    {
        private readonly ApplicationDbContext context;
        public RaceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Race race)
        {
            this.context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            this.context.Remove(race);
            return Save();
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            this.context.Update(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await this.context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await this.context.Races.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Race>> GetByCity(string city)
        {
            return await this.context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }
    }
}
