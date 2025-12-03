namespace TS.Personal.Web.ViewModels
{
    public class UserTableVm
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName {
            get { return $"{FirstName} {LastName}"; }  
        }

        public string Email { get; set; }

        public DateTime Registered { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
