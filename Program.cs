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
                MessageBox.Show($"UI ����: {e.Exception.Message}");
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                MessageBox.Show($"��UI ����: {((Exception)e.ExceptionObject).Message}");
        }
    }
}