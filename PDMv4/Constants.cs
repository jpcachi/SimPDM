using System.Drawing;

namespace PDMv4
{
    static class Constants
    {
        public const int LEFT_MARGIN = 2;
        public const int BOTTOM_MARGIN = 1;
        public const int MARGEN_EXTERIOR_IZQUIERDO_ARRASTRAR = -50;
        public const int MARGEN_EXTERIOR_BAJO_ARRASTRAR = -12;
        public const int MARGEN_EXTERIOR_BAJO_COMENZAR_ARRASTRAR = 0;
        public const int MARGEN_IZQUIERDO_CABECERA = 3;
        public const int MARGEN_ABAJO_CABECERA = 5;
        public const int MARGEN_TEXTO_RESALTADO_IZQUIERDO = 3;
        public const int MARGEN_TEXTO_RESALTADO_IZQUIERDO_AL_FINAL = 10;

        public static readonly Color DEFAULT_HEADER_BACKGROUND_COLOR = SystemColors.InactiveCaption;//Color.AliceBlue;//SystemColors.Control;
        public static readonly Color DEFAULT_COLOR_TEXT = SystemColors.WindowText;
        public static readonly Color DEFAULT_ITEM_BACKGROUND_COLOR = SystemColors.Window;
        public static readonly Color DEFAULT_ITEM_BACKGROUND_ALTERNATE_COLOR = Color.AliceBlue;
        public static readonly Color DEFAULT_SELECTED_ITEM_BACKGROUND_COLOR = Color.LightSkyBlue;
        public static readonly Color DEFAULT_READ_ITEM_COLOR = Color.DarkCyan;
        public static readonly Color DEFAULT_SELECTED_READ_ITEM_COLOR = Color.FromArgb(0, 90, 134);
        public static readonly Color DEFAULT_WRITEN_ITEM_COLOR = Color.Firebrick;
        public static readonly Color DEFAULT_SELECTED_WRITEN_ITEM_COLOR = Color.FromArgb(58, 0, 29);
        public static readonly Font DEFAULT_FONT = SystemFonts.DefaultFont;


        public static readonly Color DEFAULT_PANEL_HEADER_COLOR = SystemColors.ControlLight;//Color.CadetBlue;
        public static readonly Color DEFAULT_PANEL_TEXT_HEADER_COLOR = SystemColors.ControlDarkDark;//Color.White;
        public static readonly Color DEFAULT_PANEL_COLOR = Color.White;//SystemColors.Control;
        public static readonly Color DEFAULT_WINDOW_BACKGROUND_COLOR = Color.WhiteSmoke;//Color.White;
    }
}
