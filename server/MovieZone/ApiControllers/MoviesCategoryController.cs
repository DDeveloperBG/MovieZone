﻿namespace MovieZone.ApiControllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class MoviesCategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMovieDetails(string categoryId, int? page = 1)
        {
            return this.Ok(new
            {
                categories = new[]
                {
                    new { id = "as1", name = "Recommended Movies" },
                    new { id = "as2", name = "Popular" },
                    new { id = "as3", name = "Something else" },
                },
                movies = new[]
                {
                    new
                    {
                        id = "aaasd2",
                        name = "Qba daba",
                        description = "Lorem impusm,",
                        imgUrl = "https://occ-0-6536-2774.1.nflxso.net/dnm/api/v6/X194eJsgWBDE2aQbaNdmCXGUP-Y/AAAABZQok2nqlc0GXzT3dtgJhAB_NusOu0PBYvnwD08DEHvpDUkrdnLCHgiljv33-fiolpwvzhARi_47vwHVHf76tGjYyRE.jpg?r=c82",
                    },
                },
            });
        }
    }
}
