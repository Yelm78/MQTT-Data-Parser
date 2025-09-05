namespace MQTT_Data_Parser
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MQTTDataParser());
            Application.ThreadException += (s, e) =>
                MessageBox.Show($"UI 예외: {e.Exception.Message}");
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                MessageBox.Show($"비UI 예외: {((Exception)e.ExceptionObject).Message}");
        }
    }
}