using Application.Features.Finances.Interfaces;
using Domain.Features.Finances.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Finances
{
    public class NatureRepository : INatureRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// instanica del repositorio de naturaleza, que recibe una instancia de AppDbContext para realizar operaciones relacionadas a la naturaleza
        /// </summary>
        /// <param name="context"></param>
        public NatureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nature?>> GetAllNaturesAsync()
        {
            return await _context.Natures.ToListAsync();
        }

        public async Task<Nature?> GetNatureByIdAsync(Guid id)
        {
            return await _context.Natures.FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
