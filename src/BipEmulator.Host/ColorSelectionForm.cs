using System.Drawing;
using System.Windows.Forms;

namespace BipEmulator.Host
{
    public partial class ColorSelectionForm : Form
    {
        public ColorArray Colors { get; set; }

        public ColorSelectionForm(ColorArray colors)
        {
            InitializeComponent();

            Colors = colors;

            SetColor(lblBlack, Colors.Black);
            SetColor(lblRed, Colors.Red);
            SetColor(lblGreen, Colors.Green);
            SetColor(lblYellow, Colors.Yellow);
            SetColor(lblAqua, Colors.Aqua);
            SetColor(lblPurple,Colors.Purple);
            SetColor(lblWhite,Colors.White);
            SetColor(lblBlue,Colors.Blue);
        }

        private void SetColor(Label label, Color color)
        {
            label.BackColor = color;
            label.ForeColor = Color.FromArgb(~color.ToArgb());
        }

        private void ColorLabel_Click(object sender, System.EventArgs e)
        {
            var label = (Label)sender;

            var frm = new ColorDialog();
            frm.FullOpen = true;
            frm.Color = label.BackColor;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                SetColor(label, frm.Color);
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            Colors = new ColorArray
            {
                Black = lblBlack.BackColor,
                Red = lblRed.BackColor,
                Green = lblGreen.BackColor,
                Yellow = lblYellow.BackColor,
                Aqua = lblAqua.BackColor,
                Purple = lblPurple.BackColor,
                White = lblWhite.BackColor,
                Blue = lblBlue.BackColor
            };
        }
    }
}
