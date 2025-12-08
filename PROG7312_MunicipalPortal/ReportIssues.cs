using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace PROG7312_MunicipalPortal
{
    public partial class ReportIssues : Form
    {
        private List<ServiceIssue> serviceIssues = new List<ServiceIssue>();
        private List<string> attachedMedia = new List<string>();

        private TextBox txtLocation;
        private ComboBox cmbxCategory;
        private RichTextBox rtxtDescription;
        private Button btnAttach;
        private Button btnSubmit;
        private Button btnMenu;
        private Label lblFeedback;
        private ProgressBar prgbrReport;
        private OpenFileDialog fileDialog;
        private Label lblHeading;
        private PictureBox imgLogo;
        private Panel pnlBorder;

        private bool locationFeedbackGiven = false;
        private bool descriptionFeedbackGiven = false;

        private string otherCategory = "";


        public ReportIssues()
        {
            InitializeComponent();

            this.Text = "Report Service Issue/Request";
            this.Size = new Size(640, 800);
            createForm();
        }

        private void createForm()
        {
            txtLocation = new TextBox();
            cmbxCategory = new ComboBox();
            rtxtDescription = new RichTextBox();
            btnAttach = new Button();
            btnSubmit = new Button();
            btnMenu = new Button();
            lblFeedback = new Label();
            prgbrReport = new ProgressBar();
            fileDialog = new OpenFileDialog();
            lblHeading = new Label();
            pnlBorder = new Panel();
            imgLogo = new PictureBox();

            /*--------------------------------------------- Header Controls and Setup -------------------------------------------------------*/

            imgLogo.Image = Properties.Resources.CPT_Logo;
            imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            imgLogo.Size = new Size(this.ClientSize.Width, 150);
            imgLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            lblHeading.Text = "Report Issue";
            lblHeading.Top = 170;
            lblHeading.Left = (this.ClientSize.Width - lblHeading.Width) / 2; ;

            pnlBorder.BorderStyle = BorderStyle.FixedSingle;
            pnlBorder.Top = 210;
            pnlBorder.Left = 10;
            pnlBorder.Width = 600;
            pnlBorder.Height = 500;

            /*-------------------------------------------- Location Controls and Setup ------------------------------------------------------*/

            Label lblLocation = new Label();

            lblLocation.Text = "Location: ";
            lblLocation.Top = 20;
            lblLocation.Left = 20;
            lblLocation.Width = 100;

            txtLocation.Top = 20;
            txtLocation.Left = 150;
            txtLocation.Width = 200;

            txtLocation.TextChanged += txtLocation_TextChanged;

            /*-------------------------------------------- Category Controls and Setup ------------------------------------------------------*/

            Label lblCategory = new Label();

            lblCategory.Text = "Category: ";
            lblCategory.Top = 60;
            lblCategory.Left = 20;
            lblCategory.Width = 100;

            cmbxCategory.Top = 60;
            cmbxCategory.Left = 150;
            cmbxCategory.Width = 200;

            cmbxCategory.Items.AddRange(new string[]
            {
                "Electricity",
                "Water Supply",
                "Roads",
                "Sanitation",
                "Utilities",
                "Parks and Forrestation",
                "Other"

            });

            cmbxCategory.SelectedIndexChanged += GetCategory;                

            /*-------------------------------------------- Description Controls and Setup ------------------------------------------------------*/

            Label lblDescription = new Label();

            lblDescription.Text = "Description: ";
            lblDescription.Top = 100;
            lblDescription.Left = 20;
            lblDescription.Width = 200;

            rtxtDescription.Top = 130;
            rtxtDescription.Left = 20;
            rtxtDescription.Width = 400;
            rtxtDescription.Height = 100;

            rtxtDescription.TextChanged += txtDescription_TextChanged;

            /*-------------------------------------------- Attach Media Controls and Setup ------------------------------------------------------*/

            btnAttach.Text = "Attach Evidence";
            btnAttach.Top = 250;
            btnAttach.Left = 20;
            btnAttach.Width = 150;

            btnAttach.Click += AttachMedia;

            fileDialog.Multiselect = true;
            fileDialog.Filter = "Image Files (*.png; *.jpg)|*.png;*jpg|All Files|*.*";

            /*-------------------------------------------- Progress Bar Controls and Setup ------------------------------------------------------*/

            prgbrReport.Top = 290;
            prgbrReport.Left = 20;
            prgbrReport.Width = 500;
            prgbrReport.Height = 50;
            prgbrReport.Minimum = 0;
            prgbrReport.Maximum = 100;
            prgbrReport.Value = 0;

            /*-------------------------------------------- Engagement Controls and Setup ------------------------------------------------------*/

            lblFeedback.Text = "Complete the form to submit a request/issue, start by entering your Ward Number as a location";
            lblFeedback.Top = 370;
            lblFeedback.Left = 20;
            lblFeedback.Width = 400;

            /*-------------------------------------------- Submission Controls and Setup ------------------------------------------------------*/

            btnSubmit.Text = "Submit";
            btnSubmit.Top = 410;
            btnSubmit.Left = 20;
            btnSubmit.Width = 150;
            btnSubmit.Click += SubmitIssue;

            /*-------------------------------------------- Navigation Controls and Setup ------------------------------------------------------*/

            btnMenu.Text = "Back to Menu";
            btnMenu.Top = 440;
            btnMenu.Left = 20;
            btnMenu.Width = 150;
            btnMenu.Click += BackToMenu;

            /*------------------------------------------------ Controls Added to Form ---------------------------------------------------------*/

            this.Controls.Add(imgLogo);
            this.Controls.Add(lblHeading);
            this.Controls.Add(pnlBorder);
            pnlBorder.Controls.Add(lblLocation);
            pnlBorder.Controls.Add(txtLocation);
            pnlBorder.Controls.Add(lblCategory);
            pnlBorder.Controls.Add(cmbxCategory);
            pnlBorder.Controls.Add(lblDescription);
            pnlBorder.Controls.Add(rtxtDescription);
            pnlBorder.Controls.Add(btnAttach);
            pnlBorder.Controls.Add(prgbrReport);
            pnlBorder.Controls.Add(lblFeedback);
            pnlBorder.Controls.Add(btnSubmit);
            pnlBorder.Controls.Add(btnMenu);
        }

        private void AttachMedia(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                attachedMedia.AddRange(fileDialog.FileNames);
                Label lblFile = new Label();

                lblFile.Top = 250;
                lblFile.Left = 170;
                lblFile.Text = string.Join(", ", fileDialog.FileNames);
                lblFile.AutoSize = true;

                pnlBorder.Controls.Add(lblFile);

                UpdateFeedback(3);
            }
        }

        private void SubmitIssue(object sender, EventArgs e)
        {
            if (ValidEntry() == false)
            {
                MessageBox.Show("Please ensure all details are completed before submitting your issue");
            }
            else
            {
                ServiceIssue issue = new ServiceIssue()
                {
                    Location = txtLocation.Text,
                    Category = otherCategory,                    
                    Description = rtxtDescription.Text,
                    MediaFiles = new List<String>(attachedMedia)
                };

                serviceIssues.Add(issue);

                lblFeedback.Text = "Issues sucessfully submitted! Thank you!";
                prgbrReport.Value = 100;

                MessageBox.Show("Issues sucessfully submitted! Thank you!");
            }
        }

        private void BackToMenu(object sender, EventArgs e)
        {
            MenuScreen menuForm = new MenuScreen();

            this.Hide();
            menuForm.Show();
        }

        private bool ValidEntry()
        {
            if (txtLocation.Text.Length == 0)
            {
                return false;
            }

            if (rtxtDescription.Text.Length == 0)
            {
                return false;
            }

            if (cmbxCategory.SelectedIndex == -1)
            {
                return false;
            }

            if ((!txtLocation.Text.Contains("Ward")) && (!txtLocation.Text.Contains("ward")))
            {
                MessageBox.Show("Please ensure you indicate your ward in your location. E.g -> Ward 30 - 23 Butterfly Road");
                return false;
            }

            return true;

        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (!locationFeedbackGiven && txtLocation.Text.Length > 0)
            {
                UpdateFeedback(0);
                locationFeedbackGiven = true; 
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            if (!descriptionFeedbackGiven && txtLocation.Text.Length > 0)
            {
                UpdateFeedback(2);
                descriptionFeedbackGiven = true;
            }
        }

        private void UpdateFeedback(int index)
        {
            string[] responses = {"Awesome, now we have a location.", "Thank you, this category will help us direct your issue to the correct department.", "The description of your issue helps find a solution.", "Thank you for making our jobs easier."};

            string output = responses[index];

            if (prgbrReport.Value != 100)
            {
                if (prgbrReport.Value != 75)
                {
                    output += "Almost done!";
                }
                
                prgbrReport.Value += 25;
            }

            lblFeedback.Text = output;
            
        }

        private void GetCategory(object sender, EventArgs e)
        {
            if (cmbxCategory.SelectedIndex == 6)
            {
                otherCategory = Interaction.InputBox(
                "Please specify the category of your service:",
                "Other Cateogry",
                "");

                Label lblOther = new Label();

                lblOther.Top = 60;
                lblOther.Left = 370;
                lblOther.Text = "Your category is " + otherCategory;
                lblOther.AutoSize = true;

                pnlBorder.Controls.Add(lblOther);

            }
            else
            {
                otherCategory = cmbxCategory.SelectedItem?.ToString();
            }

            UpdateFeedback(1);
        }
    }
}
