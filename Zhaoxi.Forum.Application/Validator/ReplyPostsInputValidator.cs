using FluentValidation;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Application.Contracts.Topic;

namespace Zhaoxi.Forum.Application.Validator;

public class ReplyPostsInputValidator : AbstractValidator<ReplyPostsInput>
{
    public ReplyPostsInputValidator()
    {
        RuleFor(m => m.TopicId).GreaterThan(0)
            .WithName("主题").WithMessage("{PropertyName}为必选项！");

        RuleFor(m => m.UserId).GreaterThan(0)
            .WithName("用户").WithMessage("{PropertyName}为必选项！");

        //RuleFor(m => m.PostId).GreaterThan(0)
        //    .WithName("某一个回复").WithMessage("{PropertyName}为必选项！");

        RuleFor(m => m.Content)
            .NotEmpty().WithName("回复内容").WithMessage("{PropertyName}为必填项！")
            .Length(10, 2000).WithName("回复内容").WithMessage("{PropertyName}长度10到2000个字！");
    }
}
