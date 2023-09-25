// ReSharper disable UnusedType.Global
// ReSharper disable CheckNamespace

namespace Castle.DynamicProxy;

/// <summary>
/// An attribute used to specify one or more interceptors for a class.
/// </summary>
/// <remarks>
/// This attribute should be applied to classes that need to be intercepted
/// by one or more interceptor types.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class InterceptorAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InterceptorAttribute"/> class.
    /// </summary>
    /// <param name="interceptor">The type of interceptor to be applied to the class.</param>
    public InterceptorAttribute(Type interceptor)
    {
        Interceptor = interceptor;
    }

    /// <summary>
    /// Gets the type of interceptor specified by this attribute.
    /// </summary>
    public Type Interceptor { get; }
}
