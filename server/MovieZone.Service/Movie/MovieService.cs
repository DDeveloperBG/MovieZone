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
    using MovieZone.Service.AWS.Storage.PublicImageStorage;
    using MovieZone.Service.DTOs.Movie;
    using MovieZone.Service.DTOs.Pagination;
    using MovieZone.Service.Mapping;
    using MovieZone.Service.MoviesCategory;
    using MovieZone.Service.Pagination;
    using MovieZone.Service.Time;

    public class MovieService : IMovieService
    {
        private const string DefaultMoviesCategoryName = Globals.CategoryMovies.DefaultMoviesCategoryName;
        private const int PageSize = Globals.CategoryMovies.PageSize;

        private readonly ITimeService timeService;
        private readonly IMoviesCategoryService moviesCategoryService;
        private readonly IDeletableEntityRepository<Movie> movieRepository;
        private readonly IPaginationService paginationService;
        private readonly IPublicImageStorageService publicImageStorageService;
        private readonly IMovieStorageService movieStorageService;
        private readonly IActorService actorService;

        public MovieService(
            IDeletableEntityRepository<Movie> movieRepository,
            IPaginationService paginationService,
            ITimeService timeService,
            IMoviesCategoryService moviesCategoryService,
            IMovieStorageService movieStorageService,
            IActorService actorService,
            IPublicImageStorageService publicImageStorageService)
        {
            this.movieRepository = movieRepository;
            this.paginationService = paginationService;
            this.timeService = timeService;
            this.moviesCategoryService = moviesCategoryService;
            this.movieStorageService = movieStorageService;
            this.actorService = actorService;
            this.publicImageStorageService = publicImageStorageService;
        }

        public PaginationDTO<GetMoviesInCategoryMovieDTO> GetMoviesInCategory(
            string categoryId,
            int currentPage)
        {
            var query = this.movieRepository
                .AllAsNoTracking();

            if (categoryId != "default")
            {
                query = query.Where(x => x.MovieCategories
                    .Any(x => x.Id == categoryId));
            }
            else
            {
                query = query.Where(x => x.MovieCategories
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
            this.ValidateAddMovieInputDTO(input);

            var movie = AutoMapperConfig
                .MapperInstance.Map<AddMovieInputDTO, Movie>(input);
            movie.Duration = new TimeSpan(input.HoursDuration, input.MinutesDuration, 0);

            using (var movieStream = input.MovieFile.OpenReadStream())
            {
                await this.movieStorageService.UploadFileAsync(
                    movieStream,
                    movie.Id,
                    GetFileExtension(input.MovieFile.FileName));
            }

            movie.DetailsImgName = $"{movie.Id}-detailsImg";
            using (var detailsImgStream = input.DetailsImg.OpenReadStream())
            {
                await this.publicImageStorageService.UploadFileAsync(
                    detailsImgStream,
                    movie.DetailsImgName,
                    GetFileExtension(input.DetailsImg.FileName));
            }

            movie.ListingImgName = $"{movie.Id}-listingImg";
            using (var listingImgStream = input.ListingImg.OpenReadStream())
            {
                await this.publicImageStorageService.UploadFileAsync(
                    listingImgStream,
                    movie.ListingImgName,
                    GetFileExtension(input.DetailsImg.FileName));
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

                movie.MovieCategories.Add(category);
            }

            await this.movieRepository.AddAsync(movie);
            await this.movieRepository.SaveChangesAsync();
        }

        private static string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName)[1..].ToLower();
        }

        private void ValidateAddMovieInputDTO(AddMovieInputDTO input)
        {
            int maxYear = this.timeService.GetUtcNow().Year
                + ValidationConstants.Movie.YearOfPublishingAfterCurrentYearRange;

            if (input.YearOfPublishing < ValidationConstants.Movie.YearOfPublishingMinValue
                || input.YearOfPublishing > maxYear)
            {
                throw new ArgumentException(
                    ValidationConstants.Movie.GetYearOfPublishingExceptionMessage(maxYear));
            }

            string movieFileExtension = GetFileExtension(input.MovieFile.FileName);
            if (!ValidationConstants.Movie.MovieFileValidExtensions.Contains(movieFileExtension))
            {
                throw new ArgumentException(
                    ValidationConstants.Movie.MovieFileExtensionExceptionMessage);
            }

            string listingImgExtension = GetFileExtension(input.ListingImg.FileName);
            if (!ValidationConstants.ImageFileValidExtensions.Contains(listingImgExtension))
            {
                throw new ArgumentException(
                    ValidationConstants.ImageFileExtensionExceptionMessage);
            }

            string detailsImgExtension = GetFileExtension(input.DetailsImg.FileName);
            if (!ValidationConstants.ImageFileValidExtensions.Contains(detailsImgExtension))
            {
                throw new ArgumentException(
                    ValidationConstants.ImageFileExtensionExceptionMessage);
            }
        }
    }
}
