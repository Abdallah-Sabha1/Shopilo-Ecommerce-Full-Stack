namespace ShopiloApi.Exceptions;

public class SeedAlreadyCompletedException : Exception
{
    public SeedAlreadyCompletedException() : base("Sample data has already been imported.")
    {
    }
}
