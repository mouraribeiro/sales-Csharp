using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? mindate, DateTime? maxdate)
        {
            var result = from obj in _context.SalesRecord select obj; 

            if (mindate.HasValue)
            {
                result = result.Where(x => x.Date >= mindate.Value);

            }
            if (maxdate.HasValue)
            {
                result = result.Where(x => x.Date <= maxdate.Value);
            }

            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }

        
    }
}
