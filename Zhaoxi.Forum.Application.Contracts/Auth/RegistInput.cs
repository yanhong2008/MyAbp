﻿namespace Zhaoxi.Forum.Application.Contracts.Auth;

public class RegistInput
{
    public string Phone { get; set; }

    public string Password { get; set; }

    public string NickName { get; set; }

    public string Email { get; set; }

    public int? Sex { get; set; }
}
