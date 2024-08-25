using Vigor.Core.User.Db.Extensions.DependencyInjection;
using Vigor.Core.Common.Db.Repository.Extensions.DependencyInjection;
using Vigor.Core.User.Db;
using Vigor.Core.User.Api.Http.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDb"));
builder.Services.AddScopedUnitOfWork(provider => provider.GetRequiredService<UserDbContext>());
builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

#region Business Logic
builder.Services.AddScoped<IProfileService, ProfileService>();
#endregion

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
  o => o.IncludeXmlComments(
    Path.Combine(
      AppContext.BaseDirectory,
      $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
