// ReSharper disable UnusedType.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedParameter.Global

using System.Reflection;

namespace Castle.DynamicProxy;

/// <summary>
/// A base class for creating custom interceptors to handle method calls.
/// </summary>
/// <remarks>
/// Inherit from this class to implement your own custom interceptors for method calls.
/// Interceptors can be used to perform actions before and after method execution, 
/// handle exceptions, or manipulate method behavior.
/// </remarks>
public class BaseInterceptor : IInterceptor
{
    /// <summary>
    /// Gets the default value for a given type.
    /// </summary>
    /// <param name="type">The type for which to get the default value.</param>
    /// <returns>
    /// The default value for the specified type. If the type is a value type
    /// and not nullable, a new instance of the type is created; otherwise, null is returned.
    /// </returns>
    private static object? GetDefaultValue(Type type)
    {
        if (type.GetTypeInfo().IsValueType && Nullable.GetUnderlyingType(type) == null)
            return Activator.CreateInstance(type);
        return null;
    }

    /// <summary>
    /// Intercept method calls and perform pre- and post-execution actions.
    /// </summary>
    /// <param name="invocation">The invocation information for the method call.</param>
    public void Intercept(IInvocation invocation)
    {
        try
        {
            OnEntry(invocation);
            invocation.Proceed();
            OnSuccess(invocation);
        }
        catch (Exception ex)
        {
            invocation.ReturnValue = GetDefaultValue(invocation.Method.ReturnType);
            OnException(invocation, ex);
        }
        finally
        {
            OnExit(invocation);
        }
    }

    /// <summary>
    /// A method to be executed before the intercepted method is called.
    /// </summary>
    /// <param name="invocation">The invocation information for the method call.</param>
    protected virtual void OnEntry(IInvocation invocation)
    {
        // Implement custom logic to execute before the method call.
    }

    /// <summary>
    /// A method to be executed when an exception occurs during the intercepted method call.
    /// </summary>
    /// <param name="invocation">The invocation information for the method call.</param>
    /// <param name="exception">The exception that occurred during the method call.</param>
    protected virtual void OnException(IInvocation invocation, Exception exception)
    {
        // Implement custom exception handling logic.
    }

    /// <summary>
    /// A method to be executed after the intercepted method is successfully called.
    /// </summary>
    /// <param name="invocation">The invocation information for the method call.</param>
    protected virtual void OnSuccess(IInvocation invocation)
    {
        // Implement custom logic for successful method execution.
    }

    /// <summary>
    /// A method to be executed after the intercepted method is called, 
    /// regardless of whether it succeeded or threw an exception.
    /// </summary>
    /// <param name="invocation">The invocation information for the method call.</param>
    protected virtual void OnExit(IInvocation invocation)
    {
        // Implement custom logic to execute after the method call.
    }
}
