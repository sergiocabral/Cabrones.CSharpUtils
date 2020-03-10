// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnassignedGetOnlyAutoProperty

using System;
using System.Collections.Generic;

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

    internal class ClasseNeta : ClasseFilha
    {
        public int ClasseNetaPropriedadePúblicaInstância { get; set; }

        public static int ClasseNetaPropriedadePúblicaEstática { get; set; }

        private int ClasseNetaPropriedadePrivadaInstância
        {
            set => throw new NotImplementedException();
        }

        private static int ClasseNetaPropriedadePrivadaEstática { get; }

        public IDictionary<string[,], string[][][]> PropriedadeComplicada { get; set; }

        public TTipo[] MétodoGeneric<TTipo>(string param1, TTipo param2, TTipo[] param3)
        {
            return default;
        }
    }

    internal class ClasseSozinha
    {
    }

    internal static class ClasseEstática
    {
        public static DateTime Agora()
        {
            return DateTime.Now;
        }
    }
}