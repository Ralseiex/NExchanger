﻿namespace NExchanger.Services.Users;

public class UsernameTakenException : Exception
{
    public UsernameTakenException()
    {
    }

    public UsernameTakenException(string message) : base(message)
    {
    }

    public UsernameTakenException(string message, Exception inner) : base(message, inner)
    {
    }
}