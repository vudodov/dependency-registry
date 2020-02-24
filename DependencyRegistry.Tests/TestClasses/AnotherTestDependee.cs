namespace DependencyRegistry.Tests.TestClasses
{
    internal class AnotherTestDependee : IDependee
    {
        
    }

    internal class AnotherTestDepender : IDepender<AnotherTestDependee>
    {
        
    }
}