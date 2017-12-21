using ScotTarg.Sessions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScotTarg.SingleTarget
{
    public partial class FormSession : Form
    {
        private string sessionTitle = string.Empty;
        private Discipline _discipline = new Discipline();
        private ShootingSession _session;

        public ShootingSession Session { get { return _session; } }

        public FormSession()
        {
            InitializeComponent();
            sessionTitle = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            cmboShooter.Text = " ";
            cmboCalibre.SelectedIndex = 0;
            int id = Properties.Settings.Default.LastSession + 1;
            _session = new Sessions.ShootingSession(id);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmboShooter_TextUpdate(object sender, EventArgs e)
        {
            txtSessionName.Text = sessionTitle + " - " + cmboShooter.Text;
        }

        private void cmboTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the distance and calibre based on the selected target
            _discipline = new Sessions.Discipline();
            nudDistance.Value = (decimal)_discipline.Distance;
            cmboCalibre.Text = _discipline.AmmoDiameter.ToString("0.0");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _discipline.AmmoDiameter = float.Parse(cmboCalibre.Text);
            _session.SessionName = "txtSessionName.Text";
            _session.Discipline = _discipline; // This should really reflect selection in cmboTarget
            _session.Position = Position.Prone;
            _session.Sighters = true;
            _session.TargetWidth = Properties.Settings.Default.TargetWidth;
            _session.UpdateConstants(Properties.Settings.Default.SpeedOfSound,
                Properties.Settings.Default.MsPerCount);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
