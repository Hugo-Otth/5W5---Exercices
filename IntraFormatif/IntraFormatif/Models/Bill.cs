namespace IntraFormatif.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual IEnumerable<Item> Items { get; set; }
    }

}
