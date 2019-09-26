using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Suporte.Controles.WPF
{
    /// <summary>
    /// <para>Classe que extende as funções de um <see cref="System.Windows.Controls.DataGrid"/>.</para>
    /// </summary>
    public class DataGridEx
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="dataGrid">
        /// <para>Instância do <see cref="System.Windows.Controls.DataGrid"/>.</para>
        /// </param>
        public DataGridEx(DataGrid dataGrid)
        {
            DataGrid = dataGrid;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Instância do <see cref="System.Windows.Controls.DataGrid"/>.</para>
        /// </summary>
        public DataGrid DataGrid
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>Obtem um controle visual dentro de outro controle visual.</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Tipo a ser localizado.</para>
        /// </typeparam>
        /// <param name="parent">
        /// <para>Controle visual onde será feita a busca.</para>
        /// </param>
        /// <returns>
        /// <para>Controle visual dentro de outro controle visual.</para>
        /// </returns>
        private T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        /// <summary>
        /// <para>Obtem uma célula do <see cref="System.Windows.Controls.DataGrid"/>.</para>
        /// </summary>
        /// <param name="rowIndex">
        /// <para>Índice da linha.</para>
        /// </param>
        /// <param name="columnIndex">
        /// <para>Índice da coluna.</para>
        /// </param>
        /// <returns>
        /// <para>Célula obtida.</para>
        /// </returns>
        public DataGridCell Cell(int rowIndex, int columnIndex)
        {
            DataGridRow row = Row(rowIndex);

            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                if (cell == null)
                {
                    //Se não conseguir obter a célula, tenta reposicionar.
                    DataGrid.ScrollIntoView(row, DataGrid.Columns[columnIndex]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                }
                return cell;
            }
            return null;
        }

        /// <summary>
        /// <para>Obtem uma linha do <see cref="System.Windows.Controls.DataGrid"/>.</para>
        /// </summary>
        /// <param name="rowIndex">
        /// <para>Índice da linha.</para>
        /// </param>
        /// <returns>
        /// <para>Linha obtida.</para>
        /// </returns>
        public DataGridRow Row(int rowIndex)
        {
            DataGridRow row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row == null)
            {
                //Se não conseguir obter a linha, tenta reposicionar.
                DataGrid.ScrollIntoView(DataGrid.Items[rowIndex]);
                row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            }
            return row;
        }
    }
}
