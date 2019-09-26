using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Suporte
{
    /// <summary>
    /// <para>Funções utilitárias para componentes que herdam de <see cref="Component"/>.</para>
    /// </summary>
    public static class ComponentUtil
    {

        /// <summary>
        /// <para>Dexanexa todos os eventos de um componente, ou controle.</para>
        /// <para>Este método complementa a funcionalidade do método <see cref="Suporte.ComponentUtil.AnexaEventos"/>.</para>
        /// </summary>
        /// <param name="obj"><para>Componente, ou controle.</para></param>
        /// <returns><para>Obtem com a lista de eventos removidos.</para></returns>
        public static EventHandlerList DesanexaEventos(Component obj)
        {
            object objNew = obj.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
            PropertyInfo propEvents = obj.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList eventHandlerList_obj = (EventHandlerList)propEvents.GetValue(obj, null);
            EventHandlerList eventHandlerList_objNew = (EventHandlerList)propEvents.GetValue(objNew, null);

            eventHandlerList_objNew.AddHandlers(eventHandlerList_obj);
            eventHandlerList_obj.Dispose();

            return eventHandlerList_objNew;
        }

        /// <summary>
        /// <para>Anexar eventos a um componente, ou controle.</para>
        /// <para>Este método complementa a funcionalidade do método <see cref="Suporte.ComponentUtil.DesanexaEventos"/>.</para>
        /// </summary>
        /// <param name="obj"><para>Componente, ou controle.</para></param>
        /// <param name="eventos"><para>Lista de eventos que serão anexados.</para></param>
        public static void AnexaEventos(Component obj, EventHandlerList eventos)
        {
            PropertyInfo propEvents = obj.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList eventHandlerList_obj = (EventHandlerList)propEvents.GetValue(obj, null);

            eventHandlerList_obj.AddHandlers(eventos);
        }

        /// <summary>
        /// <para>Ordena os itens de uma <see cref="ComboBox"/></para>
        /// </summary>
        /// <param name="controle"><para>ComboBox</para>.</param>
        public static void OrdenarItens(ComboBox controle)
        {
            List<KeyValuePair<object, string>> dados = new List<KeyValuePair<object, string>>();
            foreach (object item in controle.Items)
            {
                DataRowView row = item as DataRowView;
                if (item == null || row == null)
                {
                    //Item da lista não compatível para ordenação. Abortar sem apresentar erro para o usuário.
                    return;
                }
                string key = row[controle.ValueMember].ToString();
                string value = row[controle.DisplayMember].ToString();
                dados.Add(new KeyValuePair<object, string>(key, value));
            }
            dados.Sort((x, y) => string.Compare(x.Value, y.Value));

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(controle.ValueMember);
            if (controle.ValueMember != controle.DisplayMember)
            {
                dataTable.Columns.Add(controle.DisplayMember);
            }
            foreach (KeyValuePair<object, string> item in dados)
            {
                if (controle.ValueMember != controle.DisplayMember)
                {
                    dataTable.Rows.Add(item.Key, item.Value);
                }
                else
                {
                    dataTable.Rows.Add(item.Key);
                }
            }

            EventHandlerList eventosBkp = DesanexaEventos(controle);
            controle.BeginUpdate();

            int bkpIndex = dados.FindIndex((d) => d.Key == controle.SelectedValue);

            controle.DataSource = dataTable;

            controle.SelectedIndex = bkpIndex;

            controle.EndUpdate();
            AnexaEventos(controle, eventosBkp);
        }

    }
}
