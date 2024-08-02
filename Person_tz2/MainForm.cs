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
    public partial class MainForm : Form
    {
        private AppDbContext _context;
        public MainForm()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadData();
        }
        public void MainForm_Load(object sender, EventArgs e)
        {
         
        }
        private void LoadData()
        {
            var people = _context.People.Select(p => new
            {
                p.PersonalId,
                p.LastName,
                p.FirstName,
                p.MiddleName
            }).ToList();

            dataGridView.DataSource = people;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new EditPersonForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _context.People.Add(form.Person);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var personalId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                var person = _context.People.Find(personalId);
                if (person != null)
                {
                    var form = new EditPersonForm(person);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _context.Entry(person).CurrentValues.SetValues(form.Person);
                        _context.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var personalId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                var person = _context.People.Find(personalId);
                if (person != null)
                {
                    _context.People.Remove(person);
                    _context.SaveChanges();
                    LoadData();
                }
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var personalId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                var person = _context.People.Find(personalId);
                if (person != null)
                {
                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "XML files (*.xml)|*.xml",
                        Title = "Save Person Information"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Serialize and save to XML
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Person));
                        using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                        {
                            serializer.Serialize(writer, person);
                        }
                    }
                }
            }
        }
    }
}
