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

            var propriedadesPúblicasDaInstânciaIgnorandoClassPai = tipo.OnlyProperties(BindingFlags.Public | BindingFlags.Instance, typeof(ClassePai)).Select(a => a.ToString()).ToList();
            var propriedadesPúblicasDaInstância = tipo.OnlyProperties(BindingFlags.Public | BindingFlags.Instance).Select(a => a.ToString()).ToList();
            var propriedadesPúblicasEstáticas = tipo.OnlyProperties(BindingFlags.NonPublic | BindingFlags.Static).Select(a => a.ToString()).ToList();

            // Assert, Then

            propriedadesPúblicasDaInstânciaIgnorandoClassPai.Should().BeEquivalentTo(
                "Byte get_PropriedadeConcorrente()",
                "Void set_PropriedadeConcorrente(Byte)"
            );

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
        
        [Fact]
        public void funcionamento_do_método_OnlyMethods()
        {
            // Arrange, Given

            var tipo = typeof(ClasseFilha);

            // Act, When

            var métodosPúblicosDaInstânciaIgnorandoObject = tipo.OnlyMethods(BindingFlags.Public | BindingFlags.Instance, typeof(object)).Select(a => a.ToString()).ToList();
            var métodosPúblicosDaInstância = tipo.OnlyMethods(BindingFlags.Public | BindingFlags.Instance).Select(a => a.ToString()).ToList();
            var métodosPúblicosEstáticos = tipo.OnlyMethods(BindingFlags.NonPublic | BindingFlags.Static).Select(a => a.ToString()).ToList();

            // Assert, Then

            métodosPúblicosDaInstânciaIgnorandoObject.Should().BeEquivalentTo(
                "System.String MétodoString3(Int32)",
                "System.String MétodoString1(Int32)",
                "System.String MétodoString2(Int32)",
                "Void MétodoDaInstânciaPúblico()"
            );

            métodosPúblicosDaInstância.Should().BeEquivalentTo(
                "System.String MétodoString3(Int32)",
                "System.String MétodoString1(Int32)",
                "System.String MétodoString2(Int32)",
                "Void MétodoDaInstânciaPúblico()",
                "System.Type GetType()",
                "System.String ToString()",
                "Boolean Equals(System.Object)",
                "Int32 GetHashCode()"
            );
            
            métodosPúblicosEstáticos.Should().BeEquivalentTo(
                "Void MétodoEstático()"
            );
        }
        
        [Fact]
        public void funcionamento_do_método_OnlyMyProperties()
        {
            // Arrange, Given

            var tipo = typeof(ClasseFilha);

            // Act, When

            var métodosPúblicosDaInstância = tipo.OnlyMyProperties(BindingFlags.Public | BindingFlags.Instance).Select(a => a.ToString()).ToList();
            var métodosPúblicosEstáticos = tipo.OnlyMyProperties(BindingFlags.NonPublic | BindingFlags.Static).Select(a => a.ToString()).ToList();

            // Assert, Then

            métodosPúblicosDaInstância.Should().BeEquivalentTo(
                "Byte get_PropriedadeConcorrente()",
                "Void set_PropriedadeConcorrente(Byte)"
            );
            
            métodosPúblicosEstáticos.Should().BeEquivalentTo(
                "System.String get_PropriedadeEstáticaPrivada2()",
                "Void set_PropriedadeEstáticaPrivada2(System.String)"
            );
        }
        
        [Fact]
        public void funcionamento_do_método_OnlyMyMethods()
        {
            // Arrange, Given

            var tipo = typeof(ClasseFilha);

            // Act, When

            var métodosPúblicosDaInstância = tipo.OnlyMyMethods(BindingFlags.Public | BindingFlags.Instance).Select(a => a.ToString()).ToList();
            var métodosPúblicosEstáticos = tipo.OnlyMyMethods(BindingFlags.NonPublic | BindingFlags.Static).Select(a => a.ToString()).ToList();

            // Assert, Then

            métodosPúblicosDaInstância.Should().BeEquivalentTo(
                "System.String MétodoString3(Int32)"
            );
            
            métodosPúblicosEstáticos.Should().BeEquivalentTo(
                "Void MétodoEstático()"
            );
        }
    }
}