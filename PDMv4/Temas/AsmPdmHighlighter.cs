using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PDMv4.Temas
{
    class AsmPdmHighlighter : SyntaxHighlighter
    {

        public readonly Style CommentGreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public readonly Style InstruccionStyle = new TextStyle(new SolidBrush(Color.FromArgb(40, 40, 204)), null, FontStyle.Regular);
        public readonly Style RegistroStyle = new TextStyle(new SolidBrush(Color.FromArgb(43, 145, 175)), null, FontStyle.Regular);
        public readonly Style NumberLiteralStyle = new TextStyle(new SolidBrush(Color.FromArgb(248, 107, 66)), null, FontStyle.Regular);
        public readonly Style EtiquetaStyle = new TextStyle(Brushes.Firebrick, null, FontStyle.Regular);
        public readonly Style AddressStyle = new TextStyle(new SolidBrush(Color.FromArgb(167, 47, 161)), null, FontStyle.Regular);
        public readonly Style CommaStyle = new TextStyle(new SolidBrush(Color.FromArgb(56, 58, 66)), null, FontStyle.Regular);

        protected Regex instruccionRegex;
        protected Regex registrosRegex;
        protected Regex registrosRegex2;
        protected Regex registrosRegex3;
        protected Regex registrosRegex4;
        protected Regex numberRegex;
        protected Regex hexNumber;
        protected Regex addressRegex;
        protected Regex addressRegex2;
        protected Regex commaRegex;
        protected Regex etiquetasRegex;
        protected Regex etiquetasRegex2;
        protected Regex etiquetasRegex3;

        public AsmPdmHighlighter(FastColoredTextBox currentTb) : base(currentTb) { }

        protected void InitASMPDMRegex()
        {
            etiquetasRegex = new Regex(@"(?<=(^|\n)(\s*))\b[a-zA-Z]\S*\b(?=\s+\b(LD|ST|LDM|STM|LDI|ADD|SUB|CMP|INC|ADI|SUI|CMI|ANA|ORA|XRA|CMA|ANI|ORI|XRI|JMP|BEQ|BC|LF|IN|OUT|LMR|SMR)\b)");
            instruccionRegex = new Regex(@"(?<=(^|\n)(\s*\b\w*\b\s+|\s*))\b(LD|ST|LDM|STM|LDI|ADD|SUB|CMP|INC|ADI|SUI|CMI|ANA|ORA|XRA|CMA|ANI|ORI|XRI|JMP|BEQ|BC|LF|IN|OUT|LMR|SMR)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            registrosRegex = new Regex(@"(?<=\b(LD|ST|ADD|SUB|CMP|ANA|ORA|XRA|STM)\b\s+)\b(B|C|D|E)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            registrosRegex2 = new Regex(@"(?<=\b(IN|LDM)\b\s+((@?[0-9A-Fa-f]{1,4}h?)|\b\w+\b)\s*,\s*)\b(B|C|D|E)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            registrosRegex3 = new Regex(@"(?<=\bLDI\b\s+\b(\d+|[0-9A-Fa-f]{1,2}h?)\b\s*,\s*)\b(B|C|D|E)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            registrosRegex4 = new Regex(@"(?<=\bSMR\b\s+\b(B|C|D|E)\b\s*,\s*)\b(B|C|D|E)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            numberRegex = new Regex(@"(?<=\b(LDI|ADI|SUI|CMI|ANI|ORI|XRI)\b\s+)\b\d+\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            hexNumber = new Regex(@"(?<=\b(LDI|ADI|SUI|CMI|ANI|ORI|XRI)\b\s+)\b[0-9A-Fa-f]{1,2}h?\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            addressRegex = new Regex(@"(?<=\b(IN|LDM|JMP|BEQ|BC)\b\s+)@?[0-9A-Fa-f]{1,4}h?\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            addressRegex2 = new Regex(@"(?<=\bSTM\b\s+\b(B|C|D|E)\b\s*,\s*)@?[0-9A-Fa-f]{1,4}h?\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            etiquetasRegex2 = new Regex(@"(?<=\b(IN|LDM|JMP|BEQ|BC)\b\s+)\b[a-zA-Z]\S*\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            etiquetasRegex3 = new Regex(@"(?<=\bSTM\b\s+\b(B|C|D|E)\b\s*,\s*)\b[a-zA-Z]\S*\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            commaRegex = new Regex(@",", RegexCompiledOption);

        }

        /// <summary>
        /// Highlights ASM PDM code
        /// </summary>
        /// <param name="range"></param>
        public virtual void ASMPDMSyntaxHighlight(Range range)
        {
            if (instruccionRegex == null)
                InitASMPDMRegex();

            range.ClearStyle(CommentGreenStyle, InstruccionStyle, RegistroStyle, NumberLiteralStyle, AddressStyle, CommaStyle);
            range.SetStyle(CommentGreenStyle, @";.*$", RegexOptions.Multiline);
            
            range.SetStyle(InstruccionStyle, instruccionRegex);
            range.SetStyle(RegistroStyle, registrosRegex);
            range.SetStyle(RegistroStyle, registrosRegex2);
            range.SetStyle(RegistroStyle, registrosRegex3);
            range.SetStyle(RegistroStyle, registrosRegex4);
            range.SetStyle(NumberLiteralStyle, numberRegex);
            range.SetStyle(NumberLiteralStyle, hexNumber);
            range.SetStyle(AddressStyle, addressRegex);
            range.SetStyle(AddressStyle, addressRegex2);
            range.SetStyle(EtiquetaStyle, etiquetasRegex2);
            range.SetStyle(EtiquetaStyle, etiquetasRegex3);
            range.SetStyle(CommaStyle, commaRegex);
            range.SetStyle(EtiquetaStyle, etiquetasRegex);
        }

        public override void HighlightSyntax(Language language, Range range)
        {
            switch(language)
            {
                case Language.Custom:
                    ASMPDMSyntaxHighlight(range);
                    break;
                default:
                    base.HighlightSyntax(language, range);
                    break;
            }
        }
    }
}
