using System;
using FirebirdSql.Data.FirebirdClient;
using Suporte.BancoDeDados.Criterio;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Suporte.BancoDeDados.FireBird
{
    /// <summary>
    /// <para>Classe capaz de prover informações sobre os comandos SQL 
    /// específicos para o banco de dados FireBird</para>
    /// </summary>
    public static class SintaxeSQLFireBird
    {
        /// <summary>
        /// <para>Retorna o comando SQL que declara um tipo do banco de dados.</para>
        /// </summary>
        /// <param name="campo">
        /// <para>Informações do campo.</para>
        /// </param>
        /// <returns>
        /// <para>Comando SQL que declara um tipo.</para>
        /// </returns>
        public static string ObterTipoDeCampo(ICampoDeBancoDeDados campo)
        {
            if (campo.Tipo == typeof(bool))
            {
                throw new Exception(string.Format("Tipo de dados não suportado pelo FireBird: {0}", campo.Tipo.Name));
            }
            else if (campo.Tipo == typeof(byte) ||
                     campo.Tipo == typeof(sbyte) ||
                     campo.Tipo == typeof(short))
            {
                return string.Format("SMALLINT");
            }
            else if (campo.Tipo == typeof(int) ||
                     campo.Tipo == typeof(ushort))
            {
                return string.Format("INTEGER");
            }
            else if (campo.Tipo == typeof(long) ||
                     campo.Tipo == typeof(double))
            {
                return string.Format("BIGINT");
            }
            else if (campo.Tipo == typeof(float) ||
                     campo.Tipo == typeof(uint) ||
                     campo.Tipo == typeof(ulong))
            {
                return string.Format("NUMERIC({0},{1})", campo.Tamanho, campo.Precisao);
            }
            else if (campo.Tipo == typeof(char))
            {
                return string.Format("CHAR(1)");
            }
            else if (campo.Tipo == typeof(string))
            {
                return string.Format("VARCHAR({0})", campo.Tamanho);
            }
            else if (campo.Tipo == typeof(DateTime))
            {
                return string.Format("TIMESTAMP");
            }
            else
            {                
                return string.Format("BLOB");
            }
        }

        /// <summary>
        /// <para>A partir de um dado retorna o 
        /// tipo de dados correspondente para o banco de dados FireBird.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Dado.</para>
        /// </param>
        /// <returns>
        /// <para>Tipo de dados correspondente para o banco de dados FireBird.</para>
        /// </returns>
        public static FbDbType ConverterParaTipoFireBird(object valor)
        {
            return ConverterParaTipoFireBird(valor.GetType());
        }

        /// <summary>
        /// <para>A partir de um tipo de dados retorna o 
        /// tipo de dados correspondente para o banco de dados FireBird.</para>
        /// </summary>
        /// <param name="tipo">
        /// <para>Tipo de dados.</para>
        /// </param>
        /// <returns>
        /// <para>Tipo de dados correspondente para o banco de dados FireBird.</para>
        /// </returns>
        public static FbDbType ConverterParaTipoFireBird(Type tipo)
        {
            switch (CampoDeBancoDeDados<object>.ObterTipoParaBancoDeDados(tipo))
            {
                case DbType.Boolean: 
                    return FbDbType.Boolean;
                case DbType.Byte:
                case DbType.SByte:
                case DbType.Int16:
                    return FbDbType.SmallInt;
                case DbType.Int32:
                case DbType.UInt16:
                    return FbDbType.Integer;
                case DbType.Int64:
                case DbType.Double:
                    return FbDbType.BigInt;
                case DbType.Decimal:
                case DbType.UInt32:
                case DbType.UInt64:
                    return FbDbType.Numeric;
                case DbType.String:
                    return FbDbType.VarChar;
                case DbType.DateTime:
                    return FbDbType.TimeStamp;
                default:
                    return FbDbType.Binary;
            }
        }

        /// <summary>
        /// <para>Preenche uma coleção de parâmetro do FireBird.</para>
        /// </summary>
        /// <param name="fbParameterCollection">
        /// <para>Coleção de parâmetro do FireBird que será preenchida.</para>
        /// </param>
        /// <param name="criterio">
        /// <para>Critérios do banco de dados com os parâmetros e seus valores.</para>
        /// </param>
        public static void PreencherParametros(FbParameterCollection fbParameterCollection, ICriterioFiltro criterio)
        {
            if (criterio != null)
            {
                foreach (KeyValuePair<string, object> parametro in criterio.Parametros)
                {
                    fbParameterCollection.Add(parametro.Key, ConverterParaTipoFireBird(parametro.Value)).Value = parametro.Value;
                }
            }
        }
    }
}
