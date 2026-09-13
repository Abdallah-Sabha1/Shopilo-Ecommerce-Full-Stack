namespace ShopiloApi.Exceptions
{
    public class CartItemNotFoundException:Exception
    {

        public CartItemNotFoundException(int cartItemId)
                : base($"Cart item '{cartItemId}' was not found.")
        {
        }
    }

}
