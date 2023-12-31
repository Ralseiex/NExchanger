﻿namespace NExchanger.Services.Transactions;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException()
    {
    }

    public TransactionNotFoundException(string message) : base(message)
    {
    }

    public TransactionNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}