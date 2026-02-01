namespace Demo.PL.ViewModels.IdentityViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = new List<string>();

    }
}
