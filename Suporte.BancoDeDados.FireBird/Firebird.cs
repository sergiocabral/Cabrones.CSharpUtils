using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Suporte.BancoDeDados.FireBird
{
    /// <summary>
    /// <para>Agrupa funções de operação para banco de dados tipo Firebird.</para>
    /// </summary>
    public class Firebird
    {
        /// <summary>
        /// <para>Cria um banco de dados vazio.</para>
        /// </summary>
        /// <param name="database"><para>Caminho do arquivo do banco de dados.</para></param>
        /// <param name="pageSize"><para>Valor do PAGE_SIZE.</para></param>
        /// <param name="charset"><para>Valor do DEFAULT CHARACTER SET.</para></param>
        /// <param name="sql"><para>Código SQL executado logo após criar o banco de dados.</para></param>
        public static void Criar(string database, int pageSize, string charset, string sql)
        {
            if (new FileInfo(database).Exists)
            {
                throw new IOException(string.Format("O arquivo '{0}' já existe.", database));
            }

            StringBuilder sqlCompleto = new StringBuilder();
            sqlCompleto.AppendFormat("CREATE DATABASE '{0}' ", database);
            sqlCompleto.AppendFormat("PAGE_SIZE {0} ", pageSize);
            sqlCompleto.AppendFormat("DEFAULT CHARACTER SET {0} ", charset);
            sqlCompleto.AppendLine(";");

            if (!string.IsNullOrWhiteSpace(sql))
            {
                sqlCompleto.AppendLine(sql);
            }

            FileInfo scriptSql = new FileInfo(Path.GetTempFileName());            
            using (StreamWriter stream = scriptSql.CreateText())
            {
                stream.Write(sqlCompleto);
            }

            Process process = new Process();
            process.StartInfo.FileName = "isql.exe";
            process.StartInfo.Arguments = string.Format("-i \"{0}\"", scriptSql.FullName);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();

            try
            {
                scriptSql.Delete();
            }
            catch (Exception) { }

            if (!new FileInfo(database).Exists)
            {
                throw new ArgumentException("Parâmetros incorretos. O banco de dados não foi criado.");
            }
        }

    }
}
