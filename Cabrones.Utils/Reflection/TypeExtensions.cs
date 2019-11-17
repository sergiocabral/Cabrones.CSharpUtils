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
        /// Verifica se uma declaração pertence diretamente a um tipo.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="methodInfo">Declaração.</param>
        /// <returns>Resultado.</returns>
        public static bool IsOwnOfType(this MemberInfo methodInfo, Type type) =>
            methodInfo.DeclaringType == type &&
            methodInfo.DeclaringType?.Assembly == type.Assembly;

        /// <summary>
        /// Retorna os métodos apenas das propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="bindingFlags">Filtro.</param>
        /// <param name="ignoreDeclaringType">Não retorna declarações pertencentes aos itens dessa lista.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> OnlyProperties(this Type type, BindingFlags bindingFlags, params Type[] ignoreDeclaringType)
        {
            if (type == null) return new MethodInfo[0];
            var allProperties = type.GetProperties(bindingFlags);
            return
                allProperties.Where(a => a.GetMethod != null).Select(a => a.GetMethod).Union(
                    allProperties.Where(a => a.SetMethod != null).Select(a => a.SetMethod))
                    .Where(a => !ignoreDeclaringType.Contains(a.DeclaringType));
        }

        /// <summary>
        /// Retorna os métodos que não sejam de propriedades.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="bindingFlags">Filtro.</param>
        /// <param name="ignoreDeclaringType">Não retorna declarações pertencentes aos itens dessa lista.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> OnlyMethods(this Type type, BindingFlags bindingFlags, params Type[] ignoreDeclaringType)
        {
            return type == null
                ? new MethodInfo[0]
                : type.GetMethods(bindingFlags).Where(a => 
                    !ignoreDeclaringType.Contains(a.DeclaringType) && 
                    !type.OnlyProperties(bindingFlags).Contains(a));
        }

        /// <summary>
        /// Retorna os métodos apenas das propriedades somente do tipo.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="bindingFlags">Filtro.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> OnlyMyProperties(this Type type, BindingFlags bindingFlags)
        {
            if (type == null) return new MethodInfo[0];
            var allProperties = type.GetProperties(bindingFlags);
            return
                allProperties.Where(a => a.GetMethod != null).Select(a => a.GetMethod).Union(
                        allProperties.Where(a => a.SetMethod != null).Select(a => a.SetMethod))
                    .Where(a => a.DeclaringType == type);
        }

        /// <summary>
        /// Retorna os métodos que não sejam de propriedades somente do tipo.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="bindingFlags">Filtro.</param>
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> OnlyMyMethods(this Type type, BindingFlags bindingFlags)
        {
            return type == null
                ? new MethodInfo[0]
                : type.GetMethods(bindingFlags).Where(a => 
                    a.DeclaringType == type && 
                    !type.OnlyProperties(bindingFlags).Contains(a));
        }
    }
}