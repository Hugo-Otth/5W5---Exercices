using IntraFormatif.Data;
using IntraFormatif.Models;

namespace IntraFormatif.Services
{
    public class BillService
    {
        private ApplicationDbContext _context;
        public BillService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Bill? CreateBill(string name, IEnumerable<Item> items)
        {
            if (name == null || name.Length == 0)
                throw new Exception("You need a name to create a bill");

            if (items.Count() == 0)
                return null;

            foreach (Item item in items)
            {
                if (item.Price < 0)
                {
                    throw new AreYouInsaneException("Not paying for you to take something!");
                }
                if (item.Price == 0)
                {
                    throw new AreYouInsaneException("Not giving free stuff either!!");
                }
            }

            Bill bill = new Bill()
            {
                Name = name,
                Items = items
            };

            _context.Add(bill);

            return bill;
        }
    }

}
