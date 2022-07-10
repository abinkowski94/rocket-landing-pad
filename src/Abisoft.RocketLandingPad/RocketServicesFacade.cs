using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Decorators.ThreadLocking;
using Abisoft.RocketLandingPad.Decorators.Validating;
using Abisoft.RocketLandingPad.Factories;
using Abisoft.RocketLandingPad.Properties;
using Abisoft.RocketLandingPad.Providers;
using Abisoft.RocketLandingPad.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Abisoft.RocketLandingPad;

public class RocketServicesFacade : IDisposable, IAsyncDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public ILandingAreaService AreaService
        => _serviceProvider.GetRequiredService<ILandingAreaService>();

    public ILandingPlatformService PlatformService
        => _serviceProvider.GetRequiredService<ILandingPlatformService>();

    public IRocketService RocketService
        => _serviceProvider.GetRequiredService<IRocketService>();

    public ILandingService LandingService
        => _serviceProvider.GetRequiredService<ILandingService>();

    public RocketServicesFacade()
    {
        var serviceCollection = new ServiceCollection();

        AddProviders(serviceCollection);
        AddFactories(serviceCollection);
        AddValidators(serviceCollection);
        AddServices(serviceCollection);
        AddServiceDecorators(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();

        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _serviceProvider.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    private static void AddProviders(ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISequenceIdProvider, SequenceIdProvider>();
    }

    private static void AddFactories(ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAreaFactory, AreaFactory>();
        serviceCollection.AddSingleton<IPlatformFactory, PlatformFactory>();
        serviceCollection.AddSingleton<IRocketFactory, RocketFactory>();
        serviceCollection.AddSingleton<IOutlineFactory, SquareOutlineFactory>();
    }

    private static void AddServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<AreaService>();
        serviceCollection.AddSingleton<PlatformService>();
        serviceCollection.AddSingleton<RocketService>();
        serviceCollection.AddSingleton<LandingService>();
    }

    private static void AddServiceDecorators(ServiceCollection serviceCollection)
    {
        // Validating
        serviceCollection.AddSingleton(sp => ActivatorUtilities.CreateInstance<AreaServiceValidatingDecorator>(sp, sp.GetRequiredService<AreaService>()));

        serviceCollection.AddSingleton(sp => ActivatorUtilities.CreateInstance<PlatformServiceValidatingDecorator>(sp, sp.GetRequiredService<PlatformService>()));

        serviceCollection.AddSingleton(sp => ActivatorUtilities.CreateInstance<LandingServiceValidatingDecorator>(sp, sp.GetRequiredService<LandingService>()));

        // Thread locking
        serviceCollection.AddSingleton(sp => ActivatorUtilities.CreateInstance<AreaServiceLockingDecorator>(sp, sp.GetRequiredService<AreaServiceValidatingDecorator>()));

        serviceCollection.AddSingleton(sp => ActivatorUtilities.CreateInstance<LandingServiceLockingDecorator>(sp, sp.GetRequiredService<LandingServiceValidatingDecorator>()));

        // Decorated results
        serviceCollection.AddSingleton<ILandingAreaService>(sp => sp.GetRequiredService<AreaServiceLockingDecorator>());
        serviceCollection.AddSingleton<ILandingPlatformService>(sp => sp.GetRequiredService<PlatformServiceValidatingDecorator>());
        serviceCollection.AddSingleton<IRocketService>(sp => sp.GetRequiredService<RocketService>());
        serviceCollection.AddSingleton<ILandingService>(sp => sp.GetRequiredService<LandingServiceLockingDecorator>());
    }

    private static void AddValidators(ServiceCollection serviceCollection)
    {
        foreach (var validatorType in AssemblyMarker.ValidatorTypes)
        {
            serviceCollection.AddSingleton(validatorType);

            foreach (var validatorInterfaceType in validatorType.GetInterfaces())
            {
                serviceCollection.AddSingleton(validatorInterfaceType, sp => sp.GetRequiredService(validatorType));
            }
        }
    }
}
