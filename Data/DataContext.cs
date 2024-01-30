using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<CustomerModel> Customer { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
