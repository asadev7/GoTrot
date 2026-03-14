using System.Drawing;
using System.Windows.Forms;

namespace GoTrot.Services
{
    /// <summary>
    /// Upravljanje dark/light modom za cijelu aplikaciju.
    /// </summary>
    public static class ThemeManager
    {
        public static bool IsDarkMode { get; private set; } = true;

        // Light
        public static Color LightBg => Color.FromArgb(245, 248, 250);
        public static Color LightPanel => Color.White;
        public static Color LightText => Color.FromArgb(33, 37, 41);
        public static Color LightSubText => Color.FromArgb(108, 117, 125);
        public static Color LightBorder => Color.FromArgb(220, 220, 220);

        // Dark
        public static Color DarkBg => Color.FromArgb(22, 22, 34);
        public static Color DarkPanel => Color.FromArgb(30, 30, 46);
        public static Color DarkCard => Color.FromArgb(40, 40, 60);
        public static Color DarkText => Color.FromArgb(220, 220, 235);
        public static Color DarkSubText => Color.FromArgb(140, 140, 160);
        public static Color DarkBorder => Color.FromArgb(60, 60, 80);

        // Accent (isti za oba moda)
        public static Color Accent => Color.FromArgb(13, 158, 138);
        public static Color AccentDark => Color.FromArgb(10, 120, 105);

        public static Color Bg => IsDarkMode ? DarkBg : LightBg;
        public static Color Panel => IsDarkMode ? DarkPanel : LightPanel;
        public static Color Card => IsDarkMode ? DarkCard : LightPanel;
        public static Color Text => IsDarkMode ? DarkText : LightText;
        public static Color Sub => IsDarkMode ? DarkSubText : LightSubText;
        public static Color Border => IsDarkMode ? DarkBorder : LightBorder;

        public static void Toggle() => IsDarkMode = !IsDarkMode;

        /// <summary>
        /// Primijeni temu rekurzivno na sve kontrole forme.
        /// </summary>
        public static void Apply(Form form)
        {
            form.BackColor = Bg;
            ApplyToControls(form.Controls);
        }

        // Poznate light-mode pozadinske boje koje treba zamijeniti u dark modu
        private static readonly Color[] LightBgColors = new[]
        {
            Color.White,
            Color.FromArgb(245, 248, 250),
            Color.FromArgb(235, 245, 255),
            Color.FromArgb(242, 242, 242),
            Color.FromArgb(230, 235, 240),
            Color.FromArgb(220, 220, 220),
            Color.FromArgb(200, 200, 200),
        };

        // Poznate light-mode boje teksta koje treba zamijeniti u dark modu
        private static readonly Color[] LightTextColors = new[]
        {
            Color.FromArgb(33, 37, 41),
            Color.FromArgb(44, 62, 80),
            Color.FromArgb(80, 80, 80),
            Color.FromArgb(101, 103, 107),
            Color.FromArgb(108, 117, 125),
            Color.FromArgb(30, 60, 100),
            Color.Gray,
            Color.Black,
        };

        // Boje redova u tabelama koje se mijenjaju u dark modu
        private static Color MapRowColor(Color c)
        {
            if (!IsDarkMode) return c;
            // Plava dostupan
            if (c == Color.FromArgb(213, 232, 252)) return Color.FromArgb(20, 55, 85);
            // Crvena nedostupan
            if (c == Color.FromArgb(255, 204, 204)) return Color.FromArgb(75, 25, 25);
            // Zelena dostupan (servisi tab)
            if (c == Color.FromArgb(213, 245, 213)) return Color.FromArgb(20, 65, 35);
            // Žuta punjenje
            if (c == Color.FromArgb(255, 243, 205)) return Color.FromArgb(70, 55, 10);
            // Plava u upotrebi
            if (c == Color.FromArgb(213, 232, 252)) return Color.FromArgb(20, 55, 85);
            // Crvena nova notifikacija
            if (c == Color.FromArgb(255, 220, 220)) return Color.FromArgb(80, 20, 20);
            // Baby blue
            if (c == Color.FromArgb(235, 245, 255)) return Color.FromArgb(25, 40, 65);
            return c;
        }

        private static bool IsLightBg(Color c) =>
            LightBgColors.Any(l => l.ToArgb() == c.ToArgb());

        private static bool IsLightText(Color c) =>
            LightTextColors.Any(l => l.ToArgb() == c.ToArgb());

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                switch (ctrl)
                {
                    case DataGridView dgv:
                        dgv.BackgroundColor = Panel;
                        dgv.GridColor = Border;
                        dgv.DefaultCellStyle.BackColor = Panel;
                        dgv.DefaultCellStyle.ForeColor = Text;
                        dgv.DefaultCellStyle.SelectionBackColor = Accent;
                        dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                        if (IsDarkMode)
                        {
                            dgv.AlternatingRowsDefaultCellStyle.BackColor = Card;
                            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Text;
                        }
                        else
                        {
                            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 245, 255);
                            dgv.AlternatingRowsDefaultCellStyle.ForeColor = LightTextColors[0];
                        }
                        // Zamijeni boje postojećih redova
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            var bc = row.DefaultCellStyle.BackColor;
                            if (bc != Color.Empty && bc.ToArgb() != 0)
                            {
                                row.DefaultCellStyle.BackColor = MapRowColor(bc);
                                row.DefaultCellStyle.SelectionBackColor = MapRowColor(bc);
                            }
                            row.DefaultCellStyle.ForeColor = Text;
                            row.DefaultCellStyle.SelectionForeColor = Color.White;
                        }
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = IsDarkMode ? DarkCard : Color.FromArgb(44, 62, 80);
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgv.EnableHeadersVisualStyles = false;
                        break;

