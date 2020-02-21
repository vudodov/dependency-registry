namespace DependencyRegistry.Tests.TestClasses
{
    internal interface IInheritanceTestDependee : IDependee
    {
    }

    internal interface IInheritanceTestDepender<TDependee> : IDepender<TDependee>
        where TDependee : IDependee
    {
    }

    internal class InheritanceTestDependeeClassOne : IInheritanceTestDependee
    {
    }

    internal class InheritanceTestDependerClassOne 
        : IInheritanceTestDepender<InheritanceTestDependeeClassOne>
    {
    }
    
    internal class InheritanceTestDependeeClassTwo : IInheritanceTestDependee
    {
    }

    internal class InheritanceTestDependerClassTwo 
        : IInheritanceTestDepender<InheritanceTestDependeeClassTwo>
    {
    }
}