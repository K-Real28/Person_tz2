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
        private System.Windows.Forms.Timer timer;
        private TimeSpan timeElapsed;
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

        
        
        private bool ValidateInputs()
        {
            // Добавьте здесь проверку полей
            return !string.IsNullOrEmpty(txtPersonalId.Text) &&
                   !string.IsNullOrEmpty(txtLastName.Text) &&
                   !string.IsNullOrEmpty(txtFirstName.Text) &&
                   !string.IsNullOrEmpty(txtMiddleName.Text) &&
                   dtpBirthDate.Value != DateTime.MinValue &&
                   !string.IsNullOrEmpty(txtEmail.Text) &&
                   !string.IsNullOrEmpty(txtPhoneNumber.Text);
        }
        private void UpdatePerson()
        {
            // Обновляем данные существующего объекта Person
            this.Person.PersonalId = txtPersonalId.Text;
            this.Person.LastName = txtLastName.Text;
            this.Person.FirstName = txtFirstName.Text;
            this.Person.MiddleName = txtMiddleName.Text;
            this.Person.BirthDate = dtpBirthDate.Value;
            this.Person.Email = txtEmail.Text;
            this.Person.PhoneNumber = txtPhoneNumber.Text;
        }

        private void CreateNewPerson()
        {
            // Создаем новый объект Person с данными из формы
            this.Person = new Person
            {
                PersonalId = txtPersonalId.Text,
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                MiddleName = txtMiddleName.Text,
                BirthDate = dtpBirthDate.Value,
                Email = txtEmail.Text,
                PhoneNumber = txtPhoneNumber.Text
            };
        }
        private void EditPersonForm_Load(object sender, EventArgs e)
        {   
        }
        
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += timer1_Tick;
            timer.Start();

            timeElapsed = TimeSpan.Zero;
            UpdateTimerLabel();
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

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Проверка валидации всех полей формы
            if (ValidateChildren() && ValidateInputs())
            {
                // Если редактируем существующую запись
                if (this.Person != null)
                {
                    UpdatePerson();
                }
                else // Если создаем новую запись
                {
                    CreateNewPerson();
                }

                // Сохранение данных
                Person.PersonalId = txtPersonalId.Text;
                Person.LastName = txtLastName.Text;
                Person.FirstName = txtFirstName.Text;
                Person.MiddleName = txtMiddleName.Text;
                Person.BirthDate = dtpBirthDate.Value;
                Person.Email = txtEmail.Text;
                Person.PhoneNumber = txtPhoneNumber.Text;

                // Закрытие формы с результатом OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Сообщение о некорректности заполнения формы
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
