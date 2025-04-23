using CluelessControl.Constants;

namespace CluelessControl
{
    internal static class Program
    {
        public static readonly DirectorForm DirectorForm = new DirectorForm();
        public static readonly HostScreenForm HostScreenForm = new HostScreenForm();
        public static readonly ContestantScreenForm ContestantScreenForm = new ContestantScreenForm();
        public static readonly TVScreenForm TVScreenForm = new TVScreenForm();
        public static readonly AddEnvelopeForm AddEnvelopeForm = new AddEnvelopeForm();

        [STAThread]
        static void Main()
        {
            try
            {

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();

                Application.ThreadException += (sender, args) =>
                {
                    ShowException(args.Exception);
                    Application.Exit();
                };
                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    Exception ex = args.ExceptionObject as Exception ?? new Exception("Unknown unhandled exception");
                    ShowException(ex);
                    Application.Exit();
                };

                Application.Run(DirectorForm);
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private static void ShowException(Exception ex)
        {
            MessageBox.Show(
                $"Nastąpił poważny błąd:{Environment.NewLine}{ex.InnerException?.Message ?? ex.Message}{Environment.NewLine}Program zostanie zamknięty.",
                GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}