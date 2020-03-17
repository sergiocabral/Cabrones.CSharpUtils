using System;
using System.Security.Cryptography;
using System.Text;

namespace Cabrones.Utils.Converter
{
    /// <summary>
    ///     Extensões relacionadas com: Guid
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        ///     Converte um valor qualquer em Guid sem disparar exception.
        ///     Para valores inválidos será usado Guid.Empty.
        /// </summary>
        /// <param name="value">Valor qualquer.</param>
        /// <param name="generate">Quando true gera um Guid baseado no valor.</param>
        /// <returns>Guid.</returns>
        public static Guid ToGuid(this object? value, bool generate = false)
        {
            if (value == null || value == DBNull.Value) return Guid.Empty;

            if (generate)
            {
                using var hashAlgorithm = MD5.Create();
                var hash = hashAlgorithm.ComputeHash(Encoding.Default.GetBytes($"{value}"));
                return new Guid(hash);
            }

            try
            {
                return Guid.Parse(value.ToString()!);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        ///     Converte um Guid em valor texto para ser usado em banco de dados.
        ///     Quando receber Guid.Empty retorna DBNull.Value,
        ///     do contrário converte para texto usando Guid.ToString("D").
        /// </summary>
        /// <param name="guid">Guid.</param>
        /// <returns>Valor para banco de dados.</returns>
        public static object ToDatabaseText(this Guid guid)
        {
            return guid != Guid.Empty
                ? (object) guid.ToString("")
                : DBNull.Value;
        }
    }
}