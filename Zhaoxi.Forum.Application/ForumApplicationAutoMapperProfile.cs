﻿using AutoMapper;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Application.Contracts.Auth;
using Zhaoxi.Forum.Application.Contracts.Category;
using Zhaoxi.Forum.Application.Contracts.Topic;
using Zhaoxi.Forum.Application.Contracts.User;
using Zhaoxi.Forum.Domain;
using Zhaoxi.Forum.Domain.Category;
using Zhaoxi.Forum.Domain.Topic;
using Zhaoxi.Forum.Domain.User;

namespace Zhaoxi.Forum.Application;

public class ForumApplicationAutoMapperProfile : Profile
{
    public ForumApplicationAutoMapperProfile()
    {
        CreateMap<CategoryImportDto, CategoryEntity>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.No));

        CreateMap<TopicImportDto, TopicEntity>()
            .ForMember(dest => dest.TopicName, options => options.MapFrom(src => src.Title))
            .ForMember(dest => dest.TopicContent, options => options.MapFrom(src => src.Content));

        CreateMap<UserImportDto, UserEntity>()
            .ForMember(dest => dest.Sex, options => options.MapFrom(src => src.Sex == "女" ? 0 : 1));

        CreateMap<RegistInput, UserEntity>();
        //.ForMember(dest => dest.UserAvatar, options => options.MapFrom(src => src.Avatar));
        CreateMap<UserEntity, RegistInput>();
        //.ForMember(dest => dest.Avatar, options => options.MapFrom(src => src.UserAvatar));

        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<CategoryDto, CategoryEntity>();

        CreateMap<TopicDto, TopicEntity>();
        CreateMap<TopicEntity, TopicDto>();

 

        CreateMap<UserEntity, UserDto>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.Id));
        CreateMap<UserDto, LoginDto>();
    }
}