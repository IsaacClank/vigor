using Microsoft.EntityFrameworkCore;

namespace User.Database
{
    public sealed class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
    }
}