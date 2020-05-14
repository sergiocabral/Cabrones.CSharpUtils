using System.Text;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Text
{
    public class EncodingCodeTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void ao_criar_deve_carregar_as_informações_do_código_solicitado(int code)
        {
            // Arrange, Given

            var encoding = Encoding.Unicode;
            var encodingName = encoding.WebName;
            var text = char.ConvertFromUtf32(code);
            var size = encoding.GetByteCount(text);

            // Act, When

            var sut = new EncodingCode(encoding, code);

            // Assert, Then

            sut.Code.Should().Be(code);
            sut.EncodingName.Should().Be(encodingName);
            sut.Text.Should().Be(text);
            sut.Size.Should().Be(size);
        }
    }
}