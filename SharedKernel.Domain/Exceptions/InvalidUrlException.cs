using System;

namespace SharedKernel.Domain.Exceptions;

public class InvalidUrlException : ArgumentException
{
    public InvalidUrlException(string message) : base(message)
    {
    }

    public InvalidUrlException(string message, Exception innerException) : base(message, innerException)
    {
    }
}