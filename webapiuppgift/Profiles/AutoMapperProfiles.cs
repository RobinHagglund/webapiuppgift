using AutoMapper;
using webapiuppgift.Models.Product;
using webapiuppgift.Models.User;

namespace webapiuppgift.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductEntity, Product>()
                .ForMember(d => d.ArticleNumber, option => option.MapFrom(s => s.ArticleNr));
            CreateMap<Product, ProductEntity>()
                .ForMember(d => d.ArticleNr, option => option.MapFrom(s => s.ArticleNumber));

            CreateMap<UserEntity, User>()
                .ForMember(d => d.Id, option => option.MapFrom(s => s.Id));
            CreateMap<User, UserEntity>()
                .ForMember(d => d.Id, option => option.MapFrom(s => s.Id));



        }
    }
}
