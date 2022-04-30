namespace MovieZone.Service.DTOs.Pagination
{
    using System.Collections.Generic;

    public class PaginationDTO<T>
    {
        public IEnumerable<T> CurrentPageElements { get; set; }

        public int AllPagesCount { get; set; }
    }
}
