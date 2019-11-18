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
        /// <param name="includeInterfaces">Inclui métodos das interfaces</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> AllProperties(this Type type, bool includeInterfaces = false)
        {
            var result = new List<MethodInfo>();
            
            var index = 0;
            var types = new List<Type>{ type };
            while (index < types.Count)
            {
                type = types[index++];

                result.AddRange(
                    type.GetProperties(
                            BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Static |
                            BindingFlags.Instance |
                            BindingFlags.DeclaredOnly)
                        .SelectMany(a => new[] {a.GetMethod, a.SetMethod}));

                if (type.BaseType != null && !types.Contains(type.BaseType)) types.Add(type.BaseType);
                if (!includeInterfaces) continue;
                foreach (var typeInterface in type.GetInterfaces())
                    if (!types.Contains(typeInterface))
                        types.Add(typeInterface);
            }

            result.RemoveAll(a => a == null);
            return result;
        }
        
        /// <summary>
        /// Retorna todos os métodos que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="includeInterfaces">Inclui métodos das interfaces</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> AllMethods(this Type type, bool includeInterfaces = false)
        {
            var result = new List<MethodInfo>();
            
            var allProperties = AllProperties(type, includeInterfaces).ToList();
            
            var index = 0;
            var types = new List<Type>{ type };
            while (index < types.Count)
            {
                type = types[index++];

                result.AddRange(
                    type.GetMethods(
                        BindingFlags.Public | 
                        BindingFlags.NonPublic | 
                        BindingFlags.Static | 
                        BindingFlags.Instance | 
                        BindingFlags.DeclaredOnly)
                        .Where(method => !allProperties.Contains(method)));

                if (type.BaseType != null && !types.Contains(type.BaseType)) types.Add(type.BaseType);
                if (!includeInterfaces) continue;
                foreach (var typeInterface in type.GetInterfaces())
                    if (!types.Contains(typeInterface))
                        types.Add(typeInterface);
            }

            return result;
        }
        
        /// <summary>
        /// Retorna todos os métodos declaradas no tipo apenas das propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> MyProperties(this Type type)
        {
            return type.GetProperties(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static |
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly)
                .SelectMany(a => new[] {a.GetMethod, a.SetMethod})
                .Where(a => a.DeclaringType == type && a.DeclaringType.Assembly == type.Assembly)
                .ToList();
        }
        
        /// <summary>
        /// Retorna todos os métodos declaradas no tipo que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> MyMethods(this Type type)
        {
            var myProperties = MyProperties(type).ToList();
            
            return type.GetMethods(
                    BindingFlags.Public | 
                    BindingFlags.NonPublic | 
                    BindingFlags.Static | 
                    BindingFlags.Instance | 
                    BindingFlags.DeclaredOnly)
                .Where(method => !myProperties.Contains(method))
                .Where(a => a.DeclaringType == type && a.DeclaringType.Assembly == type.Assembly)
                .ToList();
        }
    }
}