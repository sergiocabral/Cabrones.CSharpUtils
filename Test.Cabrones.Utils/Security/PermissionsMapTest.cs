using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabrones.Test;
using Cabrones.Utils.Text;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Security
{
    public class PermissionsMapTest
    {
        [Theory]
        [InlineData("012345", true)]
        [InlineData("012340", false)]
        public void falhar_se_charset_Custom_tiver_caracteres_duplicados(string charset, bool estáCorreto)
        {
            // Arrange, Given
            // Act, When

            Func<PermissionMap<string, string>> criar = () => new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                charset);

            // Assert, Then

            if (!estáCorreto) criar.Should().ThrowExactly<ArgumentException>();
            else criar.Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        public void falhar_se_charset_Custom_informar_o_texto_com_comprimento_menor_que_2(string charset)
        {
            // Arrange, Given
            // Act, When

            Func<PermissionMap<string, string>> criar = () => new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                charset);

            // Assert, Then

            if ($"{charset}".Length < 2) criar.Should().ThrowExactly<ArgumentNullException>();
            else criar.Should().NotThrow();
        }

        [Fact]
        public void falhar_se_charset_Custom_não_informar_o_texto()
        {
            // Arrange, Given

            // Act, When

            Func<PermissionMap<string, string>> criar = () => new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Custom);

            // Assert, Then

            criar.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void falhar_se_charset_não_estiver_no_enum()
        {
            // Arrange, Given
            // Act, When

            Func<PermissionMap<string, string>> criar = () => new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                (PermissionMapCharset) int.MaxValue);

            // Assert, Then

            criar.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void o_resultado_do_mapa_sempre_é_vazio_se_as_permissões_estiverem_vazias()
        {
            foreach (var charset in Enum.GetValues(typeof(PermissionMapCharset)).OfType<PermissionMapCharset>())
            {
                // Arrange, Given

                var valorParaSecurables = this.FixtureMany<string>(10).ToArray();
                var valorParaPermissions = this.FixtureMany<string>(2).ToArray();

                var nenhumaPermissão = new Dictionary<string, IList<string>>();

                var sut =
                    charset != PermissionMapCharset.Custom
                        ? new PermissionMap<string, string>(
                            valorParaSecurables,
                            valorParaPermissions,
                            charset)
                        : new PermissionMap<string, string>(
                            valorParaSecurables,
                            valorParaPermissions,
                            "abcd");

                // Act, When

                var mapa = sut.Generate(nenhumaPermissão);

                // Assert, Then

                mapa.Should().BeEmpty();
            }
        }

        [Fact]
        public void verificar_charset_Ascii()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Ascii);

            // Assert, Then

            sut.CharsetValue.Should()
                .Be(@"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~");
        }

        [Fact]
        public void verificar_charset_Binary()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Binary);

            // Assert, Then

            sut.CharsetValue.Should().Be("01");
        }

        [Fact]
        public void verificar_charset_Decimal()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Decimal);

            // Assert, Then

            sut.CharsetValue.Should().Be("0123456789");
        }

        [Fact]
        public void verificar_charset_Hexadecimal()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Hexadecimal);

            // Assert, Then

            sut.CharsetValue.Should().Be("0123456789abcdef");
        }

        [Fact]
        public void verificar_charset_Letters()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Letters);

            // Assert, Then

            sut.CharsetValue.Should().Be("abcdefghijklmnopqrstuvwxyz");
        }

        [Fact]
        public void verificar_charset_LettersCaseSensitive()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.LettersCaseSensitive);

            // Assert, Then

            sut.CharsetValue.Should().Be("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        [Fact]
        public void verificar_charset_NumbersAndLetters()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.NumbersAndLetters);

            // Assert, Then

            sut.CharsetValue.Should().Be("0123456789abcdefghijklmnopqrstuvwxyz");
        }

        [Fact]
        public void verificar_charset_NumbersAndLettersCaseSensitive_que_deve_ser_o_padrão()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>());

            // Assert, Then

            sut.CharsetValue.Should().Be("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        [Fact]
        public void verificar_charset_Octal()
        {
            // Arrange, Given
            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Octal);

            // Assert, Then

            sut.CharsetValue.Should().Be("01234567");
        }

        [Fact]
        public void verificar_charset_Unicode()
        {
            // Arrange, Given

            var charsetEsperado = new string(Encoding
                .UTF8
                .GetAllEncodedStrings()
                .Where(a => a.Key > 32 && a.Value.Length == 1)
                .Select(a => a.Value[0])
                .ToArray());

            // Act, When

            var sut = new PermissionMap<string, string>(
                this.FixtureMany<string>(),
                this.FixtureMany<string>(),
                PermissionMapCharset.Unicode);

            // Assert, Then

            sut.CharsetValue.Should().Be(charsetEsperado);
        }

        [Fact]
        public void verificar_valores_das_propriedades()
        {
            // Arrange, Given

            var valorParaSecurables = this.FixtureMany<DateTime>().ToArray();
            var valorParaPermissions = this.FixtureMany<DateTime>().ToArray();
            var valorParaCharset = this.Fixture<PermissionMapCharset>();

            if (valorParaCharset == PermissionMapCharset.Custom) valorParaCharset++;

            // Act, When

            var sut = new PermissionMap<DateTime, DateTime>(
                valorParaSecurables,
                valorParaPermissions,
                valorParaCharset);

            // Assert, Then

            sut.Permissions.Should().BeEquivalentTo(
                valorParaPermissions
                    .OrderBy(a => $"{a}"),
                options => options.WithStrictOrdering());

            sut.Securables.Should().BeEquivalentTo(
                valorParaSecurables
                    .OrderBy(a => $"{a}"),
                options => options.WithStrictOrdering());

            sut.Charset.Should().Be(valorParaCharset);
        }
    }
}