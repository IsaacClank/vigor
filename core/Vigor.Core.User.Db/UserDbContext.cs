using Microsoft.EntityFrameworkCore;

using Vigor.Core.Common.Db.PostgreSql;

namespace Vigor.Core.User.Db;

public sealed class UserDbContext(DbContextOptions<UserDbContext> options) : PostgreSqlDbContext(options)
{
}
