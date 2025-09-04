namespace Clean.Domain.Abstractions;

public interface IDateTime
{
    DateTime UtcNow { get; }
}