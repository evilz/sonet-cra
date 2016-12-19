using System.Drawing;
using System.Net;
using sonet.cra.Model;
using SimpleBrowser;
using Console = Colorful.Console;

namespace sonet.cra
{
    public class Authenticator
    {
        public CookieContainer Authenticate(UserPass userpass)
         {
             var browser = new Browser
             {
                 UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Chrome/8.0.552.224 Safari/534.10"
             };

            Console.Write($"Login on ", Color.Gainsboro);
            Console.WriteLine(@"http://sonet.soat.fr/CRA", Color.DarkKhaki);
            browser.Navigate(@"http://sonet.soat.fr/CRA");
            CheckException(browser);

            browser.Find("Email").Value = userpass.Username;
            browser.Find(ElementType.Button, FindBy.Id, "next").Click();

            browser.Find("Passwd").Value = userpass.Password;
            browser.Find(ElementType.Button, FindBy.Id, "signIn").Click();
            CheckException(browser);

            return browser.Cookies;
        }
        
        private void CheckException(Browser browser)
        {
            if (browser.LastWebException != null)
                throw browser.LastWebException;
        }
    }
}
