using AutoMapper;
using InterShopAPI.DTO;
using InterShopAPI.Models;

namespace InterShopAPI.AutoMapperProfiles;

public class InterShopProfiles : Profile
{
    public InterShopProfiles()
    {
        CreateMap<ImagesOfProduct, ImagesOfProductDTO>();
        
        CreateMap<ProductVariant, ProductVariantDetailDTO>().ReverseMap();
        
        CreateMap<ProductVariant, ProductVariantMinimalDTO>()
            .ForMember(dest => dest.Cost, option => option.MapFrom(src => src.PriceHistories.FirstOrDefault().Price)).ReverseMap();
        
        CreateMap<Product, ProductDetailDTO>()
            .ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Rating, option => option.MapFrom(src => (float)src.Comments.Sum(p => p.Rating / (float)src.Comments.Count))).ReverseMap();
        
        CreateMap<Product, ProductMinimalDTO>()
            .ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Rating, option => option.MapFrom(src => (float)src.Comments.Sum(p => p.Rating / (float)src.Comments.Count)))
            .ForMember(dest => dest.Price, option => option.MapFrom(src => src.ProductVariants.FirstOrDefault(p => p.IsMain).PriceHistories.FirstOrDefault().Price))
            .ForMember(dest => dest.Discount, option => option.MapFrom(src => src.DiscountHistories.FirstOrDefault().Discount)).ReverseMap();
        
        CreateMap<PriceHistory, PriceHistoryDTO>();
        
        CreateMap<DiscountHistory, DiscountHistoryDTO>();
        
        CreateMap<ProductVariantCharacteristics, ProductVariantCharacteristicsDTO>()
            .ForMember(dest => dest.Characteristic, option => option.MapFrom(src => src.Characteristic.Name))
            .ForMember(dest => dest.Unit, option => option.MapFrom(src => src.Characteristic.Unit.Name));
        
        CreateMap<User, UserMinimalDTO>();
        
        CreateMap<User, UserDetailDTO>()
            .ForMember(dest => dest.RoleName, option => option.MapFrom(src => src.Role.Name))
            .ReverseMap();
        
        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.Login, option => option.MapFrom(src => src.User.Login)).ReverseMap();

        
        CreateMap<Basket, BasketDTO>()
            .ForMember(dest => dest.Product, option => option.MapFrom(src => src.ProductVariant.Product))
            .ForMember(dest => dest.TotalCost, option => option.MapFrom(src => src.ProductVariant.Product.DiscountHistories.Count() == 0 ? src.ProductVariant.PriceHistories.FirstOrDefault().Price * src.Count :  src.ProductVariant.PriceHistories.FirstOrDefault().Price * (1 - (float)src.ProductVariant.Product.DiscountHistories.FirstOrDefault().Discount / 100f) * src.Count))
            .ReverseMap();

        CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();

        CreateMap<Order, OrderMinimalDTO>()
            .ForMember(dest => dest.OrderStatus, option => option.MapFrom(src => src.OrderStatus.Name))
            .ForMember(dest => dest.DeliveryType, option => option.MapFrom(src => src.DeliveryType.Name))
            .ForMember(dest => dest.PayStatus, option => option.MapFrom(src => src.PayStatus.Name))
            .ForMember(dest => dest.PaymentType, option => option.MapFrom(src => src.PaymentType.Name))
            .ForMember(dest => dest.TotalCost, option => option.MapFrom(dest => (float)dest.OrderDetails.Sum(od => (float)od.Price * od.Count)));
    
        CreateMap<Order, OrderDetailDTO>()
            .ForMember(dest => dest.OrderStatus, option => option.MapFrom(src => src.OrderStatus.Name))
            .ForMember(dest => dest.DeliveryType, option => option.MapFrom(src => src.DeliveryType.Name))
            .ForMember(dest => dest.PayStatus, option => option.MapFrom(src => src.PayStatus.Name))
            .ForMember(dest => dest.PaymentType, option => option.MapFrom(src => src.PaymentType.Name))
            .ForMember(dest => dest.TotalCost, option => option.MapFrom(dest => (float)dest.OrderDetails.Sum(od => (float)od.Price * od.Count)))
            .ReverseMap()
            .ForMember(dest => dest.PaymentType, option => option.Ignore())
            .ForMember(dest => dest.DeliveryType, option => option.Ignore());
    
        CreateMap<Shop, ShopDTO>().ReverseMap();
    }
}