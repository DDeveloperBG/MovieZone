namespace MovieZone.Persistence.Models
{
    using System.Collections.Generic;

    using MovieZone.Domain.Entities;
    using MovieZone.Persistence.Common.Models;

    public class ApplicationUser : BaseDeletableModel<string>
    {
        public ApplicationUser(string id)
        {
            this.Id = id;

            this.VideoChatConversations = new HashSet<VideoChatConversation>();
        }

        public string Email { get; set; }

        public string UserName { get; set; }

        public HashSet<VideoChatConversation> VideoChatConversations { get; set; }
    }
}
