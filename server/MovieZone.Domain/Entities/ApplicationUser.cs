namespace MovieZone.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;
    using MovieZone.Data.Common.Models;
    using MovieZone.Domain.Entities;

    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
        }

        [ForeignKey(nameof(FirebaseUser))]
        public string FirebaseUserId { get; set; }

        public FirebaseUser FirebaseUser { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
