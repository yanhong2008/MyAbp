using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Domain.Category;
using Zhaoxi.Forum.Domain.Topic;

namespace Zhaoxi.Forum.Application
{
    public class ForumApplicationAutoMapperProfile : Profile
    {
        public ForumApplicationAutoMapperProfile()
        {
            CreateMap<CategoryImportDto, CategoryEntity>()
           .ForMember(dest => dest.Id, options => options.MapFrom(src => src.No));

            CreateMap<TopicImportDto, TopicEntity>()
                .ForMember(dest => dest.TopicName, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.TopicContent, options => options.MapFrom(src => src.Content));


            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
            CreateMap<TopicEntity, TopicDto>().ReverseMap();
        }
    }
}
