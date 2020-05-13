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
        public void testar_ConvertToNumericBase_convertendo_inteiro_para_bases_desconhecidas(ulong número,
            string baseNumérica,
            string númeroConvertido)
        {
            // Arrange, Given
            // Act, When

            var númeroConvertidoObtido = número.ConvertToNumericBase(baseNumérica.ToCharArray());

            // Assert, Then

            númeroConvertidoObtido.Should().Be(númeroConvertido);
        }

        [Theory]
        [InlineData("baadce", "abcde", 3214)]
        [InlineData("211435", "12345", 3214)]
        [InlineData("C3",
            @"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~", 3214)]
        [InlineData("ǟ’¥", @"¡ĊǟɁɔ&Ή˜ņ_ήƷÙȰ@¦®śǮaϫ’Ƿ£?ǬŔØ|¥cΤɣΘʣ", 3214)]
        public void testar_ConvertFromNumericBase_convertendo_de_bases_desconhecidas_para_inteiro(string número,
            string baseNumérica,
            ulong númeroConvertido)
        {
            // Arrange, Given
            // Act, When

            var númeroConvertidoObtido = número.ConvertFromNumericBase(baseNumérica.ToCharArray());

            // Assert, Then

            númeroConvertidoObtido.Should().Be(númeroConvertido);
        }

        [Fact]
        public void testar_ConvertFromNumericBase_convertendo_de_bases_conhecidas_para_inteiro()
        {
            foreach (var basesConhecida in new[]
            {
                "01",
                "01234567",
                "0123456789",
                "0123456789abcdef"
            })
            {
                // Arrange, Given

                var númeroDecimal = ulong.MaxValue;
                var númeroEmOutraBase =
                    basesConhecida.Length != 10
                        ? Convert.ToString((long) númeroDecimal, basesConhecida.Length)
                        : númeroDecimal.ToString();

                // Act, When

                var númeroConvertido = númeroEmOutraBase.ConvertFromNumericBase(basesConhecida.ToCharArray());

                // Assert, Then

                númeroConvertido.Should().Be(númeroDecimal);
            }
        }

        [Fact]
        public void testar_ConvertToNumericBase_convertendo_inteiro_para_bases_conhecidas()
        {
            foreach (var basesConhecida in new[]
            {
                "01",
                "01234567",
                "0123456789",
                "0123456789abcdef"
            })
            {
                // Arrange, Given

                var númeroULong = ulong.MaxValue;
                var númeroUInt = uint.MaxValue;
                var númeroUShort = ushort.MaxValue;
                var númeroByte = byte.MaxValue;

                // Act, When

                var númeroULongConvertido = númeroULong.ConvertToNumericBase(basesConhecida.ToCharArray());
                var númeroUIntConvertido = númeroUInt.ConvertToNumericBase(basesConhecida.ToCharArray());
                var númeroUShortConvertido = númeroUShort.ConvertToNumericBase(basesConhecida.ToCharArray());
                var númeroByteConvertido = númeroByte.ConvertToNumericBase(basesConhecida.ToCharArray());

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