using Microsoft.Extensions.DependencyInjection;

using Vigor.Core.Common.Queue.Redis.Options;

namespace Vigor.Core.Common.Queue.Redis.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddRedisClient(
    this IServiceCollection services,
    string? connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new ArgumentNullException(
          nameof(connectionString),
          "Redis connection string cannot be null");
    }


    services.AddSingleton<RedisClient>(_ => new(connectionString));
  }

  public static void AddScopedRedisStreamPublisher(
    this IServiceCollection services,
    Func<IServiceProvider, RedisStreamPublisherOptions> getOptions)
    => services.AddScoped<RedisStreamPublisher>(provider => new(
        provider.GetRequiredService<RedisClient>(),
        getOptions.Invoke(services.BuildServiceProvider())));

  public static void AddSingletonRedisStreamPublisher(
    this IServiceCollection services,
    Func<IServiceProvider, RedisStreamPublisherOptions> getOptions) => services
      .AddSingleton<RedisStreamPublisher>(provider => new(
        provider.GetRequiredService<RedisClient>(),
        getOptions.Invoke(services.BuildServiceProvider())));

  public static void AddScopedRedisStreamConsumer(
    this IServiceCollection services,
    Func<IServiceProvider, RedisStreamConsumerOptions> getOptions) => services
      .AddScoped<RedisStreamConsumer>(provider => new(
        provider.GetRequiredService<RedisClient>(),
        getOptions.Invoke(services.BuildServiceProvider())));

  public static void AddSingletonRedisStreamConsumer(
    this IServiceCollection services,
    Func<IServiceProvider, RedisStreamConsumerOptions> getOptions) => services
      .AddSingleton<RedisStreamConsumer>(provider => new(
        provider.GetRequiredService<RedisClient>(),
        getOptions.Invoke(services.BuildServiceProvider())));
}
