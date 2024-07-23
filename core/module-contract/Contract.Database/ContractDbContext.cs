using Microsoft.EntityFrameworkCore;

namespace Contract.Database
{
  public sealed class ContractDbContext(DbContextOptions<ContractDbContext> options) : DbContext(options)
  {
  }
}
