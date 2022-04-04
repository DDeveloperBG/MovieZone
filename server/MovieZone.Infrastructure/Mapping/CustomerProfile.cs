namespace MovieZone.Infrastructure.Mapping
{
    using AutoMapper;
    using MovieZone.Domain.Entities;
    using MovieZone.Infrastructure.ViewModel;

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            this.CreateMap<CustomerModel, Customer>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.CustomerId))
                .ReverseMap();
        }
    }
}
