using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Reflection
{
    public class TestTypeExtensions
    {
        [Fact]
        public void funcionamento_do_método_IsOwnOfType()
        {
            // Arrange, Given

            var tipo = typeof(ClasseFilha);
            var métodoPróprio = tipo.GetMethod("MétodoString3");
            var métodoNãoPróprio = tipo.GetMethod("MétodoString1");

            // Act, When

            var métodoPróprioResultado = métodoPróprio.IsOwnOfType(tipo);
            var métodoNãoPróprioResultado = métodoNãoPróprio.IsOwnOfType(tipo);

            // Assert, Then

            métodoPróprioResultado.Should().BeTrue();
            métodoNãoPróprioResultado.Should().BeFalse();
        }
    }
}