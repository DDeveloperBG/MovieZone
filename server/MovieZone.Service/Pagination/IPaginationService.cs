namespace MovieZone.Service.Pagination
{
    using System.Linq;

    using MovieZone.Service.DTOs.Pagination;

    public interface IPaginationService
    {
        PaginationDTO<T> GetPaged<T>(IQueryable<T> query, int currentPage, int pageSize)
            where T : class;
    }
}
