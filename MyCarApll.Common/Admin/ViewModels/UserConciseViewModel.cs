using MyCarApp.Common.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace MyCarApp.Common.Admin.ViewModels
{
    public class UserConciseViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public bool IsLockedOut { get; set; }

        public ICollection<string> Roles { get; set; }

        public bool IsPublisher => this.Roles.Any(r => r == AppUtilities.PublisherRole);

        public bool IsAdmin => this.Roles.Any(r => r == AppUtilities.AdministratorRole);

        public bool IsObserver => this.Roles.Any(r => r == AppUtilities.ObserverRole);
    }
}
