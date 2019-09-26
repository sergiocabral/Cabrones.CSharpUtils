using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Suporte.Validador
{
    /// <summary>
    /// <para>Esta classe disponibiliza funcionalidades relacionadas ao
    /// padrão de dados JSON.</para>
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// <para>Converte (serializa) um objeto em memória para uma string
        /// na notação JSON.</para>
        /// </summary>
        /// <typeparam name="T"><para>Tipo do objeto.</para></typeparam>
        /// <param name="objeto"><para>Objeto em meória.</para></param>
        /// <returns><para>String na notação JSON.</para></returns>
        public static string ObjetoParaString<T>(T objeto)
        {
            DataContractJsonSerializer serializador = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                serializador.WriteObject(ms, objeto);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// <para>Converte (deserializa) uma string na notação JSON para 
        /// um objeto em memória.</para>
        /// </summary>
        /// <typeparam name="T"><para>Tipo do objeto.</para></typeparam>
        /// <param name="json"><para>String na notação JSON.</para></param>
        /// <returns><para>Objeto em meória.</para></returns>
        public static T StringParaObjeto<T>(string json)
        {
            DataContractJsonSerializer serializador = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                return (T)serializador.ReadObject(ms);
            }
        }
    }
}
