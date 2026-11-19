namespace RoomHub.Api.Common.CQRS;

public static class HandlerRegistration
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        foreach (var type in typeof(HandlerRegistration).Assembly.GetTypes()
                     .Where(t => t is { IsClass: true, IsAbstract: false }))
        foreach (var contract in type.GetInterfaces().Where(i => i.IsGenericType &&
                     (i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                      i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))))
            services.AddScoped(contract, type);
        return services;
    }
}
