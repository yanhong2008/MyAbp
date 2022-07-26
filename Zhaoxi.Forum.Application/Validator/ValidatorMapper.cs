﻿using FluentValidation;

namespace Zhaoxi.Forum.Application.Validator;

public static class ValidatorMapper
{
    private static readonly string[] DefaultErrors = new string[] { };

    public static readonly LoginInputValidator LoginInput =
        new LoginInputValidator();

    public static readonly RegistInputValidator RegistInput = new RegistInputValidator();

    public static readonly ReplyPostsInputValidator ReplyPostsInput =
        new ReplyPostsInputValidator();

    public static readonly SendTopicInputValidator SendTopicInput =
        new SendTopicInputValidator();

    public static bool CustomValidate<DTO>(this IValidator<DTO> validator,
        DTO dto,
        out IEnumerable<string> errors)
        where DTO : class
    {
        errors = DefaultErrors;
        var validatorResult = validator.Validate(dto);
        var isValid = validatorResult.IsValid;
        if (!validatorResult.IsValid)
        {
            errors = validatorResult.Errors.Select(err => err.ErrorMessage);
        }

        return isValid;
    }
}
