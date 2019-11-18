using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Reflection
{
    public class TestTypeExtensions
    {
        [Theory]
        [InlineData(typeof(IInterface1), 4, 4)]
        [InlineData(typeof(IInterface2), 4, 4)]
        [InlineData(typeof(IInterface3), 8, 4)]
        [InlineData(typeof(ClassePai), 16, 8)]
        [InlineData(typeof(ClasseFilha), 32, 20)]
        [InlineData(typeof(ClasseNeta), 40, 28)]
        public void funcionamento_do_método_AllProperties(Type tipoParaTeste, int declaraçõesEsperadasComInterface, int declaraçõesEsperadasSemInterface)
        {
            // Arrange, Given

            var tipo = tipoParaTeste;

            // Act, When

            var propriedadesComInterfaces = tipo.AllProperties(true).Select(a => a.ToString()).ToList();
            var propriedadesSemInterfaces = tipo.AllProperties(false).Select(a => a.ToString()).ToList();

            // Assert, Then

            propriedadesComInterfaces.Should().HaveCount(declaraçõesEsperadasComInterface);
            propriedadesSemInterfaces.Should().HaveCount(declaraçõesEsperadasSemInterface);
        }

        [Theory]
        [InlineData(typeof(IInterface1), 1, 1)]
        [InlineData(typeof(IInterface2), 1, 1)]
        [InlineData(typeof(IInterface3), 2, 1)]
        [InlineData(typeof(ClassePai), 17, 15)]
        [InlineData(typeof(ClasseFilha), 20, 17)]
        [InlineData(typeof(ClasseNeta), 20, 17)]
        public void funcionamento_do_método_AllMethods(Type tipoParaTeste, int declaraçõesEsperadasComInterface, int declaraçõesEsperadasSemInterface)
        {
            // Arrange, Given

            var tipo = tipoParaTeste;

            // Act, When

            var métodosComInterfaces = tipo.AllMethods(true).Select(a => a.ToString()).ToList();
            var métodosSemInterfaces = tipo.AllMethods(false).Select(a => a.ToString()).ToList();

            // Assert, Then

            métodosComInterfaces.Should().HaveCount(declaraçõesEsperadasComInterface);
            métodosSemInterfaces.Should().HaveCount(declaraçõesEsperadasSemInterface);
        }
    }
}