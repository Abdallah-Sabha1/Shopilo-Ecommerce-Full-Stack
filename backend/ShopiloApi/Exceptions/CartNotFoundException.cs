namespace ShopiloApi.Exceptions
{
    public class CartNotFoundException:Exception
    {
        public CartNotFoundException(Guid cartID):base($"Cart with ID {cartID} not found.")
        {

        }
    }
}
