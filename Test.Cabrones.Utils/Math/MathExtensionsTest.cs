using System;
using FluentAssertions;
using Xunit;

// ReSharper disable ConvertToConstant.Local

namespace Cabrones.Utils.Math
{
    public class MathExtensionsTest
    {
        [Theory]
        [InlineData(3214, "abcde", "baadce")]
        [InlineData(3214, "12345", "211435")]
        [InlineData(3214,
            @"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~", "C3")]
        [InlineData(3214, @"¡ĊǟɁɔ&Ή˜ņ_ήƷÙȰ@¦®śǮaϫ’Ƿ£?ǬŔØ|¥cΤɣΘʣ", "ǟ’¥")]
        public void testar_ConvertTo_convertendo_inteiro_para_bases_desconhecidas(ulong número, string baseNumérica,
            string númeroConvertido)
        {
            // Arrange, Given
            // Act, When

            var númeroConvertidoObtido = número.ConvertTo(baseNumérica.ToCharArray());

            // Assert, Then

            númeroConvertidoObtido.Should().Be(númeroConvertido);
        }

        [Fact]
        public void testar_ConvertTo_convertendo_inteiro_para_bases_conhecidas()
        {
            // Arrange, Given

            var númeroULong = ulong.MaxValue;
            var númeroUInt = uint.MaxValue;
            var númeroUShort = ushort.MaxValue;
            var númeroByte = byte.MaxValue;

            var basesConhecidas = new[]
            {
                "01",
                "01234567",
                "0123456789",
                "0123456789abcdef"
            };

            foreach (var basesConhecida in basesConhecidas)
            {
                // Act, When

                var númeroULongConvertido = númeroULong.ConvertTo(basesConhecida.ToCharArray());
                var númeroUIntConvertido = númeroUInt.ConvertTo(basesConhecida.ToCharArray());
                var númeroUShortConvertido = númeroUShort.ConvertTo(basesConhecida.ToCharArray());
                var númeroByteConvertido = númeroByte.ConvertTo(basesConhecida.ToCharArray());

                // Assert, Then

                if (basesConhecida.Length == 10)
                {
                    númeroULongConvertido.Should()
                        .Be(((ulong) long.Parse(Convert.ToString((long) númeroULong, 10))).ToString());
                    númeroUIntConvertido.Should().Be(uint.Parse(Convert.ToString(númeroUInt, 10)).ToString());
                    númeroUShortConvertido.Should().Be(ushort.Parse(Convert.ToString(númeroUShort, 10)).ToString());
                    númeroByteConvertido.Should().Be(byte.Parse(Convert.ToString(númeroByte, 10)).ToString());
                }
                else
                {
                    númeroULongConvertido.Should().Be(Convert.ToString((long) númeroULong, basesConhecida.Length));
                    númeroUIntConvertido.Should().Be(Convert.ToString(númeroUInt, basesConhecida.Length));
                    númeroUShortConvertido.Should().Be(Convert.ToString(númeroUShort, basesConhecida.Length));
                    númeroByteConvertido.Should().Be(Convert.ToString(númeroByte, basesConhecida.Length));
                }
            }
        }
    }
}