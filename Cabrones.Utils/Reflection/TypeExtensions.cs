using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cabrones.Utils.Reflection
{
    /// <summary>
    /// Extensões relacionadas com: Type
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Retorna todos os métodos apenas das propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> AllProperties(this Type type)
        {
            while (true)
            {
                if (type == null) yield break;

                foreach (var property in type.GetProperties(
                    BindingFlags.Public | 
                    BindingFlags.NonPublic | 
                    BindingFlags.Static | 
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly))
                {
                    if (property.CanRead) yield return property.GetMethod;
                    if (property.CanWrite) yield return property.SetMethod;
                }

                type = type.BaseType;
            }
        }
        
        /// <summary>
        /// Retorna todos os métodos que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> AllMethods(this Type type)
        {
            var allProperties = AllProperties(type).ToList();
            while (true)
            {
                if (type == null) yield break;

                foreach (var method in type.GetMethods(
                    BindingFlags.Public | 
                    BindingFlags.NonPublic | 
                    BindingFlags.Static | 
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly))
                {
                    if (!allProperties.Contains(method)) yield return method;
                }

                type = type.BaseType;
            }
        }
    }
}