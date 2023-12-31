﻿namespace NExchanger.Services.Transactions;

public class InvalidTransactionException : Exception
{
    public InvalidTransactionException()
    {
    }

    public InvalidTransactionException(string message) : base(message)
    {
    }

    public InvalidTransactionException(string message, Exception inner) : base(message, inner)
    {
    }
}