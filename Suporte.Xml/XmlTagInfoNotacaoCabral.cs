using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Esta classe armazena a referência para uma tag num documento Xml.</para>
    /// <para>Esta classe usa a notação Cabral que visa referênciar as tags dentro do 
    /// documentos Xml da seguinte maneira: <c>config[0].app[2].valor[0]</c>. Neste caso 
    /// o nome da tag é seguido do índice (base zero) referente 
    /// a sua posição no documento Xml.</para>
    /// <para>Também é possível referenciar mais de um índice usando
    /// o seguinte formato: <c>config[0].app[1-3,9,30].valor[0]</c>. 
    /// Neste caso, dentro da primeira tag <c>config</c> são referenciados as 
    /// tags <c>app</c> da 1º até a 3º posição e também a 9º e 30º tag.</para>
    /// <para>Para referenciar todas as tags use: <c>config[0].app[1-3,9,30].valor[]</c>.
    /// Dentro de todas as tags <c>app</c> encontradas pelos seus índices informados, 
    /// serão retornadas todas as tags existentes de <c>valor</c>, ou seja, todas as posições.</para>
    /// <para>Ao criar novas tags poderá usar: <c>config[0].app[1-3,9,30].valor[*]</c>.
    /// Dentro de todas as tags <c>app</c> encontradas pelos seus índices informados, 
    /// será criada uma tag <c>valor</c> na última posição.</para>
    /// </summary>
    public class XmlTagInfoNotacaoCabral : IXmlTagInfo
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public XmlTagInfoNotacaoCabral() : this(string.Empty) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        public XmlTagInfoNotacaoCabral(string caminhoDeTags) : this(caminhoDeTags, EnumXmlTagModo.IndiceUnico | EnumXmlTagModo.IndiceMultiplo | EnumXmlTagModo.IndiceNaoInformado | EnumXmlTagModo.IndiceNovo) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="modo">
        /// <para>Informa os modos permitidos para o caminho de tags a partir desta tag.</para>
        /// </param>
        public XmlTagInfoNotacaoCabral(string caminhoDeTags, EnumXmlTagModo modo)
        {
            Carregar(caminhoDeTags, modo);
        }

        /// <summary>
        /// <para>Lê o caminho de tags e define as propriedades desta classe.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="modo">
        /// <para>Informa os modos permitidos para o caminho de tags a partir desta tag.</para>
        /// </param>
        public void Carregar(string caminhoDeTags, EnumXmlTagModo modo)
        {
            this.modo = modo;

            if (string.IsNullOrEmpty(caminhoDeTags))
            {
                DefinirPropriedadesComValoresPadrao();
            }
            else
            {
                Match match = Regex.Match(caminhoDeTags, @"(^[^\[]*\[[0-9\*,-]*?\])");
                if (!string.IsNullOrEmpty(match.Value))
                {
                    string tag = match.Value.Substring(0, match.Value.IndexOf("["));
                    string indices = match.Value.Substring(match.Value.IndexOf("[") + 1).Replace("]", string.Empty);
                    NomeDaTag = tag;
                    IndicesTexto = indices;

                    try
                    {
                        string caminhoRestante = caminhoDeTags.Remove(0, match.Value.Length + 1);
                        ProximoXmlTagInfo = new XmlTagInfoNotacaoCabral(caminhoRestante, Modo);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        ProximoXmlTagInfo = null;
                    }
                }
                else
                {
                    throw new XmlException("Referência com sintaxe inválida.");
                }
            }
        }

        /// <summary>
        /// <para>Atribui os valores padrão para as propriedades desta classe.</para>
        /// </summary>
        private void DefinirPropriedadesComValoresPadrao()
        {
            nomeDaTag = string.Empty;
            indices = new uint[] { };
        }

        /// <summary>
        /// <para>Retorna uma representação da instância da classe em formato de texto.</para>
        /// </summary>
        /// <returns>
        /// <para>string</para>
        /// </returns>
        public override string ToString()
        {
            return ReferenciaDasTagsComIndicesCompleto;
        }

        #region IXmlTagInfo Members

        private EnumXmlTagModo modo;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Descreve os modos permitidos para o caminho de tags a partir desta tag.</para>
        /// </summary>
        public EnumXmlTagModo Modo
        {
            get
            {
                return modo;
            }
        }

        private string nomeDaTag;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome desta tag.</para>
        /// </summary>
        public string NomeDaTag
        {
            get
            {
                return nomeDaTag;
            }
            set
            {
                nomeDaTag = value;
            }
        }

        private uint[] indices;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Lista dos índices (base zero) informando as posições da tag.</para>
        /// <para>Quando o valor é um array vazio (<c>new uint[] { }</c>) indica que todas as tags foram referenciadas.</para>
        /// <para>Quando o valor é <c>null</c> indica que a tag não existe e será criada.</para>
        /// </summary>
        public uint[] Indices
        {
            get
            {
                return indices;
            }
            set
            {
                indices = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Lista dos índices (base zero) informando as posições da tag.</para>
        /// <para>Esta propriedade usa texto livre para receber os índices.</para>
        /// </summary>
        public string IndicesTexto
        {
            get
            {
                if (Indices == null)
                {
                    return "*";
                }

                StringBuilder result = new StringBuilder(Indices.Length * 4);
                bool acumulado = false;
                for (int i = 0; i < Indices.Length; i++)
                {
                    if (i < Indices.Length - 1 && Indices[i] == Indices[i + 1] - 1)
                    {
                        if (!acumulado)
                        {
                            acumulado = true;
                            result.Append(",");
                            result.Append(Indices[i]);
                            result.Append("-");
                        }
                        continue;
                    }
                    else if (acumulado)
                    {
                        acumulado = false;
                        result.Append(Indices[i]);
                        continue;
                    }
                    result.Append(",");
                    result.Append(Indices[i]);
                }

                if (result.Length > 0)
                {
                    result.Remove(0, 1);
                }
                return result.ToString();
            }
            set
            {
                uint[] indiceEx;
                string valor = value;
                if (valor == string.Empty)
                {
                    indiceEx = new uint[] { };
                }
                else
                {
                    if (valor == null || (valor != "*" && Regex.Replace(valor, "([0-9]|(?<=[0-9])[,-](?=[0-9]))", string.Empty) != string.Empty))
                    {
                        throw new XmlException("Formato dos índices é inválido.");
                    }

                    if (valor == "*")
                    {
                        indiceEx = null;
                    }
                    else
                    {
                        List<uint> novoIndice = new List<uint>();
                        MatchCollection matches = Regex.Matches(valor, "(-|[0-9]*)");
                        bool acumulado = false;
                        foreach (Match match in matches)
                        {
                            if (!string.IsNullOrEmpty(match.Value))
                            {
                                if (match.Value == "-")
                                {
                                    acumulado = true;
                                }
                                else
                                {
                                    uint? valorUltimo = novoIndice.Count == 0 ? null : (uint?)novoIndice[novoIndice.Count - 1];
                                    uint valorAtual = Convert.ToUInt32(match.Value);
                                    if (valorUltimo != null && valorUltimo.Value >= valorAtual)
                                    {
                                        throw new XmlException("Índice em ordem não crescente.");
                                    }
                                    if (acumulado)
                                    {
                                        acumulado = false;
                                        for (uint i = valorUltimo.Value + 1; i <= valorAtual; i++)
                                        {
                                            novoIndice.Add(i);
                                        }
                                    }
                                    else
                                    {
                                        novoIndice.Add(valorAtual);
                                    }
                                }
                            }
                        }
                        indiceEx = novoIndice.ToArray();
                    }
                }

                if (indiceEx != null && ((Modo & EnumXmlTagModo.IndiceNaoInformado) != EnumXmlTagModo.IndiceNaoInformado) && indiceEx.Length == 0)
                {
                    throw new XmlException("Não foi permitido o uso de índice não informado.");
                }
                else if (indiceEx != null && (Modo & EnumXmlTagModo.IndiceUnico) != EnumXmlTagModo.IndiceUnico && indiceEx.Length == 1)
                {
                    throw new XmlException("Não foi permitido o uso de índice único.");
                }
                else if (indiceEx != null && (Modo & EnumXmlTagModo.IndiceMultiplo) != EnumXmlTagModo.IndiceMultiplo && indiceEx.Length > 1)
                {
                    throw new XmlException("Não foi permitido o uso de índices múltilpos.");
                }
                else if (indiceEx == null && (Modo & EnumXmlTagModo.IndiceNovo) != EnumXmlTagModo.IndiceNovo)
                {
                    throw new XmlException("Não foi permitido o uso de índices novos.");
                }
                indices = indiceEx;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Com base nos atributos <see cref="NomeDaTag"/> e <see cref="Indices"/>
        /// monta a exibição da referência para as tags Xml.</para>
        /// </summary>
        public string ReferenciaDasTagsComIndices
        {
            get
            {
                if (string.IsNullOrEmpty(NomeDaTag))
                {
                    return string.Empty;
                }
                else
                {
                    return NomeDaTag + "[" + IndicesTexto + "]";
                }
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Com base nos atributos <see cref="NomeDaTag"/> e <see cref="Indices"/>
        /// monta a exibição da referência para as tags Xml.</para>
        /// <para>Será concatenado com a referência de todas as sub tags acessadas
        /// pela propriedade <see cref="ProximoXmlTagInfo"/></para>
        /// </summary>
        public string ReferenciaDasTagsComIndicesCompleto
        {
            get
            {
                return ReferenciaDasTagsComIndices + (ProximoXmlTagInfo == null ? string.Empty : "." + ProximoXmlTagInfo.ReferenciaDasTagsComIndicesCompleto);
            }
        }

        private IXmlTagInfo proximoXmlTagInfo;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Permite armazenar uma referência para a próxima tag Xml.</para>
        /// </summary>
        public IXmlTagInfo ProximoXmlTagInfo
        {
            get
            {
                return proximoXmlTagInfo;
            }
            set
            {
                IXmlTagInfo xmlTagInfo = value;
                while (xmlTagInfo != null)
                {
                    if (xmlTagInfo == this)
                    {
                        throw new XmlException("Ciclo infinito não permitido.");
                    }
                    xmlTagInfo = xmlTagInfo.ProximoXmlTagInfo;
                }
                proximoXmlTagInfo = value;
            }
        }

        #endregion

    }
}
