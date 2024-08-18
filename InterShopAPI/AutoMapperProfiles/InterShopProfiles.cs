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
            .ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Rating, option => option.MapFrom(src => (float)src.Comments.Sum(p => p.Rating / (float)src.Comments.Count)));

        CreateMap<PriceHistory, PriceHistoryDTO>();
        CreateMap<DiscountHistory, DiscountHistoryDTO>(); 
        CreateMap<ProductVariantCharacteristics, ProductVariantCharacteristicsDTO>()
            .ForMember(dest => dest.Characteristic, option => option.MapFrom(src => src.Characteristic.Name));

        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.Login, option => option.MapFrom(src => src.User.Login));
    }
}