                    case TabControl tc:
                        tc.BackColor = Bg;
                        foreach (TabPage tp in tc.TabPages)
                        {
                            tp.BackColor = Bg;
                            ApplyToControls(tp.Controls);
                        }
                        break;

                    case TableLayoutPanel tlp:
                        if (ShouldRecolorBg(tlp.BackColor))
                            tlp.BackColor = Bg;
                        ApplyToControls(tlp.Controls);
                        break;

                    case FlowLayoutPanel fp:
                        if (ShouldRecolorBg(fp.BackColor))
                            fp.BackColor = Bg;
                        ApplyToControls(fp.Controls);
                        break;

                    case Panel p:
                        if (ShouldRecolorBg(p.BackColor))
                            p.BackColor = Bg;
                        ApplyToControls(p.Controls);
                        break;

                    case Label lbl:
                        if (lbl.ForeColor != Color.White &&
                            lbl.ForeColor != Accent &&
                            lbl.ForeColor != AccentDark &&
                            lbl.ForeColor != Color.FromArgb(180, 230, 180) &&
                            lbl.ForeColor != Color.FromArgb(180, 220, 255) &&
                            lbl.ForeColor != Color.FromArgb(200, 255, 245) &&
                            lbl.ForeColor != Color.FromArgb(210, 235, 255) &&
                            lbl.ForeColor != Color.FromArgb(243, 156, 18) &&  // zvjezdice
                            lbl.ForeColor != Color.FromArgb(211, 84, 0) &&    // timer
                            lbl.ForeColor != Color.FromArgb(39, 174, 96))     // zelena bonus
                        {
                            if (IsLightText(lbl.ForeColor) || lbl.ForeColor == Color.FromArgb(44, 62, 80))
                                lbl.ForeColor = Text;
                        }
                        if (ShouldRecolorBg(lbl.BackColor))
                            lbl.BackColor = Color.Transparent;
                        break;

                    case TextBox tb:
                        tb.BackColor = Card;
                        tb.ForeColor = Text;
                        break;

                    case ComboBox cb:
                        cb.BackColor = Card;
                        cb.ForeColor = Text;
                        break;

                    case NumericUpDown nud:
                        nud.BackColor = Card;
                        nud.ForeColor = Text;
                        break;

                    case CheckBox chk:
                        if (ShouldRecolorBg(chk.BackColor))
                            chk.BackColor = Color.Transparent;
                        if (IsLightText(chk.ForeColor))
                            chk.ForeColor = Text;
                        break;

                    case Button btn:
                        // Ne mijenjaj accent i bojene gumbe
                        if (btn.BackColor != Accent && btn.BackColor != AccentDark &&
                            btn.BackColor != Color.FromArgb(192, 57, 43) &&
                            btn.BackColor != Color.FromArgb(231, 76, 60) &&
                            btn.BackColor != Color.FromArgb(41, 128, 185) &&
                            btn.BackColor != Color.FromArgb(211, 84, 0) &&
                            btn.BackColor != Color.FromArgb(39, 174, 96) &&
                            btn.BackColor != Color.FromArgb(52, 152, 219) &&
                            btn.BackColor != Color.FromArgb(155, 89, 182) &&
                            btn.BackColor != Color.FromArgb(52, 73, 94) &&
                            btn.BackColor != Color.FromArgb(120, 40, 31) &&
                            btn.BackColor != Color.FromArgb(13, 158, 138) &&
                            btn.BackColor != Color.FromArgb(24, 119, 242))
                        {
                            btn.BackColor = IsDarkMode ? DarkCard : Color.FromArgb(230, 235, 240);
                            btn.ForeColor = Text;
                        }
                        break;

                    default:
                        if (ShouldRecolorBg(ctrl.BackColor))
                            ctrl.BackColor = Bg;
                        if (IsLightText(ctrl.ForeColor))
                            ctrl.ForeColor = Text;
                        break;
                }

                if (ctrl.HasChildren && !(ctrl is TabControl) && !(ctrl is DataGridView))
                    ApplyToControls(ctrl.Controls);
            }
        }

        private static bool ShouldRecolorBg(Color c)
        {
            if (!IsDarkMode) return false;
            return IsLightBg(c) || c == Color.FromArgb(245, 248, 250) || c == Color.White;
        }
    }
}