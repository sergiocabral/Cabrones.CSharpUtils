using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Cabrones.Utils.Reflection
{
    /// <summary>
    /// Extensões relacionadas com: assinatura de declarações 
    /// </summary>
    public static class SignatureExtensions
    {
        /// <summary>
        /// Formata o objeto em uma assinatura C#
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <returns>Assinatura.</returns>
        public static string ToSignatureCSharp(this Type type)
        {
            string AdjustGeneric(string text)
            {
                if (!Regex.IsMatch(text, @"`\d+\[")) return text;

                var result = new StringBuilder(Regex.Replace(text, @"`\d+\[", "<"));
                var opened = 0;
                foreach (Match? match in Regex.Matches(result.ToString(), @"(\[|\])"))
                {
                    switch (match?.Value)
                    {
                        case "[":
                            opened++;
                            break;
                        case "]" when opened > 0:
                            opened--;
                            break;
                        case "]":
                            result.Remove(match.Index, 1);
                            result.Insert(match.Index, ">");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }

                return result.ToString();
            }

            string RemoveLongNames(string text) =>
                Regex.Replace(text, @"(\w+\.|`\d+)", string.Empty);
            
            string SeparateComma(string text) =>
                Regex.Replace(text, @",(?=\w)", ", ");
            
            return
                SeparateComma(
                RemoveLongNames(
                    AdjustGeneric(
                        type.ToString())));
        }
        
        /// <summary>
        /// Formata o objeto em uma assinatura C#
        /// </summary>
        /// <param name="method">Método.</param>
        /// <returns>Assinatura.</returns>
        public static string ToSignatureCSharp(this MethodInfo method)
        {
            var parametersForGeneric = method.GetGenericArguments().ToList();
            var parameters = method.GetParameters().ToList();

            var result = new StringBuilder();

            result.Append($"{method.ReturnType.ToSignatureCSharp()} {method.Name}");

            if (parametersForGeneric.Count > 0)
            {
                result.Append(
                    $"<{parametersForGeneric.Select(ToSignatureCSharp).Aggregate((acumulador, nomeDoTipo) => $"{(string.IsNullOrWhiteSpace(acumulador) ? "" : $"{acumulador}, ")}{nomeDoTipo}")}>");
            }

            result.Append(parameters.Count > 0
                ? $"({parameters.Select(a => $"{a.ParameterType.ToSignatureCSharp()}{(a.HasDefaultValue ? $" = {(a.DefaultValue == null ? "null" : a.ParameterType == typeof(char) && ((char) 0).Equals(a.DefaultValue) ? "''" : $"'{a.DefaultValue}'")}" : "")}").Aggregate((acumulador, nomeDoTipo) => $"{(string.IsNullOrWhiteSpace(acumulador) ? "" : $"{acumulador}, ")}{nomeDoTipo}")})"
                : "()");

            return result.ToString();
        }
        
        /// <summary>
        /// Formata o objeto em uma assinatura C#
        /// </summary>
        /// <param name="property">Propriedade.</param>
        /// <returns>Assinatura.</returns>
        public static string ToSignatureCSharp(this PropertyInfo property)
        {
            var result = new StringBuilder();

            result.Append($"{property.PropertyType.ToSignatureCSharp()} {property.Name} {{ ");
            if (property.CanRead) result.Append("get; ");
            if (property.CanWrite) result.Append("set; ");
            result.Append("}");
            
            return result.ToString();
        }
    }
}