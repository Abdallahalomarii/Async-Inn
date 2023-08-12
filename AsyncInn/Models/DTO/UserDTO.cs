namespace AsyncInn.Models.DTO
{
    public class UserDTO
    {
        public string ID { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public IList<string> Roles { get; set; }
    }
}
