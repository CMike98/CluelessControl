namespace CluelessControl
{
    public partial class DirectorForm : Form
    {
        #region Other Forms
        private static readonly HostScreenForm _hostScreenForm = new();
        private static readonly ContestantScreenForm _contestantScreenForm = new();
        #endregion

        public DirectorForm()
        {
            InitializeComponent();
        }

        private void DirectorForm_Load(object sender, EventArgs e)
        {
            ShowAllForms();
        }

        private static void ShowAllForms()
        {
            _hostScreenForm.Show();
            _contestantScreenForm.Show();
        }
    }
}
