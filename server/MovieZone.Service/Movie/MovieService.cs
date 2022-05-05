namespace MovieZone.Service.Movie
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MovieZone.Common;
    using MovieZone.Domain.Entities;
    using MovieZone.Persistence.Common.Repositories;
    using MovieZone.Service.Actor;
    using MovieZone.Service.AWS.Storage.MovieStorage;
    using MovieZone.Service.DTOs.Movie;
    using MovieZone.Service.DTOs.Pagination;
    using MovieZone.Service.MoviesCategory;
    using MovieZone.Service.Pagination;
    using MovieZone.Service.Time;
    using MovieZone.Services.Mapping;

    public class MovieService : IMovieService
    {
        private const string DefaultMoviesCategoryName = GlobalConstants.CategoryMovies.DefaultMoviesCategoryName;
        private const int PageSize = GlobalConstants.CategoryMovies.PageSize;

        private readonly ITimeService timeService;
        private readonly IMoviesCategoryService moviesCategoryService;
        private readonly IDeletableEntityRepository<Movie> movieRepository;
        private readonly IPaginationService paginationService;
        private readonly IMovieStorageService movieStorageService;
        private readonly IActorService actorService;

        public MovieService(
            IDeletableEntityRepository<Movie> movieRepository,
            IPaginationService paginationService,
            ITimeService timeService,
            IMoviesCategoryService moviesCategoryService,
            IMovieStorageService movieStorageService,
            IActorService actorService)
        {
            this.movieRepository = movieRepository;
            this.paginationService = paginationService;
            this.timeService = timeService;
            this.moviesCategoryService = moviesCategoryService;
            this.movieStorageService = movieStorageService;
            this.actorService = actorService;
        }

        public PaginationDTO<GetMoviesInCategoryMovieDTO> GetMoviesInCategory(
            string categoryId,
            int currentPage)
        {
            var query = this.movieRepository
                .AllAsNoTracking();

            if (categoryId != "default")
            {
                query = query.Where(x => x.MoviesCategories
                    .Any(x => x.Id == categoryId));
            }
            else
            {
                query = query.Where(x => x.MoviesCategories
                    .Any(x => x.Name == DefaultMoviesCategoryName));
            }

            var moviesAsIQuerable = query.To<GetMoviesInCategoryMovieDTO>();

            return this.paginationService.GetPaged(moviesAsIQuerable, currentPage, PageSize);
        }

        public GetMovieDetailsDTO GetMovieDetails(string id)
        {
            return this.movieRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<GetMovieDetailsDTO>()
                .SingleOrDefault();
        }

        public async Task AddMovieAsync(AddMovieInputDTO input)
        {
            var fileExtension = Path.GetExtension(input.MovieFile.FileName)[1..].ToLower();
            this.ValidateAddMovieInputDTO(input, fileExtension);

            var movie = AutoMapperConfig
                .MapperInstance.Map<AddMovieInputDTO, Movie>(input);

            using (var movieStream = input.MovieFile.OpenReadStream())
            {
                await this.movieStorageService.UploadFileAsync(
                    movieStream,
                    movie.Id,
                    fileExtension);
            }

            foreach (var actorName in input.ActorsNames)
            {
                var actor = this.actorService.GetByName(actorName);

                if (actor == null)
                {
                    actor = new Actor
                    {
                        Name = actorName,
                    };
                }

                movie.Actors.Add(actor);
            }

            foreach (var categoryName in input.MoviesCategoriesNames)
            {
                var category = this.moviesCategoryService.GetByName(categoryName);

                if (category == null)
                {
                    category = new MoviesCategory
                    {
                        Name = categoryName,
                    };
                }

                movie.MoviesCategories.Add(category);
            }

            await this.movieRepository.AddAsync(movie);
            await this.movieRepository.SaveChangesAsync();
        }

        private void ValidateAddMovieInputDTO(AddMovieInputDTO input, string fileExtension)
        {
            int maxYear = this.timeService.GetUtcNow().Year
                + ValidationConstants.Movie.YearOfPublishingAfterCurrentYearRange;

            if (input.YearOfPublishing < ValidationConstants.Movie.YearOfPublishingMinValue
                || input.YearOfPublishing > maxYear)
            {
                throw new ArgumentException(
                    ValidationConstants.Movie.GetYearOfPublishingExceptionMessage(maxYear));
            }

            if (!ValidationConstants.Movie.MovieFileValidExtensions.Contains(fileExtension))
            {
                throw new ArgumentException(
                    ValidationConstants.Movie.MovieFileExtensionExceptionMessage);
            }
        }
    }
}
