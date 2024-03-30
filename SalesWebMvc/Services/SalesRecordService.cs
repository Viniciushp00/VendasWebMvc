using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result =
                from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(sale => sale.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sale => sale.Date <= maxDate);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result =
                from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(sale => sale.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sale => sale.Date <= maxDate);
            }

            var resultfinal= await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return resultfinal.GroupBy(x => x.Seller.Department).ToList();
        }

        public async Task InsertAsync(SalesRecord sale)
        {
            await _context.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public KeyValuePair<Department, int> TopSaleDepartment(DateTime? minDate, DateTime? maxDate)
        {
            Dictionary<Department,int> saleForDepartment= new Dictionary<Department,int>();
            var result =
                from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(sale => sale.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sale => sale.Date <= maxDate);
            }

            var resultToList = result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToList();

            var resultFinal = resultToList.GroupBy(x => x.Seller.Department).ToList();

            int sum;
            foreach (var department in resultFinal)
            {
                sum = 0;
                foreach (var item in department)
                {
                    sum++;
                }
                saleForDepartment.Add(department.Key, sum);
            }


            return saleForDepartment.OrderByDescending(x => x.Value).FirstOrDefault();
        }

    }
}
