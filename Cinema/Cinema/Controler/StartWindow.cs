namespace Cinema.Controler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public static class StartWindow
    {
        public static MainWindow GetMainWindow(DependencyObject dependancyObject)
        {
            MainWindow mainWindow = null;
            Window parentWindow = Window.GetWindow(dependancyObject);

            if (parentWindow != null)
            {
                mainWindow = parentWindow as MainWindow;

                if (mainWindow != null)
                {
                    return mainWindow;
                }
            }

            return null;
        }
    }
}
