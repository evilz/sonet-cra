using System.Drawing;
using System.Linq;
using CredentialManagement;
using sonet.cra.Model;
using Console = Colorful.Console;

namespace sonet.cra
{
    public class CredentialManager
    {
        public UserPass GetOrCreateCredential()
        {
            var credentialKey = "sonet.soat.fr";
            CredentialSet set = new CredentialSet(credentialKey);

            Credential credential = null;
            set = set.Load();
            if (set.Count == 0)
            {
                var prompt = new VistaPrompt();
                prompt.ShowDialog();
                credential = new Credential(prompt.Username.Split('\\').Last(), prompt.Password, credentialKey, CredentialType.Generic);
                credential.Save();
            }
            else
            {
                credential = set[0];
                Console.Write($"User and password found ! ", Color.Gainsboro);
                Console.WriteLine(credential.Username, Color.DarkKhaki);

            }

            return new UserPass(credential.Username, credential.Password);
        }
    }
}
