using TimeHelper.Data.DbAccess.Services;

namespace TimeHelper.App
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var results = DateService.GetDates().OrderByDescending(d => d.DateValue).ToList();
            dataGridViewResults.DataSource = results;
        }
    }
}