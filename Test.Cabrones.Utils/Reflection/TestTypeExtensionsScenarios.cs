// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedMember.Local

namespace Cabrones.Utils.Reflection
{
    // Este arquivo agrupa cenários envolvendo relacionados entre classes e interface para realização de testes.
    
    internal interface IInterface1
    {
        string MétodoString1(int parâmetroInteiro);

        int PropriedadeConcorrente { get; set; }
    }

    internal interface IInterface2
    {
        string MétodoString2(int parâmetroInteiro);

        float PropriedadeConcorrente { get; set; }
    }

    internal interface IInterface3 : IInterface1
    {
        string MétodoString2(int parâmetroInteiro);

        new byte PropriedadeConcorrente { get; set; }
    }

    internal class ClassePai : IInterface1, IInterface2
    {
        public string MétodoString1(int parâmetroInteiro) => string.Empty;

        public string MétodoString2(int parâmetroInteiro) => string.Empty;

        float IInterface2.PropriedadeConcorrente { get; set; }

        public int PropriedadeConcorrente { get; set; }

        private void MétodoDaInstânciaPrivado()
        {
        }

        private static void MétodoEstáticoPrivado()
        {
        }

        public void MétodoDaInstânciaPúblico()
        {
        }

        public static void MétodoEstáticoPúblico()
        {
        }
    }

    internal class ClasseFilha : ClassePai, IInterface3
    {
        public string MétodoString3(int parâmetroInteiro) => string.Empty;

        public new byte PropriedadeConcorrente { get; set; }
    }
}