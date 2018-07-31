using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                return _context.Products.OrderBy(x => x.Title).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception occurred", ex);
                return null;
            }
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                return _context.Products.Where(x => x.Category == category).OrderBy(x => x.Title).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception occurred", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
