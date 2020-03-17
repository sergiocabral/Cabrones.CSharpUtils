using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;

namespace Cabrones.Utils.Database
{
    /// <summary>
    ///     Extensões relacionadas com: banco de dados.
    /// </summary>
    public static class DatabaseExtensions
    {
        /// <summary>
        ///     Extrai o texto da consulta SQL em um comando.
        /// </summary>
        /// <param name="command">Comando.</param>
        /// <returns>Consulta SQL.</returns>
        public static string ExtractSqlQuery(this DbCommand command)
        {
            static string FromString(object value) => 
                $"'{$"{value}".Replace("'", "''")}'";
            
            static string FromDateTime(object value) => 
                $"'{(DateTime)value:yyyy-MM-dd HH:mm:ss}'";
            
            static string FromDecimal(object value) => 
                $"{((decimal)value).ToString("0.00", CultureInfo.InvariantCulture)}";
            
            static string FromInteger(object value) => 
                $"{value}";
            
            static string FromBoolean(object value) => 
                (bool) value ? "1" : "0";
            
            var sql = new StringBuilder(command.CommandText);

            foreach (var p in command.Parameters)
            {
                if (!(p is DbParameter parameter)) continue;

                var value = parameter.DbType switch
                {
                    DbType.AnsiStringFixedLength => FromString(parameter.Value),
                    DbType.StringFixedLength => FromString(parameter.Value),
                    DbType.String => FromString(parameter.Value),
                    DbType.Object => FromString(parameter.Value),
                    DbType.Guid => FromString(parameter.Value),
                    DbType.AnsiString => FromString(parameter.Value),
                    DbType.Binary => FromString(parameter.Value),
                    DbType.Xml => FromString(parameter.Value),
                    DbType.Time => FromDateTime(parameter.Value),
                    DbType.Date => FromDateTime(parameter.Value),
                    DbType.DateTime => FromDateTime(parameter.Value),
                    DbType.DateTime2 => FromDateTime(parameter.Value),
                    DbType.DateTimeOffset => FromDateTime(parameter.Value),
                    DbType.Currency => FromDecimal(parameter.Value),
                    DbType.Decimal => FromDecimal(parameter.Value),
                    DbType.Double => FromDecimal(parameter.Value),
                    DbType.Int16 => FromInteger(parameter.Value),
                    DbType.Int32 => FromInteger(parameter.Value),
                    DbType.Int64 => FromInteger(parameter.Value),
                    DbType.SByte => FromInteger(parameter.Value),
                    DbType.Single => FromInteger(parameter.Value),
                    DbType.UInt16 => FromInteger(parameter.Value),
                    DbType.UInt32 => FromInteger(parameter.Value),
                    DbType.UInt64 => FromInteger(parameter.Value),
                    DbType.VarNumeric => FromInteger(parameter.Value),
                    DbType.Byte => FromInteger(parameter.Value),
                    DbType.Boolean => FromBoolean(parameter.Value),
                    _ => FromString(parameter.Value)
                };
                sql.Replace($"@{parameter.ParameterName}", $"{value} /* @{parameter.ParameterName} */");
            }

            return sql.ToString();
        }
    }
}