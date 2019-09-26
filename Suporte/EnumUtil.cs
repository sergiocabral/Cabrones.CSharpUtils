using System;

namespace Suporte
{
    /// <summary>
    /// <para>Obtem informações sobre uma lista de valores (<c>enum</c>).</para>
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// <para>Converte um valor em um item de uma lista de valores (<c>enum</c>).</para>
        /// </summary>
        /// <typeparam name="TEnum">
        /// <para>Deve ser o tipo da lista de valores (<c>enum</c>).</para>
        /// </typeparam>
        /// <param name="valor">
        /// <para>Valor que será pesquisado na lista de valores (<c>enum</c>).</para>
        /// </param>
        /// <returns>
        /// <para>Item de uma lista de valores (<c>enum</c>).</para>
        /// </returns>
        public static TEnum Converter<TEnum>(object valor) where TEnum : struct
        {
            if (typeof(TEnum).BaseType != typeof(System.Enum))
            {
                throw new Exception("O tipo não é uma lista de valores derivada de System.Enum.");
            }
            try
            {
                if (valor is string && (valor as string).Length == 1)
                {
                    valor = (valor as string)[0];
                }
                if (valor.GetType() == typeof(char))
                {
                    valor = (byte)((char)valor);
                }

                if (valor is string)
                {
                    return (TEnum)System.Enum.Parse(typeof(TEnum), valor as string);
                }
                else
                {
                    return (TEnum)System.Enum.ToObject(typeof(TEnum), valor);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("O valor não foi compatível para realizar uma conversão.", ex);
            }
        }
    }
}
