using System;
using System.Collections.Generic;

namespace DependencyRegistry
{
    /// <summary>
    /// Name - dependee - dependee Handler mapping registry. One Handler per dependee is allowed.
    /// </summary>
    public interface IRegistry : IEnumerable<(string name, Type dependee, Type depender)>
    {
        /// <summary>
        /// Gets dependee and dependee handler by <param name="name" /> 
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown in case if dependee not registered</exception>
        (Type dependee, Type depender) this[string name] { get; }
        
        /// <summary>
        /// Gets dependee and dependee handler by <param name="name" />. If dependee was found result will go
        /// into <param name="result" /> 
        /// </summary>
        /// <returns>True if dependee is registered, False in other cases</returns>
        bool TryGetValue(string name, out (Type dependee, Type depender) result);
    }
}