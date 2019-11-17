using System.Linq;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Reflection
{
    public class TestTypeExtensions
    {
        [Fact]
        public void funcionamento_do_método_AllProperties()
        {
            // Arrange, Given

            var tipo = typeof(ClasseNeta);

            // Act, When

            var propriedades = tipo.AllProperties().Select(a => a.ToString()).ToList();

            // Assert, Then

            propriedades.Should().BeEquivalentTo(
                "Int32 get_ClasseNetaPropriedadePúblicaInstância()",
                "Void set_ClasseNetaPropriedadePúblicaInstância(Int32)",
                "Int32 get_ClasseNetaPropriedadePúblicaEstática()",
                "Void set_ClasseNetaPropriedadePúblicaEstática(Int32)",
                "Int32 get_ClasseNetaPropriedadePrivadaInstância()",
                "Void set_ClasseNetaPropriedadePrivadaInstância(Int32)",
                "Int32 get_ClasseNetaPropriedadePrivadaEstática()",
                "Void set_ClasseNetaPropriedadePrivadaEstática(Int32)",
                "Int32 get_Interface3Propriedade()",
                "Void set_Interface3Propriedade(Int32)",
                "Byte get_PropriedadeConcorrente()",
                "Void set_PropriedadeConcorrente(Byte)",
                "Int32 get_ClasseFilhaPropriedadePúblicaInstância()",
                "Void set_ClasseFilhaPropriedadePúblicaInstância(Int32)",
                "Int32 get_ClasseFilhaPropriedadePúblicaEstática()",
                "Void set_ClasseFilhaPropriedadePúblicaEstática(Int32)",
                "Int32 get_ClasseFilhaPropriedadePrivadaInstância()",
                "Void set_ClasseFilhaPropriedadePrivadaInstância(Int32)",
                "Int32 get_ClasseFilhaPropriedadePrivadaEstática()",
                "Void set_ClasseFilhaPropriedadePrivadaEstática(Int32)",
                "Int32 get_Interface1Propriedade()",
                "Void set_Interface1Propriedade(Int32)",
                "Int32 get_Interface2Propriedade()",
                "Void set_Interface2Propriedade(Int32)",
                "Int32 get_PropriedadeConcorrente()",
                "Void set_PropriedadeConcorrente(Int32)",
                "Single Cabrones.Utils.Reflection.IInterface1.get_PropriedadeConcorrente()",
                "Void Cabrones.Utils.Reflection.IInterface1.set_PropriedadeConcorrente(Single)"
            );
        }
        [Fact]
        public void funcionamento_do_método_AllMethods()
        {
            // Arrange, Given

            var tipo = typeof(ClasseNeta);

            // Act, When

            var métodos = tipo.AllMethods().Select(a => a.ToString()).ToList();

            // Assert, Then

            métodos.Should().BeEquivalentTo(
                "System.String Interface1Método()",
                "System.String Interface2Método()",
                "System.String Interface3Método()",
                "System.String ClassePaiMétodoPúblicoInstância()",
                "System.String ClassePaiMétodoPúblicoEstático()",
                "System.String ClassePaiMétodoPrivateInstância()",
                "System.String ClassePaiMétodoPrivateEstático()",
                "System.String ClassePaiMétodoAbstrato()",
                "System.String ClassePaiMétodoAbstrato()",
                "System.Type GetType()",
                "System.Object MemberwiseClone()",
                "Void Finalize()",
                "System.String ToString()",
                "Boolean Equals(System.Object)",
                "Boolean Equals(System.Object, System.Object)",
                "Boolean ReferenceEquals(System.Object, System.Object)",
                "Int32 GetHashCode()"
            );
        }
    }
}