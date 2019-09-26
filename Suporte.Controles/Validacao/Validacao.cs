using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Suporte.Controles.Validacao
{
    /// <summary>
    /// <para>Classe principal da validação.</para>
    /// <para>Ela que aplicar a validação em um controle.</para>
    /// </summary>
    public static class Validacao
    {
        /// <summary>
        /// <para>Armazena lista de controles já utilizados.</para>
        /// </summary>
        private static List<Control> controlesJaUtilizados = new List<Control>();

        /// <summary>
        /// <para>Método principal (e único) que aplica de fato a validação.</para>
        /// </summary>
        /// <param name="controle"><para>Controle que deve ser validado.</para></param>
        /// <param name="validadores"><para>Lista de validadores.</para></param>
        public static void Aplicar(Control controle, params Validar[] validadores)
        {
            controlesJaUtilizados.Add(controle);

            //Se nenhum validador for passado, pra que continuar? - Sai fora.
            if (validadores == null || validadores.Length == 0) { return; }

            foreach (Validar validador in validadores)
            {
                //Aplicar ao controle cada um dos validadores passados.
                validador.AplicarEsteValidador(controle);
            }
        }

        #region Cancelar()

        /// <summary>
        /// <para>Cancela os validadores.</para>
        /// <para>Afeta apenas a validação atribuida por meio desta classe.</para>
        /// </summary>
        public static void Cancelar()
        {
            Cancelar(true);
        }

        /// <summary>
        /// <para>Cancela os validadores.</para>
        /// </summary>
        /// <param name="somenteValidadoresDestaClasse">
        /// <para>Quando <c>==true</c>, a abrangência deste método se restringe a validação
        /// atribuida por meio desta classe.</para>
        /// <para>Se <c>==false</c>, afeta qualquer validador atribuido em <see cref="Control.Validating"/>.</para>
        /// </param>
        public static void Cancelar(bool somenteValidadoresDestaClasse)
        {
            Cancelar(somenteValidadoresDestaClasse, controlesJaUtilizados.ToArray());
        }

        /// <summary>
        /// <para>Cancela os validadores.</para>
        /// <para>Afeta apenas a validação atribuida por meio desta classe.</para>
        /// </summary>
        /// <param name="controles"><para>Lista de controles.</para></param>
        public static void Cancelar(params Control[] controles)
        {
            Cancelar(true, controles);
        }

        /// <summary>
        /// <para>Cancela os validadores.</para>
        /// </summary>
        /// <param name="controles"><para>Lista de controles.</para></param>
        /// <param name="somenteValidadoresDestaClasse">
        /// <para>Quando <c>==true</c>, a abrangência deste método se restringe a validação
        /// atribuida por meio desta classe.</para>
        /// <para>Se <c>==false</c>, afeta qualquer validador atribuido em <see cref="Control.Validating"/>.</para>
        /// </param>
        public static void Cancelar(bool somenteValidadoresDestaClasse, params Control[] controles)
        {
            foreach (Control controle in controles)
            {
                Cancelar(somenteValidadoresDestaClasse, controle);
            }
        }

        /// <summary>
        /// <para>Cancela os validadores.</para>
        /// <para>Afeta apenas a validação atribuida por meio desta classe.</para>
        /// </summary>
        /// <param name="controle"><para>Controle.</para></param>
        public static void Cancelar(Control controle)
        {
            Cancelar(true, controle);
        }

        /// <summary>
        /// <para>Cancela os validadores.</para>
        /// </summary>
        /// <param name="somenteValidadoresDestaClasse">
        /// <para>Quando <c>==true</c>, a abrangência deste método se restringe a validação
        /// atribuida por meio desta classe.</para>
        /// <para>Se <c>==false</c>, afeta qualquer validador atribuido em <see cref="Control.Validating"/>.</para>
        /// </param>
        /// <param name="controle"><para>Controle.</para></param>
        public static void Cancelar(bool somenteValidadoresDestaClasse, Control controle)
        {
            CancelEventHandler[] handlers = ObterEventosValidating(somenteValidadoresDestaClasse, controle);
            foreach (CancelEventHandler handler in handlers)
            {
                controle.Validating -= handler;
            }
        }

        #endregion

        #region Validar()

        /// <summary>
        /// <para>Retorna o estado da validação.</para>
        /// <para>Aplicável a todos os controles já utilizados.</para>
        /// <para>Abrange apenas a validação atribuida por meio desta classe.</para>
        /// </summary>
        /// <returns><para>Booleano <c>true</c> ou <c>false</c>.</para></returns>
        public static bool Validar()
        {
            return Validar(true);
        }

        /// <summary>
        /// <para>Retorna o estado da validação.</para>
        /// <para>Aplicável a todos os controles já utilizados.</para>
        /// </summary>
        /// <param name="somenteValidadoresDestaClasse">
        /// <para>Quando <c>==true</c>, a abrangência deste método se restringe a validação
        /// atribuida por meio desta classe.</para>
        /// <para>Se <c>==false</c>, afeta qualquer validador atribuido em <see cref="Control.Validating"/>.</para>
        /// </param>
        /// <returns><para>Booleano <c>true</c> ou <c>false</c>.</para></returns>
        public static bool Validar(bool somenteValidadoresDestaClasse)
        {
            return Validar(somenteValidadoresDestaClasse, controlesJaUtilizados.ToArray());
        }

        /// <summary>
        /// <para>Retorna o estado da validação.</para>
        /// <para>Abrange apenas a validação atribuida por meio desta classe.</para>
        /// </summary>
        /// <param name="controles"><para>Lista de controles.</para></param>
        /// <returns><para>Booleano <c>true</c> ou <c>false</c>.</para></returns>
        public static bool Validar(params Control[] controles)
        {
            return Validar(true, controles);
        }

        /// <summary>
        /// <para>Retorna o estado da validação.</para>
        /// </summary>
        /// <param name="somenteValidadoresDestaClasse">
        /// <para>Quando <c>==true</c>, a abrangência deste método se restringe a validação
        /// atribuida por meio desta classe.</para>
        /// <para>Se <c>==false</c>, afeta qualquer validador atribuido em <see cref="Control.Validating"/>.</para>
        /// </param>
        /// <param name="controles"><para>Lista de controles.</para></param>
        /// <returns><para>Booleano <c>true</c> ou <c>false</c>.</para></returns>
        public static bool Validar(bool somenteValidadoresDestaClasse, params Control[] controles)
        {
            foreach (Control controle in controles)
            {
                if (!Validar(somenteValidadoresDestaClasse, controle))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// <para>Retorna o estado da validação.</para>
        /// <para>Abrange apenas a validação atribuida por meio desta classe.</para>
        /// </summary>
        /// <param name="controle"><para>Controle.</para></param>
        /// <returns><para>Booleano <c>true</c> ou <c>false</c>.</para></returns>
        public static bool Validar(Control controle)
        {
            return Validar(true, controle);
        }

        /// <summary>
        /// <para>Retorna o estado da validação.</para>
        /// </summary>
        /// <param name="somenteValidadoresDestaClasse">
        /// <para>Quando <c>==true</c>, a abrangência deste método se restringe a validação
        /// atribuida por meio desta classe.</para>
        /// <para>Se <c>==false</c>, afeta qualquer validador atribuido em <see cref="Control.Validating"/>.</para>
        /// </param>
        /// <param name="controle"><para>Controle.</para></param>
        /// <returns><para>Booleano <c>true</c> ou <c>false</c>.</para></returns>
        public static bool Validar(bool somenteValidadoresDestaClasse, Control controle)
        {
            CancelEventHandler[] handlers = ObterEventosValidating(somenteValidadoresDestaClasse, controle);
            foreach (CancelEventHandler handler in handlers)
            {
                //Chama o validador.
                CancelEventArgs eventArgs = new CancelEventArgs { Cancel = false };
                handler.DynamicInvoke(new object[] { controle, eventArgs });

                if (eventArgs.Cancel)
                {
                    ///Se não estiver válido, já retorna o resultado
                    ///sem necessidade de prosseguir testando outros validadores.
                    return false;
                }
            }
            return true;
        }

        #endregion

        /// <summary>
        /// <para>Obtem os handlers para os eventos atribuidos para <see cref="Control.Validating"/>.</para>
        /// </summary>
        /// <param name="somenteValidadoresDestaClasse">
        /// <para>Quando <c>==true</c>, a abrangência deste método se restringe a validação
        /// atribuida por meio desta classe.</para>
        /// <para>Se <c>==false</c>, afeta qualquer validador atribuido em <see cref="Control.Validating"/>.</para>
        /// </param>
        /// <param name="controle"><para>Controle.</para></param>
        /// <returns>Lista de CancelEventHandler</returns>
        private static CancelEventHandler[] ObterEventosValidating(bool somenteValidadoresDestaClasse, Control controle)
        {
            List<CancelEventHandler> retorno = new List<CancelEventHandler>();

            string name = "EventValidating";

            Type tipoDoControle = controle.GetType();

            do
            {
                FieldInfo[] fields = tipoDoControle.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);

                foreach (FieldInfo field in fields)
                {
                    if (field.Name == name)
                    {
                        EventHandlerList eventHandlers = (EventHandlerList)controle
                            .GetType()
                            .GetProperty("Events", BindingFlags.FlattenHierarchy | (BindingFlags.NonPublic | BindingFlags.Instance))
                            .GetValue(controle, null);

                        Delegate eventHandler = eventHandlers[field.GetValue(controle)];

                        if (eventHandler != null)
                        {
                            Delegate[] handlers = eventHandler.GetInvocationList();

                            ///Encontrado o evento da validação.
                            foreach (Delegate handler in handlers)
                            {
                                if (handler is CancelEventHandler)
                                {
                                    if (!somenteValidadoresDestaClasse || handler.Method.DeclaringType.IsAssignableFrom(typeof(Validar)))
                                    {
                                        retorno.Add(handler as CancelEventHandler);
                                    }
                                }
                            }
                        }
                    }
                }

                tipoDoControle = tipoDoControle.BaseType;

            } while (tipoDoControle != null);

            return retorno.ToArray();
        }
    }
}