namespace MovieZone.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using MovieZone.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser(string id)
        {
            this.Id = id;
            this.Roles = new HashSet<IdentityUserRole<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
