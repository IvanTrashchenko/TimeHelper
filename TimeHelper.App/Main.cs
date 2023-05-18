using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TimeHelper.Data.DbAccess.Services;

namespace TimeHelper.App
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Now;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            GetItems();
        }

        private void GetItems()
        {
            try
            {
                var results = DateService.GetDates().OrderBy(d => d.GetTimeUntilNextAnniversary()).ToList();
                dataGridViewResults.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsNameValid() || !IsDateValid()) return;

            try
            {
                DateService.AddOrUpdateDate(txtName.Text, dtpDate.Value);
                GetItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!IsNameValid()) return;

            try
            {
                DateService.DeleteDate(txtName.Text);
                txtName.Text = "";
                GetItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsNameValid()
        {
            txtName.Text = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Date name cannot be empty.");
                return false;
            }

            return true;
        }

        private bool IsDateValid()
        {
            if (dtpDate.Value > DateTime.Now)
            {
                MessageBox.Show("Please select a date from the past.");
                return false;
            }

            return true;
        }

        private void dataGridViewResults_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtName.Text = dataGridViewResults.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtpDate.Value = DateTime.Parse(dataGridViewResults.Rows[e.RowIndex].Cells[2].Value.ToString());
        }
    }
}