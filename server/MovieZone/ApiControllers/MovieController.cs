namespace MovieZone.ApiControllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMovieDetails(string id)
        {
            return this.Ok(
                new
                {
                    id = "aaasd2",
                    name = "Qba daba",
                    description = "Lorem impusm,",
                    imgUrl = "https://occ-0-6536-2774.1.nflxso.net/dnm/api/v6/X194eJsgWBDE2aQbaNdmCXGUP-Y/AAAABZQok2nqlc0GXzT3dtgJhAB_NusOu0PBYvnwD08DEHvpDUkrdnLCHgiljv33-fiolpwvzhARi_47vwHVHf76tGjYyRE.jpg?r=c82",
                });
        }
    }
}
