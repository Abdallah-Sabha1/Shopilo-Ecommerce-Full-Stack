namespace ShopiloApi.ExternalDtos
{
    public class DummyProductListResponse
    {
        public List<DummyProductDto> Products { get; set; } = new List<DummyProductDto>();
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}
