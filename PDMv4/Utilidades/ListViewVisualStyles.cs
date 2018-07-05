using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PDMv4.Utilidades
{
    public enum TListView
    {
        Registros, Memoria, Programa, Microinstrucciones, Flags
    }
    /// <summary>
    /// Front-end. Dibuja las tablas, cabeceras y colores de los diferentes controles de tipo ListView
    /// </summary>
    static class ListViewVisualStyles
    {
        public static List<ListView> Listas { get; } = new List<ListView>();

        private static readonly Color colorBackgroundAlternateItem = Estilos.DEFAULT_ITEM_BACKGROUND_ALTERNATE_COLOR;
        private static readonly Color colorBackgroundSelectedItem = Estilos.DEFAULT_SELECTED_ITEM_BACKGROUND_COLOR;
        private static readonly Color colorBackgroundReadItem = Estilos.DEFAULT_READ_ITEM_COLOR;
        private static readonly Color colorBackgroundSelectedReadItem = Estilos.DEFAULT_SELECTED_READ_ITEM_COLOR;
        private static readonly Color colorBackgroundWritenItem = Estilos.DEFAULT_WRITEN_ITEM_COLOR;
        private static readonly Color colorBackgroundSelectedWritenItem = Estilos.DEFAULT_SELECTED_WRITEN_ITEM_COLOR;
        private static List<int> lecturaRegistros = new List<int>();
        private static List<int> accesoRegistros = new List<int>();

        private static List<int> lecturaMemoria = new List<int>();
        private static List<int> accesoMemoria = new List<int>();

        private static List<int> lecturaPrograma = new List<int>();

        private static List<int> lecturaFlags = new List<int>();
        private static List<int> accesoFlags = new List<int>();

        private static int ejecucionMicroInstruccion = -1;

        public static void AñadirIndices(TListView vista, bool escritura = false, params int[] indices)
        {
            switch(vista)
            {
                case TListView.Registros:
                    if(escritura)
                    {
                        foreach (int indice in indices)
                            accesoRegistros.Add(indice);
                    } else
                    {
                        foreach (int indice in indices)
                            lecturaRegistros.Add(indice);
                    }
                    break;
                case TListView.Memoria:
                    if (escritura)
                    {
                        foreach (int indice in indices)
                            accesoMemoria.Add(indice);
                    }
                    else
                    {
                        foreach (int indice in indices)
                            lecturaMemoria.Add(indice);
                    }
                    break;
                case TListView.Programa:
                    lecturaPrograma.Clear();
                    accesoRegistros.Clear();
                    accesoMemoria.Clear();
                    lecturaRegistros.Clear();
                    lecturaMemoria.Clear();
                    lecturaFlags.Clear();
                    accesoFlags.Clear();
                    foreach (int indice in indices)
                        lecturaPrograma.Add(indice);
                    break;
                case TListView.Flags:
                    if (indices == null)
                        break;
                    foreach (int indice in indices)
                        if (escritura)
                            accesoFlags.Add(indice);
                        else lecturaFlags.Add(indice);
                    break;
                case TListView.Microinstrucciones:
                    ejecucionMicroInstruccion = indices[0];
                    break;

            }
        }
        public static void LimpiarIndices(TListView vista, bool todos = false)
        {
            if(todos)
            {
                LimpiarIndices();
            }
            else
            {
                switch(vista)
                {
                    case TListView.Flags:
                        lecturaFlags.Clear();
                        break;
                    case TListView.Memoria:
                        lecturaMemoria.Clear();
                        accesoMemoria.Clear();
                        break;
                    case TListView.Programa:
                        lecturaPrograma.Clear();
                        break;
                    case TListView.Registros:
                        lecturaRegistros.Clear();
                        accesoRegistros.Clear();
                        break;
                    case TListView.Microinstrucciones:
                        ejecucionMicroInstruccion = -1;
                        break;
                }
            }
        }

        public static void LimpiarIndices()
        {
            lecturaRegistros.Clear();
            accesoRegistros.Clear();
            lecturaMemoria.Clear();
            accesoMemoria.Clear();
            lecturaPrograma.Clear();
            lecturaFlags.Clear();
            ejecucionMicroInstruccion = -1;
        }

        public static Color ColorCabecera { get; set; } = Estilos.DEFAULT_HEADER_BACKGROUND_COLOR;

        public static Color ColorBackgroundItem { get; set; } = Estilos.DEFAULT_ITEM_BACKGROUND_COLOR;

        public static Color ColorTextoCabecera { get; set; } = Estilos.DEFAULT_COLOR_TEXT;

        public static Color ColorTextoItem { get; set; } = Estilos.DEFAULT_COLOR_TEXT;

        public static Font FuenteCabecera { get; set; } = Estilos.DEFAULT_FONT;

        public static Font FuenteItem { get; set; } = Estilos.DEFAULT_FONT;
        public static int EjecucionMicroInstruccion { set => ejecucionMicroInstruccion = value; }

        /// <summary>
        /// Método encargado de la renderización de los items de un listView en modo OwnerDraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DibujarItemListView(object sender, DrawListViewItemEventArgs e)
        {
            
        }
        /// <summary>
        /// Método encargado de la renderización de los subitems de un listView en modo OwnerDraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DibujarSubItemListVistaContenidoMapaProcesador(object sender, DrawListViewSubItemEventArgs e)
        {
            ListView listView = sender as ListView;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            if (e.Item.Selected)
            {
                //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(209, 232, 255)), e.Bounds);
                e.Graphics.FillRectangle(new SolidBrush(colorBackgroundSelectedItem), e.Bounds);
                //Usar:
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listView.Font, new Rectangle(new Point(e.Bounds.Location.X + 2, e.Bounds.Location.Y + 1), new Size(e.Bounds.Width - 2, e.Bounds.Height - 1)), SystemColors.HighlightText, TextFormatFlags.ExpandTabs);
                //si el color es muy oscuro y se necesita resaltar el texto en blanco
            }
            else
            {
                Color color = e.ItemIndex % 2 != 0 ? ColorBackgroundItem : colorBackgroundAlternateItem;//Color.Honeydew;
                e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - Constants.BOTTOM_MARGIN));
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, FuenteItem, new Rectangle(new Point(e.Bounds.Location.X + Constants.LEFT_MARGIN, e.Bounds.Location.Y + Constants.BOTTOM_MARGIN), new Size(e.Bounds.Width - Constants.LEFT_MARGIN, e.Bounds.Height - Constants.BOTTOM_MARGIN)), ColorTextoItem, TextFormatFlags.ExpandTabs);
            }
            //TextRenderer.DrawText(e.Graphics, e.SubItem.Text, fuenteItem, new Rectangle(new Point(e.Bounds.Location.X + Constants.LEFT_MARGIN, e.Bounds.Location.Y + Constants.BOTTOM_MARGIN), new Size(e.Bounds.Width - Constants.LEFT_MARGIN, e.Bounds.Height - Constants.BOTTOM_MARGIN)), colorTextoItem, TextFormatFlags.ExpandTabs);
        }


        public static void DibujarSubItemListView(object sender, DrawListViewSubItemEventArgs e, TListView vista)
        {
            ListView listView = sender as ListView;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            List<int> lecturas;
            List<int> escrituras;

            switch(vista)
            {
                case TListView.Registros:
                    lecturas = lecturaRegistros;
                    escrituras = accesoRegistros;
                    break;
                case TListView.Programa:
                    lecturas = lecturaPrograma;
                    escrituras = new List<int>();
                    break;
                case TListView.Memoria:
                    lecturas = lecturaMemoria;
                    escrituras = accesoMemoria;
                    break;
                case TListView.Flags:
                    lecturas = lecturaFlags;
                    escrituras = accesoFlags;
                    break;
                case TListView.Microinstrucciones:
                    lecturas = new List<int>();
                    escrituras = new List<int>();
                    lecturas.Add(ejecucionMicroInstruccion);
                    break;
                default:
                    lecturas = new List<int>();
                    escrituras = new List<int>();
                    break;
            }

            
            if (lecturas.Contains(e.Item.Index))
            {
                //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(209, 232, 255)), e.Bounds);
                e.Graphics.FillRectangle(new SolidBrush(e.Item.Selected ? colorBackgroundSelectedReadItem : colorBackgroundReadItem), e.Bounds);
                //Usar:
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listView.Font, new Rectangle(new Point(e.Bounds.Location.X + 2, e.Bounds.Location.Y + 1), new Size(e.Bounds.Width - 2, e.Bounds.Height - 1)), /*e.Item.Selected ? Color.DarkCyan : */SystemColors.HighlightText, TextFormatFlags.ExpandTabs);
                //si el color es muy oscuro y se necesita resaltar el texto en blanco
            }
            else if (escrituras.Contains(e.Item.Index))
            {
                e.Graphics.FillRectangle(new SolidBrush(e.Item.Selected ? colorBackgroundSelectedWritenItem : colorBackgroundWritenItem), e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listView.Font, new Rectangle(new Point(e.Bounds.Location.X + 2, e.Bounds.Location.Y + 1), new Size(e.Bounds.Width - 2, e.Bounds.Height - 1)), SystemColors.HighlightText, TextFormatFlags.ExpandTabs);
            }
            else if (e.Item.Selected)
            {
                    e.Graphics.FillRectangle(new SolidBrush(colorBackgroundSelectedItem), e.Bounds);
                    //Usar:
                    TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listView.Font, new Rectangle(new Point(e.Bounds.Location.X + 2, e.Bounds.Location.Y + 1), new Size(e.Bounds.Width - 2, e.Bounds.Height - 1)), SystemColors.ControlText, TextFormatFlags.ExpandTabs);
            }
            else
            {
                Color color = e.ItemIndex % 2 != 0 ? ColorBackgroundItem : colorBackgroundAlternateItem;//Color.Honeydew;
                e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - Constants.BOTTOM_MARGIN));
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, FuenteItem, new Rectangle(new Point(e.Bounds.Location.X + Constants.LEFT_MARGIN, e.Bounds.Location.Y + Constants.BOTTOM_MARGIN), new Size(e.Bounds.Width - Constants.LEFT_MARGIN, e.Bounds.Height - Constants.BOTTOM_MARGIN)), ColorTextoItem, TextFormatFlags.ExpandTabs);
            }

            //TextRenderer.DrawText(e.Graphics, e.SubItem.Text, fuenteItem, new Rectangle(new Point(e.Bounds.Location.X + Constants.LEFT_MARGIN, e.Bounds.Location.Y + Constants.BOTTOM_MARGIN), new Size(e.Bounds.Width - Constants.LEFT_MARGIN, e.Bounds.Height - Constants.BOTTOM_MARGIN)), colorTextoItem, TextFormatFlags.ExpandTabs);
        }

        public static void DibujarSubItemListViewMicroInstrucciones(object sender, DrawListViewSubItemEventArgs e)
        {
            ListView listView = sender as ListView;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (e.Item.Index == ejecucionMicroInstruccion)
            {
                //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(209, 232, 255)), e.Bounds);
                e.Graphics.FillRectangle(new SolidBrush(e.Item.Selected ? colorBackgroundSelectedReadItem : colorBackgroundReadItem), e.Bounds);
                //Usar:
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listView.Font, new Rectangle(new Point(e.Bounds.Location.X + 2, e.Bounds.Location.Y + 1), new Size(e.Bounds.Width - 2, e.Bounds.Height - 1)), /*e.Item.Selected ? Color.DarkCyan :*/ SystemColors.HighlightText, TextFormatFlags.ExpandTabs);
                //si el color es muy oscuro y se necesita resaltar el texto en blanco
            }
            else if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(colorBackgroundSelectedItem), e.Bounds);
                //Usar:
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listView.Font, new Rectangle(new Point(e.Bounds.Location.X + 2, e.Bounds.Location.Y + 1), new Size(e.Bounds.Width - 2, e.Bounds.Height - 1)), SystemColors.ControlText, TextFormatFlags.ExpandTabs);
            }
            else
            {
                Color color = e.ItemIndex % 2 != 0 ? ColorBackgroundItem : colorBackgroundAlternateItem;//Color.Honeydew;
                e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - Constants.BOTTOM_MARGIN));
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, FuenteItem, new Rectangle(new Point(e.Bounds.Location.X + Constants.LEFT_MARGIN, e.Bounds.Location.Y + Constants.BOTTOM_MARGIN), new Size(e.Bounds.Width - Constants.LEFT_MARGIN, e.Bounds.Height - Constants.BOTTOM_MARGIN)), ColorTextoItem, TextFormatFlags.ExpandTabs);
            }

            //TextRenderer.DrawText(e.Graphics, e.SubItem.Text, fuenteItem, new Rectangle(new Point(e.Bounds.Location.X + Constants.LEFT_MARGIN, e.Bounds.Location.Y + Constants.BOTTOM_MARGIN), new Size(e.Bounds.Width - Constants.LEFT_MARGIN, e.Bounds.Height - Constants.BOTTOM_MARGIN)), colorTextoItem, TextFormatFlags.ExpandTabs);
        }
        /// <summary>
        /// Método encargado de la renderización de los titulos de cabecera de un listView en modo OwnerDraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DibujarCabeceras(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (Application.VisualStyleState == VisualStyleState.NonClientAreaEnabled) e.DrawDefault = true;
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(ColorCabecera), e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Header.Text, FuenteCabecera, new Rectangle(new Point(e.Bounds.Location.X + Constants.MARGEN_IZQUIERDO_CABECERA, e.Bounds.Location.Y + Constants.MARGEN_ABAJO_CABECERA), new Size(e.Bounds.Width - Constants.MARGEN_IZQUIERDO_CABECERA, e.Bounds.Height - Constants.MARGEN_ABAJO_CABECERA)), ColorTextoCabecera, TextFormatFlags.ExpandTabs);
            }
        }

        public static void CambiarColorFondo()
        {
            foreach (ListView vista in Listas)
                vista.BackColor = ColorBackgroundItem;
        }

        public static void DibujarSubItemListView(object sender, DrawListViewSubItemEventArgs e)
        {
            ListView listView = sender as ListView;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;


            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(colorBackgroundSelectedItem), e.Bounds);
            }
            else
            {
                Color color = e.ItemIndex % 2 != 0 ? ColorBackgroundItem : colorBackgroundAlternateItem;
                e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - Constants.BOTTOM_MARGIN));
                
            }

            Color colorTexto = ColorTextoItem;
            if (e.SubItem.Text == "0")
                colorTexto = Color.Firebrick;
            else if (e.SubItem.Text == "1")
                colorTexto = Color.ForestGreen;
            else if (e.SubItem.Text == "-1")
                e.SubItem.Text = "x";

            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, FuenteItem, new Rectangle(new Point(e.Bounds.Location.X + Constants.LEFT_MARGIN, e.Bounds.Location.Y + Constants.BOTTOM_MARGIN), new Size(e.Bounds.Width - Constants.LEFT_MARGIN, e.Bounds.Height - Constants.BOTTOM_MARGIN)), colorTexto, TextFormatFlags.ExpandTabs);
        }
    }
}
