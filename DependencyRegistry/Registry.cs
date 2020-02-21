using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyRegistry
{
    public class Registry<TDependee> : IRegistry
        where TDependee : IDependee
    {
        private readonly IDictionary<string, (Type dependee, Type depender)> _mapping;

        public Registry() : this(new[] {Assembly.GetCallingAssembly()})
        {
        }

        public Registry(IEnumerable<Assembly> assemblies)
        {
            _mapping = Scan(assemblies);
        }

        public (Type dependee, Type depender) this[string name]
        {
            get
            {
                if (_mapping.TryGetValue(name.ToLowerInvariant(), out var map))
                    return map;
                throw new KeyNotFoundException($"The dependee {name} was not found in the registry.");
            }
        }

        public bool TryGetValue(string name, out (Type dependee, Type depender) result) =>
            _mapping.TryGetValue(name.ToLowerInvariant(), out result);

        public IEnumerator<(string name, Type dependee, Type depender)> GetEnumerator()
        {
            return _mapping.Select(m =>
                    (name: m.Key, dependee: m.Value.dependee, depender: m.Value.depender))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static Dictionary<string, (Type dependee, Type depender)> Scan(
            IEnumerable<Assembly> assemblies)
        {
            if (!assemblies.Any())
                throw new ArgumentException("The registry requires at least one assembly to scan for dependencies");
            try
            {
                var dependeeToDependerMapping = new HashSet<(Type dependee, Type depender)>();

                foreach (var assembly in assemblies)
                {
                    var discoverDependeeTypes = assembly.GetTypes().Where(type =>
                        type.IsClass && !type.IsAbstract && typeof(TDependee).IsAssignableFrom(type));

                    foreach (var dependee in discoverDependeeTypes)
                    {
                        bool GetDependerInterfacePredicate(Type @interface) =>
                            @interface.IsGenericType
                            && @interface.GetGenericTypeDefinition().IsAssignableFrom(typeof(IDepender<>))
                            && @interface.GetGenericArguments().Single().IsAssignableFrom(dependee);

                        var depender =
                            assembly.GetTypes()
                                .Single(type => type.GetInterfaces()
                                    .Any(GetDependerInterfacePredicate));

                        dependeeToDependerMapping.Add((dependee: dependee, depender: depender));
                    }
                }

                return dependeeToDependerMapping.ToDictionary(map =>
                    map.dependee.Name.ToKebabCase(), map => map);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Each dependee must have one and only one dependee handler", e);
            }
        }
    }
}