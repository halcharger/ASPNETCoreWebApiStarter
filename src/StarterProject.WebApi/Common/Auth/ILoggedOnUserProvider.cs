﻿namespace StarterProject.WebApi.Common.Auth
{
    public interface ILoggedOnUserProvider
    {
        string UserId { get; }
        string Username { get; }
        string Email { get; }
        string[] Roles { get; }

    }
}