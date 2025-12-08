using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MunicipalServices
{
    public partial class MenuScreen : Form
    {
        private Button btnReportIssues;
        private Button btnLocalEvents;
        private Button btnCheckStatus;

        public MenuScreen()
        {
            InitializeComponent();

            this.Text = "Municipal Service Portal";

            btnReportIssues = new Button();
            btnLocalEvents = new Button();
            btnCheckStatus = new Button();


            this.Resize += ReadjustButtons;

            btnReportIssues.Name = "reportIssues";
            btnReportIssues.Text = "Report Issues";
            btnReportIssues.Size = new Size(100, 40);

            int center_X_value = (this.ClientSize.Width - btnReportIssues.Width) / 2;
            int center_Y_value = (this.ClientSize.Height - btnReportIssues.Height) / 2;

            btnReportIssues.Left = center_X_value;
            btnReportIssues.Top = center_Y_value - 50;

            btnReportIssues.Click += ReportIssues;

            btnLocalEvents.Name = "localEvents";
            btnLocalEvents.Left = center_X_value;
            btnLocalEvents.Top = center_Y_value;
            btnLocalEvents.Text = "Local Events";
            btnLocalEvents.Size = new Size(100, 40);

            btnLocalEvents.Click += LocalEvents;

            btnCheckStatus.Name = "checkStatus";
            btnCheckStatus.Left = center_X_value;
            btnCheckStatus.Top = center_Y_value + 50;
            btnCheckStatus.Text = "View Request Status";
            btnCheckStatus.Size = new Size(100, 40);

            btnCheckStatus.Click += CheckStatus;

            this.Controls.Add(btnReportIssues);
            this.Controls.Add(btnLocalEvents);
            this.Controls.Add(btnCheckStatus);
        }

        private void ReportIssues(object sender, EventArgs e)
        {
            ReportIssues reportForm = new ReportIssues();
            this.Hide();
            reportForm.Show();

        }

        private void LocalEvents(object sender, EventArgs e)
        {
            LocalEvents eventsForm = new LocalEvents();
            this.Hide();
            eventsForm.Show();
        }

        private void CheckStatus(object sender, EventArgs e)
        {
            CheckStatus statusForm = new CheckStatus();
            this.Hide();
            statusForm.Show();
        }

        private void ReadjustButtons(object sender, EventArgs e)
        {
            int center_X_value = (this.ClientSize.Width - btnReportIssues.Width) / 2;
            int center_Y_value = (this.ClientSize.Height - btnReportIssues.Height) / 2;

            btnReportIssues.Left = center_X_value;
            btnReportIssues.Top = center_Y_value - 50;

            btnLocalEvents.Left = center_X_value;
            btnLocalEvents.Top = center_Y_value;

            btnCheckStatus.Left = center_X_value;
            btnCheckStatus.Top = center_Y_value + 50;

        }
    }
}
