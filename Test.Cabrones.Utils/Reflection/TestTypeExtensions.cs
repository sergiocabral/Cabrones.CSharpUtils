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

            // ReSharper disable once RedundantArgumentDefaultValue
            var propriedadesSemInterfaces = tipo.AllProperties(false).Select(a => a.ToString()).ToList();
            var propriedadesComInterfaces = tipo.AllProperties(true).Select(a => a.ToString()).ToList();

            // Assert, Then

            propriedadesSemInterfaces.Should().HaveCount(declaraçõesEsperadasSemInterface);
            propriedadesComInterfaces.Should().HaveCount(declaraçõesEsperadasComInterface);
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

            // ReSharper disable once RedundantArgumentDefaultValue
            var métodosSemInterfaces = tipo.AllMethods(false).Select(a => a.ToString()).ToList();
            var métodosComInterfaces = tipo.AllMethods(true).Select(a => a.ToString()).ToList();

            // Assert, Then

            métodosSemInterfaces.Should().HaveCount(declaraçõesEsperadasSemInterface);
            métodosComInterfaces.Should().HaveCount(declaraçõesEsperadasComInterface);
        }
        
        [Theory]
        [InlineData(typeof(IInterface1), 4)]
        [InlineData(typeof(IInterface2), 4)]
        [InlineData(typeof(IInterface3), 4)]
        [InlineData(typeof(ClassePai), 8)]
        [InlineData(typeof(ClasseFilha), 12)]
        [InlineData(typeof(ClasseNeta), 8)]
        public void funcionamento_do_método_MyProperties(Type tipoParaTeste, int declaraçõesEsperadas)
        {
            // Arrange, Given

            var tipo = tipoParaTeste;

            // Act, When

            var propriedades = tipo.MyProperties().Select(a => a.ToString()).ToList();

            // Assert, Then

            propriedades.Should().HaveCount(declaraçõesEsperadas);
        }

        [Theory]
        [InlineData(typeof(IInterface1), 1)]
        [InlineData(typeof(IInterface2), 1)]
        [InlineData(typeof(IInterface3), 1)]
        [InlineData(typeof(ClassePai), 7)]
        [InlineData(typeof(ClasseFilha), 2)]
        [InlineData(typeof(ClasseNeta), 0)]
        public void funcionamento_do_método_MyMethods(Type tipoParaTeste, int declaraçõesEsperadas)
        {
            // Arrange, Given

            var tipo = tipoParaTeste;

            // Act, When

            var métodos = tipo.MyMethods().Select(a => a.ToString()).ToList();

            // Assert, Then

            métodos.Should().HaveCount(declaraçõesEsperadas);
        }
    }
}