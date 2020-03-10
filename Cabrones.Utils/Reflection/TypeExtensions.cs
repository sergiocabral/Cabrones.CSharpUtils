using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cabrones.Utils.Reflection
{
    /// <summary>
    ///     Extensões relacionadas com: Type
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Localiza a propriedade associada a este método.
        /// </summary>
        /// <param name="method">Método.</param>
        /// <returns>Propriedade.</returns>
        public static PropertyInfo? GetProperty(this MethodInfo method)
        {
            if (method == null || method.DeclaringType == null) return null;
            return method.DeclaringType.GetProperties()
                .SingleOrDefault(a => a.GetMethod == method || a.SetMethod == method);
        }

        /// <summary>
        ///     Retorna todos os métodos apenas das propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="includeInterfaces">Inclui métodos das interfaces</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> AllProperties(this Type? type, bool includeInterfaces = false)
        {
            if (type == null) return new MethodInfo[0];

            var result = new List<MethodInfo?>();

            var index = 0;
            var types = new List<Type> {type};
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
                        .SelectMany(a => new[] {a.GetMethod, a.SetMethod})
                        .Where(a => a != null));

                if (type.BaseType != null && !types.Contains(type.BaseType)) types.Add(type.BaseType);
                if (!includeInterfaces) continue;
                foreach (var typeInterface in type.GetInterfaces())
                    if (!types.Contains(typeInterface))
                        types.Add(typeInterface);
            }

            return result!;
        }

        /// <summary>
        ///     Retorna todos os métodos que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="includeInterfaces">Inclui métodos das interfaces</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> AllMethods(this Type? type, bool includeInterfaces = false)
        {
            if (type == null) return new MethodInfo[0];

            var result = new List<MethodInfo>();

            var allProperties = AllProperties(type, includeInterfaces).ToList();

            var index = 0;
            var types = new List<Type> {type};
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
        ///     Retorna todas as interfaces e classe ancestral.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<Type> AllImplementations(this Type? type)
        {
            if (type == null) return new Type[0];

            var result = new List<Type?>();

            while (type != null)
            {
                result.AddRange(type.GetInterfaces());
                result.Add(type.BaseType);

                type = type.BaseType;
            }

            result = result.Where(a => a != null).Distinct().ToList();

            return result!;
        }

        /// <summary>
        ///     Retorna todos os métodos declaradas no tipo apenas das propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> MyProperties(this Type? type)
        {
            if (type == null) return new MethodInfo[0];

            return type.GetProperties(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static |
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly)
                .SelectMany(a => new[] {a.GetMethod, a.SetMethod})
                .Where(a => a != null && a.DeclaringType == type && a.DeclaringType.Assembly == type.Assembly)
                .ToList()!;
        }

        /// <summary>
        ///     Retorna todos os métodos declaradas no tipo que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> MyMethods(this Type? type)
        {
            if (type == null) return new MethodInfo[0];

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

        /// <summary>
        ///     Retorna todas as interfaces e classe ancestral no mesmo assembly que o tipo.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<Type> MyImplementations(this Type? type)
        {
            if (type == null) return new Type[0];

            var allImplementations = AllImplementations(type)
                .Where(a => a.Assembly == type.Assembly).ToList();

            return allImplementations;
        }

        /// <summary>
        ///     Retorna todos os métodos declaradas no tipo que não sejam herdados apenas das propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> MyOwnProperties(this Type? type)
        {
            return type.MyOwn(MyProperties, AllProperties);
        }

        /// <summary>
        ///     Retorna todos os métodos declaradas no tipo que não sejam herdados e que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> MyOwnMethods(this Type? type)
        {
            return type.MyOwn(MyMethods, AllMethods);
        }

        /// <summary>
        ///     Retorna todos os métodos declaradas no tipo que não sejam herdados e que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="methodMy">MyProperties ou MyMethods</param>
        /// <param name="methodAll">AllProperties ou AllMethods</param>
        /// <returns>Lista.</returns>
        private static IEnumerable<MethodInfo> MyOwn(this Type? type, Func<Type?, IEnumerable<MethodInfo>> methodMy,
            Func<Type?, bool, IEnumerable<MethodInfo>> methodAll)
        {
            if (type == null) return new MethodInfo[0];

            var inAncestral = new List<string?>();
            foreach (var baseType in type.GetInterfaces().Union(new[] {type.BaseType}))
                inAncestral.AddRange(methodAll(baseType, true).Select(a => a.ToString()));
            var own = methodMy(type)
                .Where(a =>
                    !a.Name.Contains(".") &&
                    !inAncestral.Contains(a.ToString()))
                .ToList();

            return own;
        }

        /// <summary>
        ///     Retorna todas as interfaces e classe ancestral somente no tipo.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<Type> MyOwnImplementations(this Type? type)
        {
            if (type == null) return new Type[0];

            var baseImplementations = type.GetInterfaces().SelectMany(AllImplementations)
                .Union(AllImplementations(type.BaseType));
            var myOwnImplementations = AllImplementations(type)
                .Except(baseImplementations)
                .Union(new[] {type.BaseType})
                .Where(a => a != null && a.Assembly == type.Assembly)
                .ToList();

            return myOwnImplementations!;
        }
    }
}