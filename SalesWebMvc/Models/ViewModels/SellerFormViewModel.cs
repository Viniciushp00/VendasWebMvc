namespace SalesWebMvc.Models.ViewModels
{
    //Classe com dados para cadastro de vendedor
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
