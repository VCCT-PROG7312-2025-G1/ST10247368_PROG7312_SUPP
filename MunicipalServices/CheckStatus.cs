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
    public partial class CheckStatus : Form
    {
        private BST binaryTree = new BST();
        private AVLTree avlTree = new AVLTree();
        private MinHeap priorityHeap = new MinHeap();
        private Graph dependencyGraph = new Graph();

        public Dictionary<string, ServiceIssue> requestsById = new Dictionary<string, ServiceIssue>();

        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnMenu;
        private Label lblSearch;
        private Button btnClear;
        private DataGridView dgvDisplay;
        private DateTimePicker dtSearchDate;
        private Button btnShowGraph;
        private Label lblHeading;
        private PictureBox imgLogo;
        private Panel pnlBorder;

        public CheckStatus()
        {
            InitializeComponent();

            this.Text = "Service Updates";
            this.Size = new Size(850, 700);
            loadData();
            createForm();
        }

        public void createForm()
        {
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnMenu = new Button();
            btnClear = new Button();
            lblSearch = new Label();
            dgvDisplay = new DataGridView();
            dtSearchDate = new DateTimePicker();
            btnShowGraph = new Button();
            lblHeading = new Label();
            imgLogo = new PictureBox();
            pnlBorder = new Panel();

            /*----------------------------------------------- Header Controls and Setup ----------------------------------------------------------*/

            imgLogo.Image = Properties.Resources.CPT_Logo;
            imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            imgLogo.Size = new Size(this.ClientSize.Width, 150);
            imgLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            lblHeading.Text = "Request Updates";
            lblHeading.Top = 170;
            lblHeading.Width = 200;
            lblHeading.Height = 40;
            lblHeading.Left = (this.ClientSize.Width - lblHeading.Width) / 2;
            lblHeading.Font = new Font("Calibri", 18, FontStyle.Bold);

            pnlBorder.BorderStyle = BorderStyle.FixedSingle;
            pnlBorder.Top = 210;
            pnlBorder.Left = 10;
            pnlBorder.Width = 780;
            pnlBorder.Height = 680;

            /*-------------------------------------------- Search Controls and Setup ------------------------------------------------------*/

            lblSearch.Text = "Search by ID";
            lblSearch.Top = 20;
            lblSearch.Left = 20;

            txtSearch.Top = 20;
            txtSearch.Left = 150;
            txtSearch.Width = 150;

            dtSearchDate.Top = 20;
            dtSearchDate.Left = 330;
            dtSearchDate.Width = 130;
            dtSearchDate.ShowCheckBox = true;

            btnSearch.Text = "Search";
            btnSearch.Top = 20;
            btnSearch.Left = 500;
            btnSearch.Width = 150;
            btnSearch.Click += search;

            /*----------------------------------------- Clear Filters Controls and Setup ----------------------------------------------------*/

            btnClear.Text = "Clear Filters";
            btnClear.Top = 60;
            btnClear.Left = 20;
            btnClear.Width = 150;
            btnClear.Click += clearFilters;

            /*----------------------------------------- Show Graph Controls and Setup ----------------------------------------------------*/

            btnShowGraph.Text = "Display Dependencies";
            btnShowGraph.Top = 60;
            btnShowGraph.Left = 200;
            btnShowGraph.Width = 150;
            btnShowGraph.Click += ShowGraphBFS;

            /*----------------------------------------- Back to Menu Controls and Setup ----------------------------------------------------*/

            btnMenu.Text = "Back to Menu";
            btnMenu.Top = 60;
            btnMenu.Left = 400;
            btnMenu.Width = 150;
            btnMenu.Click += BackToMenu;

            /*----------------------------------------- DataGrideView Controls and Setup ----------------------------------------------------*/

            dgvDisplay.Top = 100;
            dgvDisplay.Left = 20;
            dgvDisplay.Width = 800;
            dgvDisplay.Height = 300;
            dgvDisplay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDisplay.ReadOnly = true;
            dgvDisplay.AllowUserToAddRows = false;
            dgvDisplay.ScrollBars = ScrollBars.Both;
            dgvDisplay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDisplay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            /*------------------------------------------------ Controls Added to Form ---------------------------------------------------------*/

            this.Controls.Add(imgLogo);
            this.Controls.Add(lblHeading);
            this.Controls.Add(pnlBorder);
            pnlBorder.Controls.Add(lblSearch);
            pnlBorder.Controls.Add(txtSearch);
            pnlBorder.Controls.Add(btnSearch);
            pnlBorder.Controls.Add(dtSearchDate);
            pnlBorder.Controls.Add(btnMenu);
            pnlBorder.Controls.Add(btnClear);
            pnlBorder.Controls.Add(btnShowGraph);
            pnlBorder.Controls.Add(dgvDisplay);

            displayAllResults();
        }

        private void BackToMenu(object sender, EventArgs e)
        {
            MenuScreen menuForm = new MenuScreen();

            this.Hide();
            menuForm.Show();
        }

        private void clearFilters(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dtSearchDate.Value = DateTime.Today;
            dtSearchDate.Checked = false;

            displayResults(requestsById.Values.ToList());
        }

        private void search(object sender, EventArgs e)
        {
            string issueID = txtSearch.Text.Trim();

            DateTime searchDate = dtSearchDate.Value.Date;

            List<ServiceIssue> results = new List<ServiceIssue>();

            if (issueID.Length>0)
            {
                IssueNode bstNode = binaryTree.searchNode(int.Parse(issueID));
                ServiceIssue foundBst = bstNode.Value;
                AVLNode avlNode = avlTree.searchNode(int.Parse(issueID));
                ServiceIssue foundAvl = avlNode.Value;

                if(foundAvl!=foundBst)
                {
                    MessageBox.Show(" Sorry, there was an issue in finding your request ID. Please try again");
                }
                else
                {
                    ServiceIssue found = foundBst;

                    if (found != null)
                    {
                        results.Add(found);
                    }
                    else
                    {
                        MessageBox.Show(" Sorry, there was an issue in finding your request ID. Please try again");
                    }
                }
                
            }
            else
            {
                
                foreach(var elem in requestsById)
                {
                    if(searchDate == elem.Value.SubmissionDate)
                    {
                        results.Add(elem.Value);
                    }
                }
                                
                if (results.Count == 0)
                {
                    MessageBox.Show(" Sorry, there was an issue in finding your request ID. Please try again");

                    results = requestsById.Values.ToList();
                }
            }

            displayResults(results);
        }

        public void displayResults(List<ServiceIssue> results)
        {
            dgvDisplay.DataSource = null;

            dgvDisplay.DataSource = results.ToList();
        }

        public void displayAllResults()
        {
            dgvDisplay.DataSource = null;

            dgvDisplay.DataSource = requestsById.Values.ToList();
        }

        private void ShowGraphBFS(object sender, EventArgs e)
        {
            string startId = "1156";

            List<string> bfs = dependencyGraph.BFS(startId);

            MessageBox.Show("BFS order: " + string.Join(", ", bfs), "Graph BFS");
        }

        private void loadData()
        {

            ServiceIssue IssueThree = new ServiceIssue()
            {
                RequestID = 1156,
                Location = "Ward 30",
                Category = "Water and Electricity",
                Description = "Streetlight not working.",
                Status = "In Progress",
                SubmissionDate = DateTime.Today.AddDays(-3),
                Priority = 2
            };

            ServiceIssue IssueTwo = new ServiceIssue()
            {
                RequestID = 1157,
                Location = "Ward 28",
                Category = "Roads",
                Description = "Large pothole on Main St.",
                Status = "Submitted",
                SubmissionDate = DateTime.Today.AddDays(-1),
                Priority = 3
            };

            ServiceIssue IssueOne = new ServiceIssue()
            {
                RequestID = 1158,
                Location = "Ward 32",
                Category = "Water and Electricity",
                Description = "Leak causing flooding.",
                Status = "Urgent",
                SubmissionDate = DateTime.Today,
                Priority = 1
            };

            AddToData(IssueOne);
            AddToData(IssueTwo);
            AddToData(IssueThree);

            foreach (var elem in SharedDataModel.serviceIssues)
            {
                AddToData(elem);
            }

            dependencyGraph.createEdge("1158", "1156", weight: 5);
            dependencyGraph.createEdge("1157", "1156", weight: 2);
        }

        public void AddToData(ServiceIssue issue)
        {
            if (issue == null || issue.RequestID==0)
            {
                return;
            }

            requestsById[issue.RequestID.ToString()] = issue;

            binaryTree.InsertNode(issue.RequestID, issue);

            avlTree.InsertNode(issue.RequestID, issue);

            priorityHeap.addToHeap(issue, issue.Priority);
            
            dependencyGraph.insert(issue.RequestID.ToString());
        }
    }
}
