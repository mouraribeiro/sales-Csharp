namespace SalesWebMVC.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
