using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models.ViewModels
{
    public class SalesFormViewModel
    {
        public SalesRecord Sale { get; set; }
        public List<Seller> Sellers { get; set; }
        public List<string> SaleStatus { get; set; }
    }
}
