using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Database
{
    public class DatabaseExtensionsTest
    {
        [Fact]
        public void método_ExtractSqlQuery_deve_transformar_DbCommand_em_texto()
        {
            // Arrange, Given

            using var connection = (DbConnection) new SQLiteConnection();
            using var comando = connection.CreateCommand();
            comando.CommandText = @"
SELECT * 
  FROM Tabela 
 WHERE Id = @id
   AND Int = @int
   AND Float = @float
   AND Date = @date
   AND Bool = @bool
";

            comando.Parameters.Add(comando.CreateParameter());
            comando.Parameters[^1].ParameterName = "id";
            comando.Parameters[^1].Value = Guid.NewGuid();

            comando.Parameters.Add(comando.CreateParameter());
            comando.Parameters[^1].ParameterName = "int";
            comando.Parameters[^1].Value = long.MaxValue;

            comando.Parameters.Add(comando.CreateParameter());
            comando.Parameters[^1].ParameterName = "float";
            comando.Parameters[^1].Value = decimal.MaxValue;

            comando.Parameters.Add(comando.CreateParameter());
            comando.Parameters[^1].ParameterName = "date";
            comando.Parameters[^1].Value = DateTime.Now;

            comando.Parameters.Add(comando.CreateParameter());
            comando.Parameters[^1].ParameterName = "bool";
            comando.Parameters[^1].Value = true;

            var sqlEsperado = comando.CommandText
                .Replace(
                    $"@{comando.Parameters[0].ParameterName}",
                    $"'{comando.Parameters[0].Value}' /* @{comando.Parameters[0].ParameterName} */")
                .Replace(
                    $"@{comando.Parameters[1].ParameterName}",
                    $"{comando.Parameters[1].Value} /* @{comando.Parameters[1].ParameterName} */")
                .Replace(
                    $"@{comando.Parameters[2].ParameterName}",
                    $"{((decimal) comando.Parameters[2].Value).ToString("0.00", CultureInfo.InvariantCulture)} /* @{comando.Parameters[2].ParameterName} */")
                .Replace(
                    $"@{comando.Parameters[3].ParameterName}",
                    $"'{comando.Parameters[3].Value:yyyy-MM-dd HH:mm:ss}' /* @{comando.Parameters[3].ParameterName} */")
                .Replace(
                    $"@{comando.Parameters[4].ParameterName}",
                    $"{((bool) comando.Parameters[4].Value ? "1" : "0")} /* @{comando.Parameters[4].ParameterName} */");

            // Act, When

            var sql = comando.ExtractSqlQuery();

            // Assert, Then

            sql.Should().Be(sqlEsperado);
        }
    }
}