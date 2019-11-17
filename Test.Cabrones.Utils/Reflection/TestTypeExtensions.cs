using System.Linq;
using System.Reflection;
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
        [Fact]
        public void funcionamento_do_método_OnlyProperties()
        {
            // Arrange, Given

            var tipo = typeof(ClasseFilha);

            // Act, When

            var propriedadesPúblicasDaInstância = tipo.OnlyProperties(BindingFlags.Public | BindingFlags.Instance).Select(a => a.ToString()).ToList();
            var propriedadesPúblicasEstáticas = tipo.OnlyProperties(BindingFlags.NonPublic | BindingFlags.Static).Select(a => a.ToString()).ToList();

            // Assert, Then

            propriedadesPúblicasDaInstância.Should().BeEquivalentTo(
                "Byte get_PropriedadeConcorrente()",
                "Void set_PropriedadeConcorrente(Byte)",
                "Int32 get_PropriedadeConcorrente()",
                "Void set_PropriedadeConcorrente(Int32)"
            );
            
            propriedadesPúblicasEstáticas.Should().BeEquivalentTo(
                "System.String get_PropriedadeEstáticaPrivada2()",
                "Void set_PropriedadeEstáticaPrivada2(System.String)"
            );
        }
    }
}