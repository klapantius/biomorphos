using Microsoft.Extensions.DependencyInjection;

namespace biomorphos.library.services
{

    public interface IDimensionProviderFactory
    {
        IDimensionProvider Create(params int[] dimensions);
        IDimensionProvider Create(int dimensionCount);
    }

    /// <summary>
    /// Factory for creating IDimensionProvider instances with specified dimensions.
    /// </summary>
    public class DimensionProviderFactory(IServiceProvider serviceProvider, Type providerImplementation) : IDimensionProviderFactory
    {
        /**
        Configure the factory in Program.cs like this:

        services.AddTransient<IDimensionProviderFactory>(sp =>
            new DimensionProviderFactory(dimensions =>
                ActivatorUtilities.CreateInstance<DimensionProvider>(sp, dimensions)));

        Be aware of the usage of the type DimensionProvider class in this registration.
        **/
        
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly Type _providerType = providerImplementation;

        /// <summary>
        /// Creates a new DimensionProvider with the specified sizes for each dimension.
        /// </summary>
        /// <param name="dimensions">The sizes of each dimension.</param>
        /// <returns>A new DimensionProvider instance.</returns>
        public IDimensionProvider Create(params int[] dimensions)
        {
            return (IDimensionProvider)ActivatorUtilities.CreateInstance(_serviceProvider, _providerType, [dimensions]);
        }

        /// <summary>
        /// Creates a new DimensionProvider with the specified number of dimensions, all set to -1 (unbounded).
        /// </summary>
        /// <param name="dimensionCount">The number of dimensions.</param>
        /// <returns>A new DimensionProvider instance.</returns>
        public IDimensionProvider Create(int dimensionCount)
        {
            if (dimensionCount < 0) { throw new ArgumentOutOfRangeException(nameof(dimensionCount), "Dimension count cannot be negative."); }
            return (IDimensionProvider)ActivatorUtilities.CreateInstance(_serviceProvider, _providerType, dimensionCount);
        }
    }
}