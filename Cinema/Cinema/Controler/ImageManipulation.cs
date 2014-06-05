namespace Cinema.Controler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public static class ImageManipulation
    {
        public static bool UploadFile(string location)
        {
            WebClient cl = new WebClient();

            try
            {
                cl.UploadFile("http://tu-cinema.net78.net/client.php", location);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
