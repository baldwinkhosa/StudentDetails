using AutoMapper;
using StudentDetails.ClientApp.Entities;
using StudentDetails.Data.Infrastructure;
using StudentDetails.Domain.Model;
using StudentDetails.ServicePlatform.ExternalContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDetails.ClientApp
{
    public partial class Form1: Form
    {
        private readonly IStudentService _StudentService;

        public Form1(IStudentService StudentService)
        {
            _StudentService = StudentService;
            InitializeComponent();
            Mapper.CreateMap<StudentModel, Student>();

            dtpDOB.Format = DateTimePickerFormat.Custom;
            dtpDOB.CustomFormat = "dd/MM/yyyy";

            refreshdata();
        }

        private void refreshdata()
        {  
            try
            {
                dgStudDetails.DataSource = _StudentService.SelectAllStudents().ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        private void dgStudDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isvalid = true;

                var stud = new StudentModel()
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    CellNumber = txtCellNumber.Text,
                    EmailAddress = txtEmailAddress.Text,
                    DOB = dtpDOB.Value, 
                    Gender = cmbGender.Text
                };

                if (!IsValidEmail(stud.EmailAddress))
                {
                    MessageBox.Show($"Invalid Email address {stud.EmailAddress}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isvalid = false;
                }

                if(!IsPhoneNumber(stud.CellNumber))
                {
                    MessageBox.Show($"Invalid cellphone number {stud.CellNumber}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isvalid = false;
                }

                if(isvalid)
                {
                    var model = Mapper.Map<Student>(stud);

                    var result = _StudentService.CreateStudent(model);

                    if (result > 0)
                    {
                        MessageBox.Show("Saved Successfully!..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong!. Please try again!..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshdata();
                    ClearFields();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error occured!, {ex.Message}. Please try again later!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void dgStudDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgStudDetails.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgStudDetails.SelectedRows)
                {
                    lblId.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtLastName.Text = row.Cells[2].Value.ToString();
                    txtCellNumber.Text = row.Cells[3].Value.ToString();
                    txtEmailAddress.Text = row.Cells[4].Value.ToString();
                    dtpDOB.Value = Convert.ToDateTime(row.Cells[5].Value.ToString());
                    cmbGender.SelectedItem = row.Cells[6].Value.ToString();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var studId =Int32.Parse(lblId.Text);
                _StudentService.DeleteStudent(studId);

                MessageBox.Show("Deleted Successfully!..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshdata();
                ClearFields();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured!, {ex.Message}. Please try again later!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private  void ClearFields()  
        {
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtCellNumber.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            dtpDOB.Value = DateTime.Now;
            cmbGender.SelectedIndex = -1;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool isvalid = true;

                var stud = new StudentModel()
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    CellNumber = txtCellNumber.Text,
                    EmailAddress = txtEmailAddress.Text,
                    DOB = dtpDOB.Value,
                    Gender = cmbGender.Text
                };

                if (!IsValidEmail(stud.EmailAddress))
                {
                    MessageBox.Show($"Invalid Email address {stud.EmailAddress}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isvalid = false;
                }

                if (!IsPhoneNumber(stud.CellNumber))
                {
                    MessageBox.Show($"Invalid cellphone number {stud.CellNumber}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isvalid = false;
                }

                if(isvalid)
                {
                    var model = Mapper.Map<Student>(stud);

                    _StudentService.UpdateStudent(model);

                    MessageBox.Show("Updated Successfully!..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refreshdata();
                    ClearFields();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error occured!, {ex.Message}. Please try again later!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^((?:\+27|27)|0)(=72|82|73|83|74|84)(\d{7})$").Success;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = $"{txtPath.Text}\\Student.XML";
                DataTable dt = new DataTable("Student");
                DataSet dS = new DataSet("Students");

                dgStudDetails.DataSource = _StudentService.SelectAllStudents().Where(x => x.Exported == false).ToList();

                object[] cellValues = new object[dgStudDetails.Columns.Count];

                foreach (DataGridViewColumn column in dgStudDetails.Columns)
                {
                    dt.Columns.Add(column.HeaderText);
                }

                foreach (DataGridViewRow row in dgStudDetails.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        cellValues[i] = row.Cells[i].Value;
                    }
                    dt.Rows.Add(cellValues);
                }
                dS.Tables.Add(dt);
                dS.WriteXml($"{fileName}");

                UpdateExportedStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured!, {ex.Message}. Please try again later!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void UpdateExportedStudents()
        {
            try
            {
                var students = _StudentService.SelectAllStudents().Where(x => x.Exported == false).ToList();
                
                if(students != null)
                {
                    foreach(var student in students)
                    {
                        student.Exported = true;
                        var model = Mapper.Map<Student>(student);

                        _StudentService.UpdateStudent(model);                       
                    }
                    refreshdata();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
