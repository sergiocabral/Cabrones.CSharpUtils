// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Cabrones.Utils.Reflection
{
    // Este arquivo agrupa cenários envolvendo relacionados entre classes e interface para realização de testes.
    
    internal interface IInterface1
    {
        string Interface1Método();

        int Interface1Propriedade { get; set; }

        float PropriedadeConcorrente { get; set; }
    }

    internal interface IInterface2
    {
        string Interface2Método();

        int Interface2Propriedade { get; set; }

        int PropriedadeConcorrente { get; set; }
    }

    internal interface IInterface3 : IInterface1
    {
        string Interface3Método();

        int Interface3Propriedade { get; set; }

        new byte PropriedadeConcorrente { get; set; }
    }

    internal abstract class ClassePai : IInterface1, IInterface2
    {
        public string Interface1Método() => string.Empty;

        public int Interface1Propriedade { get; set; }
        public string Interface2Método() => string.Empty;

        public int Interface2Propriedade { get; set; }
        
        public int PropriedadeConcorrente { get; set; }

        float IInterface1.PropriedadeConcorrente { get; set; }

        public string ClassePaiMétodoPúblicoInstância() => string.Empty;
        
        private string ClassePaiMétodoPrivateInstância() => string.Empty;

        public static string ClassePaiMétodoPúblicoEstático() => string.Empty;
        
        private static string ClassePaiMétodoPrivateEstático() => string.Empty;
        
        protected abstract string ClassePaiMétodoAbstrato();
        
        public abstract string ClassePaiMétodoAbstratoPúblico();
    }

    internal class ClasseFilha : ClassePai, IInterface3
    {
        public string Interface3Método() => string.Empty;

        public int Interface3Propriedade { get; set; }
        
        public new byte PropriedadeConcorrente { get; set; }

        protected override string ClassePaiMétodoAbstrato() => string.Empty;
        
        public override string ClassePaiMétodoAbstratoPúblico() => string.Empty;
        
        public int ClasseFilhaPropriedadePúblicaInstância { get; set; }
        
        public static int ClasseFilhaPropriedadePúblicaEstática { get; set; }
        
        private int ClasseFilhaPropriedadePrivadaInstância { get; set; }
        
        private static int ClasseFilhaPropriedadePrivadaEstática { get; set; }
    }

    internal class ClasseNeta : ClasseFilha
    {
        public int ClasseNetaPropriedadePúblicaInstância { get; set; }
        
        public static int ClasseNetaPropriedadePúblicaEstática { get; set; }
        
        private int ClasseNetaPropriedadePrivadaInstância
        {
            set => throw new System.NotImplementedException();
        }

        private static int ClasseNetaPropriedadePrivadaEstática { get; }
    }
}