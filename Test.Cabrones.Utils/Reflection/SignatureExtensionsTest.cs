﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Reflection
{
    public class SignatureExtensionsTest
    {
        [Theory]
        [InlineData(typeof(string), "String")]
        [InlineData(typeof(int[]), "Int32[]")]
        [InlineData(typeof(int[,,]), "Int32[,,]")]
        [InlineData(typeof(IDictionary<string[,], string[][][]>), "IDictionary<String[,], String[][][]>")]
        public void ToSignatureCSharp_para_Type_deve_funcionar_corretamente(Type tipo, string assinaturaEsperada)
        {
            // Arrange, Given
            // Act, When

            var assinatura = tipo.ToSignatureCSharp();

            // Assert, Then

            assinatura.Should().Be(assinaturaEsperada);
        }

        [Theory]
        [InlineData(typeof(IInterface1), "String Interface1Método()", true)]
        [InlineData(typeof(IInterface2), "String Interface1Método()", false)]
        [InlineData(typeof(ClasseNeta), "TTipo[] MétodoGeneric<TTipo>(String, TTipo, TTipo[])", true)]
        [InlineData(typeof(ClasseEstática), "DateTime Agora()", false)]
        [InlineData(typeof(ClasseEstática), "static DateTime Agora()", true)]
        [InlineData(typeof(ClasseComGenerics<,>), "TMetodo MétodoComGenerics1<TMetodo>(TMetodo)", true)]
        [InlineData(typeof(ClasseComGenerics<string, int>), "TMetodo MétodoComGenerics1<TMetodo>(TMetodo)", true)]
        [InlineData(typeof(ClasseComGenerics<,>), "TClasse1 MétodoComGenerics2<TClasse1>(TClasse1)", true)]
        [InlineData(typeof(ClasseComGenerics<string, int>), "TClasse1 MétodoComGenerics2<TClasse1>(TClasse1)", true)]
        [InlineData(typeof(ClasseComGenerics<,>), "TClasse1 MétodoComGenerics3(TClasse1)", true)]
        [InlineData(typeof(ClasseComGenerics<string, int>), "String MétodoComGenerics3(String)", true)]
        [InlineData(typeof(ClasseComGenerics<,>), "Void MétodoComGenerics4<TMetodo1, TMetodo2>(TMetodo1, TMetodo2)",
            true)]
        [InlineData(typeof(ClasseComGenerics<string, int>),
            "Void MétodoComGenerics4<TMetodo1, TMetodo2>(TMetodo1, TMetodo2)", true)]
        [InlineData(typeof(ClasseComGenerics<,>), "Void MétodoComGenerics5<TClasse1, TClasse2>(TClasse1, TClasse2)",
            true)]
        [InlineData(typeof(ClasseComGenerics<string, int>),
            "Void MétodoComGenerics5<TClasse1, TClasse2>(TClasse1, TClasse2)", true)]
        [InlineData(typeof(ClasseComGenerics<,>), "Void MétodoComGenerics6(TClasse1, TClasse2)", true)]
        [InlineData(typeof(ClasseComGenerics<string, int>), "Void MétodoComGenerics6(String, Int32)", true)]
        [InlineData(typeof(ClasseComMembrosFilhos),
            "ClasseComMembrosFilhos.Listagem GetLista(ClasseComMembrosFilhos.Listagem, ClasseComMembrosFilhos.Listagem[])",
            true)]
        [InlineData(typeof(ClasseComMembrosFilhos),
            "ClasseComMembrosFilhos+Listagem GetLista(ClasseComMembrosFilhos+Listagem, ClasseComMembrosFilhos+Listagem[])",
            false)]
        public void ToSignatureCSharp_para_MethodInfo_deve_funcionar_corretamente(Type tipo, string assinatura,
            bool existe)
        {
            // Arrange, Given

            var métodos = tipo.AllMethods();

            // Act, When

            var assinaturas = métodos.Select(s => s.ToSignatureCSharp());

            // Assert, Then

            if (existe)
                assinaturas.Should().Contain(assinatura);
            else
                assinaturas.Should().NotContain(assinatura);
        }

        [Theory]
        [InlineData(typeof(IInterface1), "Interface1Propriedade", "Int32 Interface1Propriedade { get; set; }")]
        [InlineData(typeof(ClasseNeta), "ClasseNetaPropriedadePrivadaEstáticaPublic",
            "static Int32 ClasseNetaPropriedadePrivadaEstáticaPublic { get; }")]
        [InlineData(typeof(ClasseNeta), "ClasseNetaPropriedadePrivadaInstânciaPublic",
            "Int32 ClasseNetaPropriedadePrivadaInstânciaPublic { set; }")]
        [InlineData(typeof(ClasseNeta), "PropriedadeComplicada",
            "IDictionary<String[,], String[][][]> PropriedadeComplicada { get; set; }")]
        [InlineData(typeof(ClasseComGenerics<,>), "PropriedadeComGenerics",
            "TClasse1 PropriedadeComGenerics { get; set; }")]
        [InlineData(typeof(ClasseComGenerics<string, int>), "PropriedadeComGenerics",
            "String PropriedadeComGenerics { get; set; }")]
        [InlineData(typeof(ClasseComMembrosFilhos), "Lista",
            "ClasseComMembrosFilhos.Listagem Lista { get; set; }")]
        [InlineData(typeof(ClasseComModificadoresDeAcesso), "PropriedadeSetPrivate",
            "Int32 PropriedadeSetPrivate { get; }")]
        [InlineData(typeof(ClasseComModificadoresDeAcesso), "PropriedadeGetInternal",
            "Int32 PropriedadeGetInternal { set; }")]
        [InlineData(typeof(ClasseComModificadoresDeAcesso), "PropriedadeSetProtected",
            "Int32 PropriedadeSetProtected { get; }")]
        public void ToSignatureCSharp_para_PropertyInfo_deve_funcionar_corretamente(Type tipo, string nomeDaPropriedade,
            string assinaturaEsperada)
        {
            // Arrange, Given

            var propriedade = tipo.GetProperty(nomeDaPropriedade,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            // Act, When

            var assinatura = propriedade.ToSignatureCSharp();

            // Assert, Then

            assinatura.Should().Be(assinaturaEsperada);
        }

        [Theory]
        [InlineData(typeof(ClasseComEvento), "EventoDaInstância", "Action EventoDaInstância")]
        [InlineData(typeof(ClasseComEvento), "EventoEstático", "static Func<String, Int32> EventoEstático")]
        public void ToSignatureCSharp_para_EventInfo_deve_funcionar_corretamente(Type tipo, string nomeDoEvento,
            string assinaturaEsperada)
        {
            // Arrange, Given

            var evento = tipo.GetEvent(nomeDoEvento,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            // Act, When

            var assinatura = evento.ToSignatureCSharp();

            // Assert, Then

            assinatura.Should().Be(assinaturaEsperada);
        }

        [Theory]
        [InlineData(typeof(Classe4ComCamposDeExemplo<bool>), "CampoPúblico", "Int32 CampoPúblico")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<bool>), "CampoPrivado", "Int32 CampoPrivado")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<>), "CampoPúblico", "Int32 CampoPúblico")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<>), "CampoPrivado", "Int32 CampoPrivado")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<bool>), "CampoGenéricoPúblico", "Boolean CampoGenéricoPúblico")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<bool>), "CampoGenéricoPrivado", "Boolean CampoGenéricoPrivado")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<>), "CampoGenéricoPúblico", "TTipo CampoGenéricoPúblico")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<>), "CampoGenéricoPrivado", "TTipo CampoGenéricoPrivado")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<bool>), "CampoGenérico2Público",
            "Func<Boolean, String> CampoGenérico2Público")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<bool>), "CampoGenérico2Privado",
            "Func<Boolean, String> CampoGenérico2Privado")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<>), "CampoGenérico2Público",
            "Func<TTipo, String> CampoGenérico2Público")]
        [InlineData(typeof(Classe4ComCamposDeExemplo<>), "CampoGenérico2Privado",
            "Func<TTipo, String> CampoGenérico2Privado")]
        public void ToSignatureCSharp_para_FieldInfo_deve_funcionar_corretamente(Type tipo, string nomeDoCampo,
            string assinaturaEsperada)
        {
            // Arrange, Given

            var campo = tipo.GetField(nomeDoCampo,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            // Act, When

            var assinatura = campo!.ToSignatureCSharp();

            // Assert, Then

            assinatura.Should().Be(assinaturaEsperada);
        }
    }
}