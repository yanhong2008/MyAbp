﻿namespace Zhaoxi.Forum.Application.Contracts.User;

public class UserDto
{
    public long UserId { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户头像
    /// </summary>
    public string UserAvatar { get; set; }
}
