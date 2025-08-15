using AutoMapper;
using JWTTraining.Dtos.Blog;
using JWTTraining.Entities;

namespace JWTTraining.AutoMapperProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogsListDto>();
            CreateMap<Blog, BlogDto>();
            CreateMap<AddBlogDto, Blog>();
            CreateMap<Blog, UpdateBlogDto>();
            CreateMap<UpdateBlogDto, Blog>();
        }
    }
}
