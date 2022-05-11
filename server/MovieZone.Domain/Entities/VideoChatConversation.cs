namespace MovieZone.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using MovieZone.Persistence.Common.Models;
    using MovieZone.Persistence.Models;

    public class VideoChatConversation : BaseDeletableModel<string>
    {
        public VideoChatConversation()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Participants = new HashSet<ApplicationUser>();
        }

        public HashSet<ApplicationUser> Participants { get; set; }
    }
}
