namespace DependencyRegistry.Tests.TestClasses
{
    internal class TestDependee : IDependee
    {
        
    }

    internal class TestDepender : IDepender<TestDependee>
    {
        
    }
}