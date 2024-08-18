using AutoMapper;
using InterShopAPI.DTO;
using InterShopAPI.Models;

namespace InterShopAPI.AutoMapperProfiles;

public class InterShopProfiles : Profile
{
    public InterShopProfiles()
    {
        CreateMap<ImagesOfProduct, ImagesOfProductDTO>();
        CreateMap<ProductVariant, ProductVariantDTO>();

        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.ProductVariants, option => option.MapFrom(src => src.ProductVariants))
            .ForMember(dest => dest.ImagesOfProduct, option => option.MapFrom(src => src.ImagesOfProduct))
            .ForMember(dest => dest.DiscountHistories, option => option.MapFrom(src => src.DiscountHistories))

            .ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.Category.Name));
        CreateMap<PriceHistory, PriceHistoryDTO>();
        CreateMap<DiscountHistory, DiscountHistoryDTO>();
        CreateMap<ProductVariantCharacteristics, ProductVariantCharacteristicsDTO>()
            .ForMember(dest => dest.Characteristic, option => option.MapFrom(src => src.Characteristic.Name));
        CreateMap<User, UserMinimalDTO>();
        CreateMap<User, UserDetailDTO>()
            .ForMember(dest => dest.RoleName, option => option.MapFrom(src => src.Role.Name))
            .ReverseMap();            
                    
    }
}