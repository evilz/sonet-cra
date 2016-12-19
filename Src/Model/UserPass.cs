namespace sonet.cra.Model
{
    public class UserPass
    {
        public UserPass(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
