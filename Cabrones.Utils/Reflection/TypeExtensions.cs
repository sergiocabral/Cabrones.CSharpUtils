using System;
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
    }
}