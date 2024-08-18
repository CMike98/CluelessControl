using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace CluelessControl
{
    public partial class DirectorForm : Form
    {
        #region Other Forms
        private static readonly HostScreenForm _hostScreenForm = new();
        private static readonly ContestantScreenForm _contestantScreenForm = new();
        private static readonly TVScreenForm _tvScreenForm = new();
        #endregion

        #region Json Serializer Options
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            Converters = { new JsonChequeConverter(), new JsonQuestionConverter(), new JsonQuestionSetConverter() },
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
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
            _tvScreenForm.Show();
        }
    }
}
