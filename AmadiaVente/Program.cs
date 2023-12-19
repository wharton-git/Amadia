namespace AmadiaVente
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Créez et affichez le Splash Screen
            splashScreen.splashScreen splashScreen = new splashScreen.splashScreen();
            splashScreen.Show();

            // Simulation d'une opération de chargement (remplacez cela par votre propre logique de chargement)
            System.Threading.Thread.Sleep(4000);

            // Fermez le Splash Screen
            splashScreen.Close();

            Application.Run(new Form1());
        }
    }
}