// ReSharper disable UnusedType.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

using System.Reflection;

namespace Castle.DynamicProxy;

/// <summary>
/// A utility class for creating instances of classes with optional interceptors.
/// </summary>
public static class New
{
    /// <summary>
    /// Creates a new instance of a class of type <typeparamref name="TClass"/> with optional interceptors defined using attributes.
    /// </summary>
    /// <typeparam name="TClass">The type of class to create.</typeparam>
    /// <returns>A new instance of the specified class with interceptors applied.</returns>
    /// <remarks>
    /// This method creates an instance of the specified class with interceptors defined
    /// using <see cref="InterceptorAttribute"/> attributes applied to the class.
    /// </remarks>
    public static TClass Of<TClass>()
        where TClass : class, new()
    {
        var interceptors = typeof(TClass).GetTypeInfo().GetCustomAttributes(typeof(InterceptorAttribute), false)
            .Cast<InterceptorAttribute>()
            .Select(x => x.Interceptor)
            .ToList();

        var instances = interceptors.Select(Activator.CreateInstance).Cast<IInterceptor>().ToArray();

        var generator = new ProxyGenerator();
        return generator.CreateClassProxy<TClass>(instances);
    }
    
    /// <summary>
    /// Creates a new instance of a class of type <typeparamref name="TClass"/> with optional interceptors provided as parameters.
    /// </summary>
    /// <typeparam name="TClass">The type of class to create.</typeparam>
    /// <param name="interceptors">An array of interceptors to apply to the class.</param>
    /// <returns>A new instance of the specified class with the provided interceptors applied.</returns>
    /// <remarks>
    /// This method creates an instance of the specified class and applies the provided interceptors.
    /// If interceptors are defined using <see cref="InterceptorAttribute"/> attributes on the class,
    /// they are also included.
    /// </remarks>
    public static TClass Of<TClass>(params IInterceptor[] interceptors)
        where TClass : class, new()
    {
        var interceptors1 = typeof(TClass).GetTypeInfo().GetCustomAttributes(typeof(InterceptorAttribute), false)
            .Cast<InterceptorAttribute>()
            .Select(x => x.Interceptor)
            .ToList();

        var instances = interceptors1.Select(Activator.CreateInstance).Cast<IInterceptor>().ToList();

        if (interceptors is {Length: > 0})
        {
            instances.AddRange(interceptors);
        }

        var generator = new ProxyGenerator();
        return generator.CreateClassProxy<TClass>(instances.ToArray());
    }
    
    /// <summary>
    /// Creates a new instance of a class of type <typeparamref name="TClass"/> with optional interceptors and proxy generation options.
    /// </summary>
    /// <typeparam name="TClass">The type of class to create.</typeparam>
    /// <param name="options">The proxy generation options to customize proxy behavior.</param>
    /// <param name="interceptors">An array of interceptors to apply to the class.</param>
    /// <returns>A new instance of the specified class with the provided interceptors and proxy generation options applied.</returns>
    /// <remarks>
    /// This method creates an instance of the specified class and applies the provided interceptors and proxy generation options.
    /// </remarks>
    public static TClass Of<TClass>(ProxyGenerationOptions options, params IInterceptor[] interceptors)
        where TClass : class, new()
    {
        var generator = new ProxyGenerator();
        return generator.CreateClassProxy<TClass>(options, interceptors);
    }
}