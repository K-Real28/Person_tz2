using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Person_tz2
{
    public partial class EditPersonForm : Form
    {
        public Person Person { get; private set; }
        public EditPersonForm()
        {
            InitializeComponent();
            Person = new Person
            {
                BirthDate = DateTime.Today // Установите текущую дату как значение по умолчанию
            };
            BindControls();
            InitializeTimer();
        }
        public EditPersonForm(Person person)
        {
            InitializeComponent();
            Person = person;
            BindControls();
            LoadData();
        }
        private void BindControls()
        {
            txtPersonalId.DataBindings.Add("Text", Person, "PersonalId");
            txtLastName.DataBindings.Add("Text", Person, "LastName");
            txtFirstName.DataBindings.Add("Text", Person, "FirstName");
            txtMiddleName.DataBindings.Add("Text", Person, "MiddleName");
            dtpBirthDate.DataBindings.Add("Value", Person, "BirthDate");
            txtEmail.DataBindings.Add("Text", Person, "Email");
            txtPhoneNumber.DataBindings.Add("Text", Person, "PhoneNumber");
        }

        private void LoadData()
        {
            txtPersonalId.Text = Person.PersonalId;
            txtLastName.Text = Person.LastName;
            txtFirstName.Text = Person.FirstName;
            txtMiddleName.Text = Person.MiddleName;
            dtpBirthDate.Value = Person.BirthDate;
            txtEmail.Text = Person.Email;
            txtPhoneNumber.Text = Person.PhoneNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                // Save data
                Person.PersonalId = txtPersonalId.Text;
                Person.LastName = txtLastName.Text;
                Person.FirstName = txtFirstName.Text;
                Person.MiddleName = txtMiddleName.Text;
                Person.BirthDate = dtpBirthDate.Value;
                Person.Email = txtEmail.Text;
                Person.PhoneNumber = txtPhoneNumber.Text;

                DialogResult = DialogResult.OK;
            }
        }

        private void EditPersonForm_Load(object sender, EventArgs e)
        {   
        }
        private System.Windows.Forms.Timer timer;
        private TimeSpan timeElapsed;
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += timer1_Tick;
            timer.Start();

            timeElapsed = TimeSpan.Zero;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1)); // Добавляем 1 секунду
            UpdateTimerLabel();
            //lblTimer.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void UpdateTimerLabel()
        {
            lblTimer.Text = timeElapsed.ToString(@"hh\:mm\:ss"); // Форматируем время
        }
    }
}
