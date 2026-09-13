namespace ShopiloApi.Exceptions
{
    public class InsufficientStockException : Exception
    {

        public InsufficientStockException(int productId, int requested, int available)

            : base($"Insufficient stock for product {productId}. Requested: {requested}, Available: {available}")

       {

        }
  }
}
