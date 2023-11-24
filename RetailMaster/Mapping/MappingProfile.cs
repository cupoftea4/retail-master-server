using AutoMapper;

namespace RetailMaster.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Product, DTO.ProductDto>();
        CreateMap<Models.Invoice, DTO.InvoiceDto>();
        CreateMap<Models.InvoiceProduct, DTO.InvoiceProductDto>();
        CreateMap<Models.User, DTO.UserDto>();
    }
}