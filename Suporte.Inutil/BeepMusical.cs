using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace Suporte.Inutil
{
    /// <summary>
    /// <para>Classe que agrupa beeps musicais.</para>
    /// </summary>
    public static class BeepMusical
    {
        /// <summary>
        /// <para>Lista de músicas.</para>
        /// </summary>
        public enum Musicas
        {
            /// <summary>
            /// <para>Tema do Super Mario Bros.</para>
            /// </summary>
            SuperMarioBrosTheme,

            /// <summary>
            /// <para>Pretty Woman</para>
            /// </summary>
            PrettyWoman,

            /// <summary>
            /// <para>Dó Ré Mí Fá</para>
            /// </summary>
            DoReMiFa, 

            /// <summary>
            /// <para>Som crescente.</para>
            /// </summary>
            Crescente,

            /// <summary>
            /// <para>Som descrescente.</para>
            /// </summary>
            Descrescente
        }

        /// <summary>
        /// <para>Notas musicais.</para>
        /// <para>Onde Key é a frequência, e Value a duração.</para>
        /// </summary>
        private static Dictionary<Musicas, KeyValuePair<int, int>[]> Notas { get; set; }

        /// <summary>
        /// <para>Construtor tipo <c>static</c>.</para>
        /// </summary>
        static BeepMusical()
        {
            ListaDeBackgroundWorker = new List<BackgroundWorker>(); 
            Notas = new Dictionary<Musicas, KeyValuePair<int, int>[]>();
            Notas.Add(Musicas.SuperMarioBrosTheme, ObterNotasPara_SuperMarioBrosTheme());
            Notas.Add(Musicas.DoReMiFa, ObterNotasPara_DoReMiFa());
            Notas.Add(Musicas.PrettyWoman, ObterNotasPara_PrettyWoman());
        }

        /// <summary>
        /// <para>Toca uma música.</para>
        /// </summary>
        /// <param name="musica"><para>Música.</para></param>
        /// <param name="modoBackground">
        /// <para>Quando <c>==true</c>, a música é executada em outro
        /// Thread, sem travar a aplicação.</para>
        /// </param>
        public static void Tocar(Musicas musica, bool modoBackground)
        {
            if (modoBackground)
            {
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (sender, e) =>
                    {
                        Tocar(musica, sender as BackgroundWorker);
                    };
                backgroundWorker.RunWorkerCompleted += (sender, e) =>
                    {
                        (sender as BackgroundWorker).Dispose();
                    };
                backgroundWorker.WorkerSupportsCancellation = true;
                backgroundWorker.RunWorkerAsync();
                ListaDeBackgroundWorker.Add(backgroundWorker);
            }
            else
            {
                Tocar(musica, null);
            }
        }

        /// <summary>
        /// <para>Para todas as músicas em execução.</para>
        /// </summary>
        public static void PararTodas()
        {
            foreach (BackgroundWorker backgroundWorker in ListaDeBackgroundWorker)
            {
                if (backgroundWorker.IsBusy)
                {
                    backgroundWorker.CancelAsync();
                }
            }
        }

        /// <summary>
        /// <para>Lista de <see cref="System.ComponentModel.BackgroundWorker"/> já utilizados.</para>
        /// <para>Lista necessária para interromper todas as músicas em execução.</para>
        /// </summary>
        private static List<BackgroundWorker> ListaDeBackgroundWorker { get; set; }

        /// <summary>
        /// <para>Frequência MÍNIMA para o padrão <see cref="Musicas.Crescente"/> e <see cref="Musicas.Descrescente"/>.</para>
        /// <para>Frequência MÍNIMA permitida pelo sistema operacional: 37</para>
        /// </summary>
        private const int FREQUENCIA_MINIMA = 100;

        /// <summary>
        /// <para>Frequência MÁXIMA para o padrão <see cref="Musicas.Crescente"/> e <see cref="Musicas.Descrescente"/>.</para>
        /// <para>Frequência MÁXIMA permitida pelo sistema operacional: 32767</para>
        /// </summary>
        private const int FREQUENCIA_MAXIMA = 4000;


        /// <summary>
        /// <para>Toca uma música.</para>
        /// </summary>
        /// <param name="musica"><para>Música.</para></param>
        /// <param name="backgroundWorker">
        /// <para>Refere-se ao <see cref="System.ComponentModel.BackgroundWorker"/> que
        /// está tocando a música.</para>
        /// <para>Parâmetro necessário para cancelar a execução da música.</para>
        /// </param>
        private static void Tocar(Musicas musica, BackgroundWorker backgroundWorker)
        {
            if (musica != Musicas.Crescente &&
                musica != Musicas.Descrescente &&
                !Notas.ContainsKey(musica))
            {
                throw new NotImplementedException("Música não implementada.");
            }

            switch (musica)
            {
                case Musicas.Crescente:
                    TocarPadraoCrescente(FREQUENCIA_MINIMA, FREQUENCIA_MAXIMA, backgroundWorker);
                    break;
                case Musicas.Descrescente:
                    TocarPadraoCrescente(FREQUENCIA_MAXIMA, FREQUENCIA_MINIMA, backgroundWorker);
                    break;
                default:
                    TocarNotas(musica, backgroundWorker);
                    break;
            }
        }
                
        /// <summary>
        /// <para>Toca uma música baseada em padrões.</para>
        /// </summary>
        /// <param name="frequenciaInicial"><para>Frequência inicial.</para></param>
        /// <param name="frequenciaFinal"><para>Frequência final.</para></param>
        /// <param name="backgroundWorker">
        /// <para>Refere-se ao <see cref="System.ComponentModel.BackgroundWorker"/> que
        /// está tocando a música.</para>
        /// <para>Parâmetro necessário para cancelar a execução da música.</para>
        /// </param>
        private static void TocarPadraoCrescente(int frequenciaInicial, int frequenciaFinal, BackgroundWorker backgroundWorker)
        {
            int delaySom = 50;
            int delaySilencio = 1;

            bool crescente = frequenciaInicial <= frequenciaFinal;
            int inicio = crescente ? frequenciaInicial : frequenciaFinal;
            int fim = crescente ? frequenciaFinal : frequenciaInicial;

            for (int i = inicio; i <= fim; i++)
            {
                i += 100; if (i > fim) { i = fim; }
                int frequencia = crescente ? i : frequenciaInicial - (i - frequenciaFinal);
                Console.Beep(frequencia, delaySom);
                Thread.Sleep(delaySilencio);
                if (backgroundWorker != null && backgroundWorker.CancellationPending) { break; }
            }
        }

        /// <summary>
        /// <para>Toca uma música baseada em notas.</para>
        /// </summary>
        /// <param name="musica"><para>Música.</para></param>
        /// <param name="backgroundWorker">
        /// <para>Refere-se ao <see cref="System.ComponentModel.BackgroundWorker"/> que
        /// está tocando a música.</para>
        /// <para>Parâmetro necessário para cancelar a execução da música.</para>
        /// </param>
        private static void TocarNotas(Musicas musica, BackgroundWorker backgroundWorker)
        {
            KeyValuePair<int, int>[] notas = Notas[musica];
            foreach (KeyValuePair<int, int> nota in notas)
            {
                if (nota.Key == 0) { Thread.Sleep(nota.Value); }
                else { Console.Beep(nota.Key, nota.Value); }

                if (backgroundWorker != null && backgroundWorker.CancellationPending) { break; }
            }
        }

        /// <summary>
        /// <para>Obtem as notas para a música: SuperMarioBrosTheme</para>
        /// </summary>
        /// <returns><para>Conjunto de notas musicais.</para></returns>
        private static KeyValuePair<int, int>[] ObterNotasPara_SuperMarioBrosTheme()
        {
            List<KeyValuePair<int, int>> notas = new List<KeyValuePair<int, int>>();

            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 167));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(0, 375));
            notas.Add(new KeyValuePair<int,int>(392, 125));
            notas.Add(new KeyValuePair<int,int>(0, 375));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(392, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(330, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(494, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(466, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(392, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(880, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(494, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(392, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(330, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(494, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(466, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(392, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(880, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(494, 125));
            notas.Add(new KeyValuePair<int,int>(0, 375));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(740, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 167));
            notas.Add(new KeyValuePair<int,int>(415, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(740, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 167));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 625));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(740, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 167));
            notas.Add(new KeyValuePair<int,int>(415, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 1125));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(740, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 167));
            notas.Add(new KeyValuePair<int,int>(415, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(740, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 167));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 625));
            notas.Add(new KeyValuePair<int,int>(784, 125));
            notas.Add(new KeyValuePair<int,int>(740, 125));
            notas.Add(new KeyValuePair<int,int>(698, 125));
            notas.Add(new KeyValuePair<int,int>(0, 42));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(659, 125));
            notas.Add(new KeyValuePair<int,int>(0, 167));
            notas.Add(new KeyValuePair<int,int>(415, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 125));
            notas.Add(new KeyValuePair<int,int>(440, 125));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(622, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(587, 125));
            notas.Add(new KeyValuePair<int,int>(0, 250));
            notas.Add(new KeyValuePair<int,int>(523, 125));
            notas.Add(new KeyValuePair<int,int>(0, 625));

            return notas.ToArray();
        }

        /// <summary>
        /// <para>Obtem as notas para a música: PrettyWoman</para>
        /// </summary>
        /// <returns><para>Conjunto de notas musicais.</para></returns>
        private static KeyValuePair<int, int>[] ObterNotasPara_PrettyWoman()
        {
            List<KeyValuePair<int, int>> notas = new List<KeyValuePair<int, int>>();

            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(415, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(523, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 1000));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(415, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(523, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 1000));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(415, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(523, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(784, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(698, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(415, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(523, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(784, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(698, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(415, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(523, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(784, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(698, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(415, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(523, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(587, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(784, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(698, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(440, 2000));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(370, 2000));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(440, 2000));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(440, 2000));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(293, 2000));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(440, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(440, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(440, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(440, 200));

            return notas.ToArray();
        }

        /// <summary>
        /// <para>Obtem as notas para a música: DoReMiFa</para>
        /// </summary>
        /// <returns><para>Conjunto de notas musicais.</para></returns>
        private static KeyValuePair<int, int>[] ObterNotasPara_DoReMiFa()
        {
            List<KeyValuePair<int, int>> notas = new List<KeyValuePair<int, int>>();

            notas.Add(new KeyValuePair<int, int>(261, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(293, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(349, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(349, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(349, 800));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(261, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(293, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(261, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(293, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(293, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(293, 800));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(261, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(392, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(349, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 800));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(261, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(293, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(329, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(349, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(349, 200));
            notas.Add(new KeyValuePair<int, int>(0, 100));
            notas.Add(new KeyValuePair<int, int>(349, 800));

            return notas.ToArray();
        }
    }
}
