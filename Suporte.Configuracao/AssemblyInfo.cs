using System;
using System.Reflection;

namespace Suporte.Configuracao
{
    /// <summary>
    /// <para>Esta classe disponibiliza informações sobre um assembly.</para>
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public AssemblyInfo() : this(Assembly.GetCallingAssembly()) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="assembly">
        /// <para>Assembly de onde serão extraídas as informações.</para>
        /// <para>Poderá usar: <c>Assembly.GetExecutingAssembly()</c></para>
        /// </param>
        public AssemblyInfo(Assembly assembly)
        {
            Assembly = assembly;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Assembly de onde serão extraídas as informações.</para>
        /// </summary>
        public Assembly Assembly
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: AlgorithmId</para>
        /// </summary>
        public uint AlgorithmId
        {
            get
            {
                return ((AssemblyAlgorithmIdAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyAlgorithmIdAttribute))).AlgorithmId;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Company</para>
        /// </summary>
        public string Company
        {
            get
            {
                return ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyCompanyAttribute))).Company;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Configuration</para>
        /// </summary>
        public string Configuration
        {
            get
            {
                return ((AssemblyConfigurationAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyConfigurationAttribute))).Configuration;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Copyright</para>
        /// </summary>
        public string Copyright
        {
            get
            {
                return ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyCopyrightAttribute))).Copyright;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Culture</para>
        /// </summary>
        public string Culture
        {
            get
            {
                return ((AssemblyCultureAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyCultureAttribute))).Culture;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: DefaultAlias</para>
        /// </summary>
        public string DefaultAlias
        {
            get
            {
                return ((AssemblyDefaultAliasAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyDefaultAliasAttribute))).DefaultAlias;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: DelaySign</para>
        /// </summary>
        public bool DelaySign
        {
            get
            {
                return ((AssemblyDelaySignAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyDelaySignAttribute))).DelaySign;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Description</para>
        /// </summary>
        public string Description
        {
            get
            {
                return ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyDescriptionAttribute))).Description;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: File Version</para>
        /// </summary>
        public string FileVersion
        {
            get
            {
                return ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyFileVersionAttribute))).Version;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: AssemblyFlags</para>
        /// </summary>
        public int AssemblyFlags
        {
            get
            {
                return ((AssemblyFlagsAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyFlagsAttribute))).AssemblyFlags;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: AlgorithmId</para>
        /// </summary>
        public string InformationalVersion
        {
            get
            {
                return ((AssemblyInformationalVersionAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyInformationalVersionAttribute))).InformationalVersion;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: AlgorithmId</para>
        /// </summary>
        public string KeyFile
        {
            get
            {
                return ((AssemblyKeyFileAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyKeyFileAttribute))).KeyFile;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: AlgorithmId</para>
        /// </summary>
        public string KeyName
        {
            get
            {
                return ((AssemblyKeyNameAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyKeyNameAttribute))).KeyName;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Product</para>
        /// </summary>
        public string Product
        {
            get
            {
                return ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyProductAttribute))).Product;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Title</para>
        /// </summary>
        public string Title
        {
            get
            {
                return ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyTitleAttribute))).Title;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Trademark</para>
        /// </summary>
        public string Trademark
        {
            get
            {
                return ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyTrademarkAttribute))).Trademark;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Do assembly em execução, atributo: Product</para>
        /// </summary>
        public string Version
        {
            get
            {
                return ((AssemblyVersionAttribute)Attribute.GetCustomAttribute(Assembly,
                    typeof(AssemblyVersionAttribute))).Version;
            }
        }

    }
}
