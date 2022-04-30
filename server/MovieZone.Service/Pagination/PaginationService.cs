namespace MovieZone.Service.Pagination
{
    using System;
    using System.Linq;

    using MovieZone.Service.DTOs.Pagination;

    public class PaginationService : IPaginationService
    {
        public PaginationDTO<T> GetPaged<T>(IQueryable<T> query, int currentPage, int pageSize)
             where T : class
        {
            var collection = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            var pagingData = new PaginationDTO<T>
            {
                AllPagesCount = (int)Math.Ceiling((double)query.Count() / pageSize),
                CurrentPageElements = collection,
            };

            return pagingData;
        }
    }
}
