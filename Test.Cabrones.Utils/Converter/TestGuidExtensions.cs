using System;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Converter
{
    public class TestGuidExtensions
    {
        [Theory]
        [InlineData(null, "00000000-0000-0000-0000-000000000000")]
        [InlineData("inválido", "00000000-0000-0000-0000-000000000000")]
        [InlineData("F52EEEC5-6290-4A9E-B17B-794F2D516FDA", "f52eeec5-6290-4a9e-b17b-794f2d516fda")]
        [InlineData("f52eeec5-6290-4a9e-b17b-794f2d516fda", "f52eeec5-6290-4a9e-b17b-794f2d516fda")]
        [InlineData("f52eeec562904a9eb17b794f2d516fda", "f52eeec5-6290-4a9e-b17b-794f2d516fda")]
        [InlineData("(f52eeec5-6290-4a9e-b17b-794f2d516fda)", "f52eeec5-6290-4a9e-b17b-794f2d516fda")]
        [InlineData("{f52eeec5-6290-4a9e-b17b-794f2d516fda}", "f52eeec5-6290-4a9e-b17b-794f2d516fda")]
        public void deve_converter_qualquer_objeto_para_Guid_sem_disparar_exception(object valor, string guidEsperado)
        {
            // Arrange, Given
            // Act, When

            var guid = valor.ToGuid();

            // Assert, Then

            guid.ToString("D").Should().Be(guidEsperado);
        }
        
        [Fact]
        public void deve_converter_DBNullValue_para_GuidEmpty()
        {
            // Arrange, Given
            // Act, When

            var guid = DBNull.Value.ToGuid();

            // Assert, Then

            guid.Should().BeEmpty();
        }

        [Theory]
        [InlineData("f52eeec5-6290-4a9e-b17b-794f2d516fda", "f52eeec5-6290-4a9e-b17b-794f2d516fda")]
        [InlineData("00000000-0000-0000-0000-000000000000", null)]
        public void deve_converter_um_Guid_para_texto_que_possa_ser_gravado_em_campo_de_banco_de_dados(string guid, string resultadoEsperado)
        {
            // Arrange, Given
            // Act, When

            var valor = Guid.Parse(guid).ToDatabaseText();

            // Assert, Then

            if (resultadoEsperado != null)
            {
                valor.Should().Be(resultadoEsperado);
            }
            else
            {
                valor.Should().Be(DBNull.Value);
            }
        }
    }
}