﻿// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable EventNeverSubscribedTo.Local

#pragma warning disable 67
#pragma warning disable 693

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 108,114
#pragma warning disable 649
#pragma warning disable 169

// ReSharper disable EventNeverInvoked.Global

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Cabrones.Utils.Reflection
{
    // Este arquivo agrupa cenários envolvendo relacionados entre classes e interface para realização de testes.

    internal interface IInterface1
    {
        int Interface1Propriedade { get; set; }

        float PropriedadeConcorrente { get; set; }
        string Interface1Método();
    }

    internal interface IInterface2
    {
        int Interface2Propriedade { get; set; }

        int PropriedadeConcorrente { get; set; }
        string Interface2Método();
    }

    internal interface IInterface3 : IInterface1
    {
        int Interface3Propriedade { get; set; }

        new byte PropriedadeConcorrente { get; set; }
        string Interface3Método();
    }

    [ExcludeFromCodeCoverage]
    internal abstract class ClassePai : IInterface1, IInterface2
    {
        public string Interface1Método()
        {
            return string.Empty;
        }

        public int Interface1Propriedade { get; set; }

        float IInterface1.PropriedadeConcorrente { get; set; }

        public string Interface2Método()
        {
            return string.Empty;
        }

        public int Interface2Propriedade { get; set; }

        public int PropriedadeConcorrente { get; set; }

        public string ClassePaiMétodoPúblicoInstância()
        {
            return string.Empty;
        }

        private string ClassePaiMétodoPrivateInstância()
        {
            return string.Empty;
        }

        public static string ClassePaiMétodoPúblicoEstático()
        {
            return string.Empty;
        }

        private static string ClassePaiMétodoPrivateEstático()
        {
            return string.Empty;
        }

        protected abstract string ClassePaiMétodoAbstrato();

        public abstract string ClassePaiMétodoAbstratoPúblico();
    }

    [ExcludeFromCodeCoverage]
    internal class ClasseFilha : ClassePai, IInterface3
    {
        public int ClasseFilhaPropriedadePúblicaInstância { get; set; }

        public static int ClasseFilhaPropriedadePúblicaEstática { get; set; }

        private int ClasseFilhaPropriedadePrivadaInstância { get; set; }

        private static int ClasseFilhaPropriedadePrivadaEstática { get; set; }

        public string Interface3Método()
        {
            return string.Empty;
        }

        public int Interface3Propriedade { get; set; }

        public new byte PropriedadeConcorrente { get; set; }

        protected override string ClassePaiMétodoAbstrato()
        {
            return string.Empty;
        }

        public override string ClassePaiMétodoAbstratoPúblico()
        {
            return string.Empty;
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ClasseNeta : ClasseFilha
    {
        public int ClasseNetaPropriedadePúblicaInstância { get; set; }

        public static int ClasseNetaPropriedadePúblicaEstática { get; set; }

        private int ClasseNetaPropriedadePrivadaInstância
        {
            set => throw new NotImplementedException();
        }

        public int ClasseNetaPropriedadePrivadaInstânciaPublic
        {
            set => throw new NotImplementedException();
        }

        private static int ClasseNetaPropriedadePrivadaEstática { get; }

        public static int ClasseNetaPropriedadePrivadaEstáticaPublic { get; }

        public IDictionary<string[,], string[][][]> PropriedadeComplicada { get; set; }

        public TTipo[] MétodoGeneric<TTipo>(string param1, TTipo param2, TTipo[] param3)
        {
            return default;
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ClasseSozinha
    {
    }

    [ExcludeFromCodeCoverage]
    internal static class ClasseEstática
    {
        public static DateTime Agora()
        {
            return DateTime.Now;
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ClasseComGenerics<TClasse1, TClasse2>
    {
        public TClasse1 PropriedadeComGenerics { get; set; }

        public TMetodo MétodoComGenerics1<TMetodo>(TMetodo arg)
        {
            return arg;
        }

        public TClasse1 MétodoComGenerics2<TClasse1>(TClasse1 arg)
        {
            return arg;
        }

        public TClasse1 MétodoComGenerics3(TClasse1 arg)
        {
            return arg;
        }

        public void MétodoComGenerics4<TMetodo1, TMetodo2>(TMetodo1 arg1, TMetodo2 arg2)
        {
        }

        public void MétodoComGenerics5<TClasse1, TClasse2>(TClasse1 arg1, TClasse2 arg2)
        {
        }

        public void MétodoComGenerics6(TClasse1 arg1, TClasse2 arg2)
        {
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ClasseComMembrosFilhos
    {
        public enum Listagem
        {
            Valor1,
            Valor2
        }

        public Listagem Lista { get; set; }

        public Listagem GetLista(Listagem valor, Listagem[] valores)
        {
            return Listagem.Valor1;
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ClasseComModificadoresDeAcesso
    {
        public int PropriedadeSetPrivate { get; private set; }
        public int PropriedadeGetInternal { internal get; set; }
        public int PropriedadeSetProtected { get; protected set; }
    }

    public interface InterfaceComEvento
    {
        public event Action EventoDaInterface;
    }

    [ExcludeFromCodeCoverage]
    internal class ClasseComEvento : InterfaceComEvento
    {
        public event Action EventoDaInterface;
        public event Action EventoDaInstância;
        public static event Func<string, int> EventoEstático;
        private event Action EventoPrivado;
        protected event Action EventoProtegido;
    }

    [ExcludeFromCodeCoverage]
    internal class Classe1ComCampo : Classe2ComCampo
    {
        private static string CampoPrivadoEstático;
        public static string CampoPúblicoEstático;
        private string CampoPrivado;
        public string CampoPúblico;
    }

    [ExcludeFromCodeCoverage]
    internal class Classe2ComCampo : Classe3ComCampo
    {
        private static string CampoPrivadoEstático;
        public static string CampoPúblicoEstático;
        private static string Campo2PrivadoEstático;
        public static string Campo2PúblicoEstático;

        private string Campo2Privado;
        public string Campo2Público;
        private string CampoPrivado;
        public string CampoPúblico;
    }

    internal class Classe3ComCampo
    {
    }

    internal class Classe4ComCamposDeExemplo<TTipo>
    {
        private Func<TTipo, string> CampoGenérico2Privado;
        public Func<TTipo, string> CampoGenérico2Público;
        private TTipo CampoGenéricoPrivado;
        public TTipo CampoGenéricoPúblico;
        private int CampoPrivado;
        public int CampoPúblico;
    }
}