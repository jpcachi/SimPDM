using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMv4.Temas
{
    public class Estilos
    {
        private static Estilos _estilos;
        public static Estilos GetStyle()
        {
            if (_estilos == null)
                _estilos = new Estilos();

            return _estilos;
        }

        public static bool EsColorOscuro(Color col)
        {
            return (col.R * 0.2126 + col.G * 0.7152 + col.B * 0.0722 < 255 / 2);
        }

        /* TEMA CLARO*/
        public readonly Color DEFAULT_HEADER_BACKGROUND_COLOR = SystemColors.InactiveCaption;
        public readonly Color DEFAULT_COLOR_TEXT = SystemColors.WindowText;
        public readonly Color DEFAULT_ITEM_BACKGROUND_COLOR = SystemColors.Window;
        public readonly Color DEFAULT_ITEM_BACKGROUND_ALTERNATE_COLOR = Color.AliceBlue;
        public readonly Color DEFAULT_GRID_COLOR = SystemColors.Control;
        public readonly Color DEFAULT_SELECTED_ITEM_BACKGROUND_COLOR = Color.LightSkyBlue;
        public readonly Color DEFAULT_READ_ITEM_COLOR = Color.DarkCyan;
        public readonly Color DEFAULT_SELECTED_READ_ITEM_COLOR = Color.FromArgb(0, 90, 134);
        public readonly Color DEFAULT_WRITEN_ITEM_COLOR = Color.Firebrick;
        public readonly Color DEFAULT_SELECTED_WRITEN_ITEM_COLOR = Color.FromArgb(58, 0, 29);
        public readonly Font DEFAULT_FONT = SystemFonts.DefaultFont;


        public readonly Color DEFAULT_PANEL_HEADER_COLOR = SystemColors.ControlLight;
        public readonly Color DEFAULT_PANEL_TEXT_HEADER_COLOR = SystemColors.ControlDarkDark;
        public readonly Color DEFAULT_PANEL_COLOR = Color.White;
        public readonly Color DEFAULT_WINDOW_BACKGROUND_COLOR = Color.WhiteSmoke;
        public readonly Color DEFAULT_PANEL_BORDER_COLOR = Color.White;

        public readonly Color DEFAULT_STATUS_STRIP_COLOR = SystemColors.ActiveCaption;
        public readonly Color RUNNING_STATUS_STRIP_COLOR = Color.OrangeRed;

        private Estilos()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            DEFAULT_HEADER_BACKGROUND_COLOR         = appSettings["DEFAULT_HEADER_BACKGROUND_COLOR"]          == null ? DEFAULT_HEADER_BACKGROUND_COLOR           : ColorTranslator.FromHtml(appSettings["DEFAULT_HEADER_BACKGROUND_COLOR"]);
            DEFAULT_COLOR_TEXT                      = appSettings["DEFAULT_COLOR_TEXT"]                       == null ? DEFAULT_COLOR_TEXT                        : ColorTranslator.FromHtml(appSettings["DEFAULT_COLOR_TEXT"]);
            DEFAULT_ITEM_BACKGROUND_COLOR           = appSettings["DEFAULT_ITEM_BACKGROUND_COLOR"]            == null ? DEFAULT_ITEM_BACKGROUND_COLOR             : ColorTranslator.FromHtml(appSettings["DEFAULT_ITEM_BACKGROUND_COLOR"]);
            DEFAULT_ITEM_BACKGROUND_ALTERNATE_COLOR = appSettings["DEFAULT_ITEM_BACKGROUND_ALTERNATE_COLOR"]  == null ? DEFAULT_ITEM_BACKGROUND_ALTERNATE_COLOR   : ColorTranslator.FromHtml(appSettings["DEFAULT_ITEM_BACKGROUND_ALTERNATE_COLOR"]);
            DEFAULT_GRID_COLOR                      = appSettings["DEFAULT_GRID_COLOR"]                       == null ? DEFAULT_GRID_COLOR                        : ColorTranslator.FromHtml(appSettings["DEFAULT_GRID_COLOR"]);
            DEFAULT_SELECTED_ITEM_BACKGROUND_COLOR  = appSettings["DEFAULT_SELECTED_ITEM_BACKGROUND_COLOR"]   == null ? DEFAULT_SELECTED_ITEM_BACKGROUND_COLOR    : ColorTranslator.FromHtml(appSettings["DEFAULT_SELECTED_ITEM_BACKGROUND_COLOR"]);
            DEFAULT_READ_ITEM_COLOR                 = appSettings["DEFAULT_READ_ITEM_COLOR"]                  == null ? DEFAULT_READ_ITEM_COLOR                   : ColorTranslator.FromHtml(appSettings["DEFAULT_READ_ITEM_COLOR"]);
            DEFAULT_SELECTED_READ_ITEM_COLOR        = appSettings["DEFAULT_SELECTED_READ_ITEM_COLOR"]         == null ? DEFAULT_SELECTED_READ_ITEM_COLOR          : ColorTranslator.FromHtml(appSettings["DEFAULT_SELECTED_READ_ITEM_COLOR"]);
            DEFAULT_WRITEN_ITEM_COLOR               = appSettings["DEFAULT_WRITEN_ITEM_COLOR"]                == null ? DEFAULT_WRITEN_ITEM_COLOR                 : ColorTranslator.FromHtml(appSettings["DEFAULT_WRITEN_ITEM_COLOR"]);
            DEFAULT_SELECTED_WRITEN_ITEM_COLOR      = appSettings["DEFAULT_SELECTED_WRITEN_ITEM_COLOR"]       == null ? DEFAULT_SELECTED_WRITEN_ITEM_COLOR        : ColorTranslator.FromHtml(appSettings["DEFAULT_SELECTED_WRITEN_ITEM_COLOR"]);
            DEFAULT_PANEL_HEADER_COLOR              = appSettings["DEFAULT_PANEL_HEADER_COLOR"]               == null ? DEFAULT_PANEL_HEADER_COLOR                : ColorTranslator.FromHtml(appSettings["DEFAULT_PANEL_HEADER_COLOR"]);
            DEFAULT_PANEL_BORDER_COLOR              = appSettings["DEFAULT_PANEL_BORDER_COLOR"]               == null ? DEFAULT_PANEL_BORDER_COLOR                : ColorTranslator.FromHtml(appSettings["DEFAULT_PANEL_BORDER_COLOR"]);
            DEFAULT_PANEL_TEXT_HEADER_COLOR         = appSettings["DEFAULT_PANEL_TEXT_HEADER_COLOR"]          == null ? DEFAULT_PANEL_TEXT_HEADER_COLOR           : ColorTranslator.FromHtml(appSettings["DEFAULT_PANEL_TEXT_HEADER_COLOR"]);
            DEFAULT_PANEL_COLOR                     = appSettings["DEFAULT_PANEL_COLOR"]                      == null ? DEFAULT_PANEL_COLOR                       : ColorTranslator.FromHtml(appSettings["DEFAULT_PANEL_COLOR"]);
            DEFAULT_WINDOW_BACKGROUND_COLOR         = appSettings["DEFAULT_WINDOW_BACKGROUND_COLOR"]          == null ? DEFAULT_WINDOW_BACKGROUND_COLOR           : ColorTranslator.FromHtml(appSettings["DEFAULT_WINDOW_BACKGROUND_COLOR"]);
            DEFAULT_STATUS_STRIP_COLOR              = appSettings["DEFAULT_STATUS_STRIP_COLOR"]               == null ? DEFAULT_STATUS_STRIP_COLOR                : ColorTranslator.FromHtml(appSettings["DEFAULT_STATUS_STRIP_COLOR"]);
            RUNNING_STATUS_STRIP_COLOR              = appSettings["RUNNING_STATUS_STRIP_COLOR"]               == null ? RUNNING_STATUS_STRIP_COLOR                : ColorTranslator.FromHtml(appSettings["RUNNING_STATUS_STRIP_COLOR"]);
        }
    }
}
