using System.Collections.Generic;
using System;
using System.Linq;


namespace SalesWebMVC.Models
{
    public class Department
    {
       

        public int Id { get; set; }
        public string Name { get ; set ; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller s) 
        {
            Sellers.Add(s);
        }

        public double TotalSales(DateTime initial, DateTime final) 
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }


    }


}
