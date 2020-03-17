using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Text
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("teste", "teste")]
        [InlineData("forçação", "forcacao")]
        [InlineData("forçação de barra", "forcacao-de-barra")]
        [InlineData("    forçação     de     barra    ", "forcacao-de-barra")]
        [InlineData("forcacao-de-barra", "forcacao-de-barra")]
        [InlineData("--forcacao--de--barra--", "forcacao-de-barra")]
        [InlineData("!forcacao de barra?", "forcacao-de-barra")]
        public void método_Slug_deve_converter_um_texto_para_slug(string valor, string slugEsperado)
        {
            // Arrange, Given
            // Act, When

            var slug = valor.ToSlug();

            // Assert, Then

            slug.Should().Be(slugEsperado);
        }

        [Theory]
        [InlineData("forçação", "forcacao")]
        [InlineData("forçação?", "forcacao?")]
        public void método_RemoveAccent_deve_remover_acentos_do_texto(string texto, string textoEsperado)
        {
            // Arrange, Given
            // Act, When

            var valor = texto.RemoveAccent();

            // Assert, Then

            valor.Should().Be(textoEsperado);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("  ", "  ")]
        [InlineData("Texto Comum", "Texto Comum")]
        [InlineData(null, null, 1, 2, 3, 4)]
        [InlineData("", "", 1, 2, 3, 4)]
        [InlineData("  ", "  ", 1, 2, 3, 4)]
        [InlineData("Texto Comum", "Texto Comum", 1, 2, 3, 4)]
        [InlineData("Variável {0}", "Variável Aqui", "Aqui")]
        [InlineData("Variável {0} ou {aqui}", "Variável Aqui ou Aqui Também", "Aqui", "Aqui Também")]
        [InlineData("Variável {0} ou {aqui} mas aqui {{não}, {não}}, {{não}}, {}, {{}}",
            "Variável Aqui ou Aqui Também mas aqui {{não}, {não}}, {{não}}, {}, {{}}", "Aqui", "Aqui Também")]
        [InlineData("Variável {zero}, {um} e {dois}", "Variável ok, {um} e {dois}", "ok")]
        [InlineData("Variável {0}, {1} e {2}", "Variável ok, {1} e {2}", "ok")]
        public void método_QueryString_por_array_deve_substituir_argumentos_no_texto(string máscara, string textoEsperado,
            params object[] args)
        {
            // Arrange, Given
            // Act, When

            var valor = máscara.QueryString(args);

            // Assert, Then

            valor.Should().Be(textoEsperado);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("  ", "  ")]
        [InlineData("Texto Comum", "Texto Comum")]
        [InlineData(null, null, "key1=1", "key2=2", "key3=3", "key4=4")]
        [InlineData("", "", "key1=1", "key2=2", "key3=3", "key4=4")]
        [InlineData("  ", "  ", "key1=1", "key2=2", "key3=3", "key4=4")]
        [InlineData("Texto Comum", "Texto Comum", "key1=1", "key2=2", "key3=3", "key4=4")]
        [InlineData("Variável {0}", "Variável Aqui", "0=Aqui")]
        [InlineData("Variável {aqui}", "Variável Aqui", "aqui=Aqui")]
        [InlineData("Variável {aqui-não}", "Variável {aqui-não}", "aqui=Aqui")]
        [InlineData("Variável {var1} - {var2}", "Variável {var1} - hahaha", "var2=hahaha", "var3=rsrsrs")]
        public void método_QueryString_por_dicionário_deve_substituir_argumentos_no_texto(string máscara, string textoEsperado,
            params string[] args)
        {
            // Arrange, Given

            var dicionário = args.ToDictionary(
                key => key.Substring(0, key.IndexOf("=")),
                value => value.Substring(value.IndexOf("=") + 1));
            
            // Act, When

            var valor = máscara.QueryString(dicionário);

            // Assert, Then

            valor.Should().Be(textoEsperado);
        }
    }
}