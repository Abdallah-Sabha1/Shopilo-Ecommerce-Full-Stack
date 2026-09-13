namespace ShopiloApi.Exceptions;

public class EmptyCartException : Exception
{
    public EmptyCartException() : base("The cart is empty.")
    {
    }
}
