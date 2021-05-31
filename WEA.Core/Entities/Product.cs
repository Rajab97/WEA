using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class Product : AuditEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ProductLocationDescription { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
        public decimal PurchasePrice { get; set; }
        public Guid PurchaseCurrencyTypeId { get; set; }
        public CurrencyType PurchaseCurrencyType { get; set; }
        public decimal SalePrice { get; set; }
        public Guid SaleCurrencyTypeId { get; set; }
        public CurrencyType SaleCurrencyType { get; set; }
        public int TotalQuantity { get; set; }

    }
}
