using System;
using Cabrones.Test;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Converter
{
    public class GuidExtensionsTest
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

        [Theory]
        [InlineData("f52eeec5-6290-4a9e-b17b-794f2d516fda", "f52eeec5-6290-4a9e-b17b-794f2d516fda")]
        [InlineData("00000000-0000-0000-0000-000000000000", null)]
        public void deve_converter_um_Guid_para_texto_que_possa_ser_gravado_em_campo_de_banco_de_dados(string guid,
            string resultadoEsperado)
        {
            // Arrange, Given
            // Act, When

            var valor = Guid.Parse(guid).ToDatabaseText();

            // Assert, Then

            if (resultadoEsperado != null)
                valor.Should().Be(resultadoEsperado);
            else
                valor.Should().Be(DBNull.Value);
        }

        [Fact]
        public void a_geração_de_Guid_baseada_no_valor_do_objeto_deve_ser_única_para_o_mesmo_valor()
        {
            // Arrange, Given

            var valor = this.Fixture<string>();

            // Act, When

            var guid1 = valor.ToGuid(true);
            var guid2 = valor.ToGuid(true);

            // Assert, Then

            guid1.Should().NotBeEmpty();
            guid1.Should().Be(guid2);
        }

        [Fact]
        public void converter_null_ou_DBNullValue_para_GuidEmpty_deve_ser_feito_sem_GuidParse()
        {
            // Arrange, Given
            // Act, When

            var (tempoParaTextoVálido, _) =
                new Func<Guid>(() => "f52eeec5-6290-4a9e-b17b-794f2d516fda".ToGuid()).StopwatchFor();
            var (tempoParaTextoInválido, _) = new Func<Guid>(() => "inválido".ToGuid()).StopwatchFor();
            // ReSharper disable once InconsistentNaming
            var (tempoParaDBNullValue, _) = new Func<Guid>(() => DBNull.Value.ToGuid()).StopwatchFor();
            var (tempoParaNull, _) = new Func<Guid>(() => ((string) null).ToGuid()).StopwatchFor();

            // Assert, Then

            tempoParaDBNullValue.Should().BeLessThan(tempoParaTextoVálido);
            tempoParaDBNullValue.Should().BeLessThan(tempoParaTextoInválido);
            tempoParaNull.Should().BeLessThan(tempoParaTextoVálido);
            tempoParaNull.Should().BeLessThan(tempoParaTextoInválido);
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

        [Fact]
        public void deve_converter_null_para_GuidEmpty()
        {
            // Arrange, Given
            // Act, When

            var guid = ((string) null).ToGuid();

            // Assert, Then

            guid.Should().BeEmpty();
        }
    }
}