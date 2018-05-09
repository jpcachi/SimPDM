using System.Windows.Forms;

namespace PDMv4.Controles
{
    public partial class CustomButton : Button
    {
        protected override bool ShowFocusCues
        {
            get
            {
                //return base.ShowFocusCues;
                return false;
            }
        }

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }
    }
}
