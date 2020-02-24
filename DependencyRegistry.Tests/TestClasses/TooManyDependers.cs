namespace DependencyRegistry.Tests.TestClasses
{
    internal class TooManyDependersDependee : IDependee
    {
        
    }

    internal class TooManyDependersOne : IDepender<TooManyDependersDependee>
    {
        
    }
    internal class TooManyDependersTwo : IDepender<TooManyDependersDependee>
    {
        
    }
}