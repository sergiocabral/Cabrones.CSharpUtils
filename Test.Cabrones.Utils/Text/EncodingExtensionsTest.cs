using System;
using System.Collections.Generic;
using System.Text;
using Cabrones.Test;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Text
{
    public class EncodingExtensionsTest
    {
        [Fact]
        public void GetAllEncodedStrings_deve_obter_todos_os_caracteres_válidos()
        {
            foreach (var encoding in new[]
            {
                // Arrange, Given

                Encoding.Default,
                Encoding.Unicode,
                Encoding.BigEndianUnicode,
                Encoding.UTF7,
                Encoding.UTF8,
                Encoding.UTF32,
                Encoding.ASCII
            })
            {
                // Act, When

                Action consultar = () => encoding.GetAllEncodedStrings();

                Func<IReadOnlyDictionary<int, string>> obterTodosOsCaracteres = () => encoding.GetAllEncodedStrings();

                // Assert, Then

                consultar.AssertTheSameValueButTheSecondTimeIsFaster();

                var caracteres = obterTodosOsCaracteres.Should().NotThrow().Which;

                caracteres.Should().HaveCount(encoding.WebName == Encoding.ASCII.WebName ? 128 : 1112064);
            }
        }
    }
}