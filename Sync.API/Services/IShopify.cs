using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sync.API.Persistance.DTO;

namespace Sync.API.Services
{
    public interface IShopify
    {
        Task<ProductsDTO> GetAllProducts();
        Task<VariantsDTO> GetProductVariant(string productId, string sku);
        Task UpdateProductVariant(Variant variant, int quantity);
        Task<OrderDTO> GetAllOrdersPaid();
    }
}
