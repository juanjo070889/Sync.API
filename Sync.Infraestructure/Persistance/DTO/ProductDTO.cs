using System;
using System.Collections.Generic;
using System.Text;

namespace Sync.Infraestructure.Persistance.DTO
{
    // ProductDTO myDeserializedClass = JsonConvert.DeserializeObject<ProductDTO>(myJsonResponse);
    public class Option
    {
        public string option1 { get; set; }
    }

    public class Price
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class CompareAtPrice
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentPrice
    {
        public Price price { get; set; }
        public CompareAtPrice compare_at_price { get; set; }
        public List<PresentmentPrice> presentment_prices { get; set; }
    }

    public class ProductDTO
    {
        public string barcode { get; set; }
        public string compare_at_price { get; set; }
        public DateTime created_at { get; set; }
        public string fulfillment_service { get; set; }
        public int grams { get; set; }
        public int id { get; set; }
        public int image_id { get; set; }
        public int inventory_item_id { get; set; }
        public string inventory_management { get; set; }
        public string inventory_policy { get; set; }
        public int inventory_quantity { get; set; }
        public int old_inventory_quantity { get; set; }
        public int inventory_quantity_adjustment { get; set; }
        public Option option { get; set; }
        public PresentmentPrice presentment_prices { get; set; }
        public int position { get; set; }
        public string price { get; set; }
        public int product_id { get; set; }
        public bool requires_shipping { get; set; }
        public string sku { get; set; }
        public bool taxable { get; set; }
        public string tax_code { get; set; }
        public string title { get; set; }
        public DateTime updated_at { get; set; }
        public int weight { get; set; }
        public string weight_unit { get; set; }
    }

}
