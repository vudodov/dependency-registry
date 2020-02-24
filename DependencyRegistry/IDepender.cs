namespace DependencyRegistry
{
    public interface IDepender<in TDependee> where TDependee : IDependee
    {
    }
}