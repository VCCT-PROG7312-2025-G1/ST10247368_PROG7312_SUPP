using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServices
{
    public partial class LocalEvents : Form
    {
        private Dictionary<string, Event> allEvents = new Dictionary<string, Event>();
        //private Dictionary<int, string> eventTitles = new Dictionary<int, string>;
        private HashSet<string> eventCategories = new HashSet<string>();
        private SortedDictionary<DateTime, Event> eventsByDate = new SortedDictionary<DateTime, Event>();
        private Queue<Event> upcomingEvents = new Queue<Event>();
        private Stack<Event> recentViewed = new Stack<Event>();
        private PriorityQueue<Event, int> urgentEvents = new PriorityQueue<Event, int>();        

        private TextBox txtSearch;
        private ComboBox cmbxCategory;
        private DateTimePicker dtSearchDate;
        private DataGridView dgvEvents;
        private Panel pnlBorder;
        private Button btnSearch;
        private Button btnMenu;
        private Button btnUpcoming;
        private Button btnRecent;
        private Label lblHeading;
        private PictureBox imgLogo;

        public LocalEvents()
        {
            
            InitializeComponent();
            this.Text = "Local Events and Announcements";
            this.Size = new Size(900, 800);

            loadData();
            createForm();
            
        }

        private void createForm()
        {
            txtSearch = new TextBox();
            cmbxCategory = new ComboBox();
            pnlBorder = new Panel();
            btnSearch = new Button();
            btnMenu = new Button();
            dtSearchDate = new DateTimePicker();
            dgvEvents = new DataGridView();
            btnUpcoming = new Button();
            btnRecent = new Button();
            lblHeading = new Label();
            imgLogo = new PictureBox();

            /*----------------------------------------------- Header Controls and Setup ----------------------------------------------------------*/

            imgLogo.Image = Properties.Resources.CPT_Logo;
            imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            imgLogo.Size = new Size(this.ClientSize.Width, 150);
            imgLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            lblHeading.Text = "Local Events";
            lblHeading.Top = 170;
            lblHeading.Left = (this.ClientSize.Width - lblHeading.Width) / 2; ;

            pnlBorder.BorderStyle = BorderStyle.FixedSingle;
            pnlBorder.Top = 210;
            pnlBorder.Left = 10;
            pnlBorder.Width = 780;
            pnlBorder.Height = 700;

            /*-------------------------------------------- Category Search Controls and Setup ------------------------------------------------------*/

            Label lblCategory = new Label();

            lblCategory.Text = "Search by Category";
            lblCategory.Top = 20;
            lblCategory.Left = 20;

            cmbxCategory.Left = 150;
            cmbxCategory.Top = 20;
            cmbxCategory.Width = 200;
            cmbxCategory.DropDownStyle = ComboBoxStyle.DropDownList;

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

            /*--------------------------------------------- Date Search Controls and Setup -------------------------------------------------------*/


            Label lblDate = new Label();

            lblDate.Text = "Search by Date";
            lblDate.Top = 20;
            lblDate.Left = 390;

            dtSearchDate.Left = 520;
            dtSearchDate.Top = 20;
            dtSearchDate.Width = 200;
            dtSearchDate.ShowCheckBox = true;
            dtSearchDate.Checked = false;


            /*-------------------------------------------- Keyword Search Controls and Setup -----------------------------------------------------*/

            Label lblKeyword = new Label();

            lblKeyword.Text = "Search by Keyword";
            lblKeyword.Top = 60;
            lblKeyword.Left = 20;

            txtSearch.Top = 60;
            txtSearch.Left = 150;
            txtSearch.Width = 200;

            /*-------------------------------------------- Search Button Controls and Setup -----------------------------------------------------*/

            btnSearch.Text = "Search";
            btnSearch.Top = 60;
            btnSearch.Left = 280;
            btnSearch.Width = 120;

            btnSearch.Click += searchEvents;

            /*-------------------------------------------- Events Display Controls and Setup -----------------------------------------------------*/

            dgvEvents.Left = 20;
            dgvEvents.Top = 100;
            dgvEvents.Width = 800;
            dgvEvents.Height = 300;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.ReadOnly = true;
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.ScrollBars = ScrollBars.Both;

            List<Event> results = allEvents.Values.ToList(); ;

            displayResults(results);

            /*-------------------------------------------- Menu Button Controls and Setup ------------------------------------------------------*/

            btnMenu.Text = "Back to Menu";
            btnMenu.Top = 440;
            btnMenu.Left = 20;
            btnMenu.Width = 150;
            btnMenu.Click += BackToMenu;

            /*-------------------------------------------- Upcoming Button Controls and Setup ------------------------------------------------------*/

            btnUpcoming.Text = "Upcoming Events";
            btnUpcoming.Top = 440;
            btnUpcoming.Left = 210;
            btnUpcoming.Width = 150;
            btnUpcoming.Click += showUpcoming;

            /*-------------------------------------------- Recent Button Controls and Setup ------------------------------------------------------*/

            btnRecent.Text = "Recently Viewed Events";
            btnRecent.Top = 440;
            btnRecent.Left = 400;
            btnRecent.Width = 150;
            btnRecent.Click += showRecent;

            /*------------------------------------------------- Controls Added to Form ----------------------------------------------------------*/

            this.Controls.Add(imgLogo);
            this.Controls.Add(lblHeading);
            this.Controls.Add(pnlBorder);
            pnlBorder.Controls.Add(lblCategory);
            pnlBorder.Controls.Add(lblDate);
            pnlBorder.Controls.Add(lblKeyword);
            pnlBorder.Controls.Add(dtSearchDate);
            pnlBorder.Controls.Add(btnSearch);
            pnlBorder.Controls.Add(txtSearch);
            pnlBorder.Controls.Add(cmbxCategory);
            pnlBorder.Controls.Add(dgvEvents);
            pnlBorder.Controls.Add(btnMenu);
            pnlBorder.Controls.Add(btnUpcoming);
            pnlBorder.Controls.Add(btnRecent);


            displayResults(allEvents.Values.ToList());
        }

        private void loadData()
        {
            Event eventOne = new Event()
            {
                Title = "Community Budget Meeting",
                Date = new DateTime(2025, 10, 03),
                Category = "Finance",
                Description = "Meeting to discuss budget of 2026",
                Priority = 5
            };

            Event eventTwo = new Event()
            {
                Title = "Community Cleanup",
                Date = new DateTime(2025, 06, 16),
                Category = "Sanitation",
                Description = "Community cleaning up streets",
                Priority = 5
            };

            Event eventThree = new Event()
            {
                Title = "Assistance with Grants",
                Date = new DateTime(2025, 04, 20),
                Category = "Finance",
                Description = "Meeting to assistance members with grant applications",
                Priority = 7
            };

            Event eventFour = new Event()
            {
                Title = "Repair Street Lights",
                Date = new DateTime(2025, 12, 15),
                Category = "Electricity",
                Description = "Repair stree light outside school",
                Priority = 7
            };

            allEvents.Add("Community Budget Meeting", eventOne);
            eventsByDate.Add(new DateTime(2025, 10, 03), eventOne);
            eventCategories.Add("Finance");

            allEvents.Add("Community Cleanup", eventTwo);
            eventsByDate.Add(new DateTime(2025, 06, 16), eventTwo);
            eventCategories.Add("Sanitation");

            allEvents.Add("Assistance with Grants", eventThree);
            eventsByDate.Add(new DateTime(2025, 04, 20), eventThree);

            allEvents.Add("Repair Street Lights", eventFour);
            eventsByDate.Add(new DateTime(2025, 12, 15), eventFour);
            eventCategories.Add("Electricity");
        }

        private void searchEvents(object sender, EventArgs e)
        {
            List<Event> filteredEvents = new List<Event>();

            bool keywordFound = false;

            if (txtSearch.Text.Length > 0)
            {
                MessageBox.Show(txtSearch.Text);

                keywordFound = true;

                string keyword = txtSearch.Text.Trim().ToLower();

                foreach (var eventElement in allEvents)
                {
                    string eventTitle = eventElement.Key.ToLower();

                    if (eventTitle.ToLower().Contains(keyword))
                    {
                        filteredEvents.Add(eventElement.Value);
                    }
                }
            }

            bool categoryFound = false;

            string searchCategory = cmbxCategory.SelectedItem?.ToString();


            if (cmbxCategory.SelectedIndex != -1)
            {
                if (eventCategories.Contains(searchCategory))
                {
                    categoryFound = true;
                }

            }

            List<Event> categoryFinds = new List<Event>();

            if(categoryFound==true)
            {
                foreach(var eventElem in allEvents)
                {
                    if (eventElem.Value.Category == searchCategory)
                    {
                        categoryFinds.Add(eventElem.Value);
                    }
                }

                if (keywordFound == true)
                {
                    List<Event> searchIntersections = new List<Event>();

                    foreach (var elem in filteredEvents)
                    {
                        if (categoryFinds.Contains(elem))
                        {
                            searchIntersections.Add(elem);
                        }
                    }

                    filteredEvents = searchIntersections;
                }
                else
                {
                    filteredEvents = categoryFinds;
                }
            }
                        

            bool dateUsed = dtSearchDate.Checked;

            if(dateUsed == true)
            {
                DateTime searhDate = dtSearchDate.Value.Date;

                bool foundDate = false;

                if (eventsByDate.ContainsKey(searhDate))
                {
                    foundDate = true;
                }

                Event eventDate = null;

                try
                {
                    eventDate = eventsByDate[searhDate];
                }
                catch (Exception eError)
                {
                    MessageBox.Show("There are not events during the date you've specified");
                }

                if (keywordFound == true || categoryFound == true)
                {
                    if (filteredEvents.Contains(eventDate))
                    {
                        filteredEvents = new List<Event>() { eventDate };
                    }
                    else
                    {
                        filteredEvents.Clear();
                        MessageBox.Show("There are not events during the date you've specified");
                    }

                }
                else
                {
                    filteredEvents = new List<Event>() { eventDate };
                }
            }    


            

            displayResults(filteredEvents);
            updateRecents(filteredEvents);

        }

        public void displayResults(List<Event> results)
        {
            dgvEvents.DataSource = null;

            dgvEvents.DataSource = results.ToList();
        }

        public void updateRecents(List<Event> results)
        {
            foreach(var eventElem in results)
            {
                recentViewed.Push(eventElem);
            }
        }

        public void findUpcoming()
        {
            DateTime currentDate = DateTime.Today;
            
            foreach(var elem in eventsByDate)
            {
                if(elem.Key >= currentDate && elem.Key <= currentDate.AddDays(7))
                {
                    upcomingEvents.Enqueue(elem.Value);
                }
            }
        }

        private void BackToMenu(object sender, EventArgs e)
        {
            MenuScreen menuForm = new MenuScreen();

            this.Hide();
            menuForm.Show();
        }

        private void showUpcoming(object sender, EventArgs e)
        {
            findUpcoming();
            displayResults(upcomingEvents.ToList());
        }

        private void showRecent(object sender, EventArgs e)
        {
            displayResults(recentViewed.ToList());
        }
    }
}
