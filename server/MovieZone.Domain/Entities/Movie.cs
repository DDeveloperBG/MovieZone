namespace MovieZone.Domain.Entities
{
    using System;

    using MovieZone.Data.Common.Models;

    public class Movie : BaseDeletableModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }
    }
}
