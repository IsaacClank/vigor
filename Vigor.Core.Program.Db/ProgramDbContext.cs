using Microsoft.EntityFrameworkCore;

using Vigor.Core.Common.Db.PostgreSql;

namespace Vigor.Core.Program.Db;

public sealed class ProgramDbContext(DbContextOptions<ProgramDbContext> options) : PostgreSqlDbContext(options)
{
}
