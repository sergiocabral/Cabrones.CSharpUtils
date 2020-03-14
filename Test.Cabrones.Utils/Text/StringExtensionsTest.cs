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
    }
}