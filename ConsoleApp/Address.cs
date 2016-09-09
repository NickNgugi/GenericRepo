namespace ConsoleApp
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}