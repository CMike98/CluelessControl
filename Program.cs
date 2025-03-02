namespace CluelessControl
{
    internal static class Program
    {
        public static readonly DirectorForm DirectorForm = new DirectorForm();
        public static readonly HostScreenForm HostScreenForm = new HostScreenForm();
        public static readonly ContestantScreenForm ContestantScreenForm = new ContestantScreenForm();
        public static readonly TVScreenForm TVScreenForm = new TVScreenForm();
        public static readonly AddEnvelopeForm AddEnvelopeForm = new AddEnvelopeForm();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(DirectorForm);
        }
    }
}