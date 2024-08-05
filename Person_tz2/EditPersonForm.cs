using System;
using System.Linq;
using System.Windows.Forms;

namespace Person_tz2
{
    public partial class EditPersonForm : Form
    {
        private System.Windows.Forms.Timer timer;
        private TimeSpan timeElapsed;
        public Person Person { get; private set; }
        private ToolTip toolTip;
        public EditPersonForm()
        {
            InitializeComponent();
            Person = new Person { BirthDate = new DateTime(2000,1,1) };
            InitializeTimer();
            toolTip = new ToolTip();

            // Настройка подсказок для каждого элемента
            toolTip.SetToolTip(txtPersonalId, "Введите персональный идентификатор (до 20 цифр).");
            toolTip.SetToolTip(txtLastName, "Введите фамилию (до 50 символов кириллицы).");
            toolTip.SetToolTip(txtFirstName, "Введите имя (до 50 символов кириллицы).");
            toolTip.SetToolTip(txtMiddleName, "Введите отчество (до 50 символов кириллицы).");
            toolTip.SetToolTip(txtEmail, "Введите электронный адрес в формате [пользователь]@[доменная зона].");
            toolTip.SetToolTip(txtPhoneNumber, "Введите номер телефона в формате +[код страны][код оператора][номер абонента].");
            toolTip.InitialDelay = 0;
            toolTip.ReshowDelay = 0;
        }
        public EditPersonForm(Person person)
        {
            InitializeComponent();
            Person = person;
            BindControls();
            LoadData();
            InitializeTimer();
            txtPersonalId.ReadOnly = true;
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
        private void EditPersonForm_Load(object sender, EventArgs e)
        {
        }
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 секунда
            timer.Tick += timer1_Tick;
            timer.Start();

            timeElapsed = TimeSpan.Zero;
            UpdateTimerLabel();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1)); // Добавляю 1 секунду
            UpdateTimerLabel();
        }
        private void UpdateTimerLabel()
        {
            lblTimer.Text = timeElapsed.ToString(@"hh\:mm\:ss"); // Форматирую время
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Проверка валидации всех полей формы
            if (ValidateChildren() && ValidateInputs())
            {
                if (this.Person != null)
                {
                    UpdatePerson();
                }
                else
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
                MessageBox.Show("Изменение завершено.", "Обновление данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Сообщение о некорректности заполнения формы
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInputs()
        {
            // Проверка ид
            if (string.IsNullOrWhiteSpace(txtPersonalId.Text) ||
                txtPersonalId.Text.Length > 20 ||
                !txtPersonalId.Text.All(char.IsDigit))
            {
                MessageBox.Show("Персональный идентификатор должен содержать до 20 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка фамилии
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                txtLastName.Text.Length > 50 ||
                !txtLastName.Text.All(c => char.IsLetter(c) && IsCyrillic(c)))
            {
                MessageBox.Show("Фамилия должна содержать до 50 символов кириллицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка имени
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                txtFirstName.Text.Length > 50 ||
                !txtFirstName.Text.All(c => char.IsLetter(c) && IsCyrillic(c)))
            {
                MessageBox.Show("Имя должно содержать до 50 символов кириллицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка отчества
            if (string.IsNullOrWhiteSpace(txtMiddleName.Text) ||
                txtMiddleName.Text.Length > 50 ||
                !txtMiddleName.Text.All(c => char.IsLetter(c) && IsCyrillic(c)))
            {
                MessageBox.Show("Отчество должно содержать до 50 символов кириллицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Проверка электронной почты
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Электронный адрес должен быть корректного формата.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Проверка номера телефона
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                !IsValidPhoneNumber(txtPhoneNumber.Text))
            {
                MessageBox.Show("Номер телефона должен быть корректного формата.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        // Метод для проверки, что символ является кириллицей
        private bool IsCyrillic(char c)
        {
            return c >= 'А' && c <= 'я' || c == 'ё' || c == 'Ё';
        }

        // Метод для проверки электронной почты
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        // Метод для проверки номера телефона
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Проверка формата телефона
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+\d{1,3}\d{3}\d{7}$");
        }
        private void UpdatePerson()
        {
            // Обновляю данные существующего объекта Person
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
    }
}
