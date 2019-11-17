﻿using System;
using System.Collections;
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
        /// <returns>Lista.</returns>
        public static IEnumerable<MethodInfo> OnlyProperties(this Type type, BindingFlags bindingFlags)
        {
            if (type == null) return new MethodInfo[0];
            var allProperties = type.GetProperties(bindingFlags);
            return
                allProperties.Where(a => a.GetMethod != null).Select(a => a.GetMethod).Union(
                        allProperties.Where(a => a.SetMethod != null).Select(a => a.SetMethod));
        }
    }
}