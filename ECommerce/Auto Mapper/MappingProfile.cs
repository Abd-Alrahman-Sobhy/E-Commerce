using AutoMapper;
using ECommerce.Dtos;
using ECommerce.Models;

namespace ECommerce.Auto_Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<UserRegisterDto, User>()
			.ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
			.ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));

		CreateMap<ProductCreateDto, Product>();
		CreateMap<ProductUpdateDto, Product>();

		CreateMap<CategoryCreateDto, Category>();
		CreateMap<CategoryUpdateDto, Category>();

		CreateMap<CartItemUpdateDto, CartItem>();

		CreateMap<Category, CategoryOutputDto>();

		CreateMap<Product, ProductOutputDto>()
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));

		CreateMap<User, UserOutputDto>();
		CreateMap<UpdateUserInfoDto, User>();

		CreateMap<Cart, CartOutputDto>()
			.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
			.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Items!.Sum(i => i.Quantity * i.Product!.Price)));

		CreateMap<CartItem, CartItemOutputDto>()
			.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product!.ImageUrl))
			.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product!.Price));

		CreateMap<Order, OrderOutputDto>();
		CreateMap<OrderItem, OrderItemOutputDto>()
			.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name));
	}
}
