using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Sync.API.Persistance.DTO;
using Sync.API.Services;
using Sync.API.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sync.API.Controllers
{
    public class SyncController : Controller
    {
        private readonly Persistance.SyncContext _context;
        private readonly IStringLocalizer<Resources.SharedMessages> _sharedMessagesLocalizer;
        private readonly IShopify _shopify;

        public SyncController(IStringLocalizer<Resources.SharedMessages> sharedMessagesLocalizer,
                              IShopify shopify,
                              Persistance.SyncContext context)
        {
            _sharedMessagesLocalizer = sharedMessagesLocalizer;
            _shopify = shopify;
            _context = context;
        }

        [HttpPost("FillNewProducts")]
        public async Task<ActionResult> FillNewProducts()
        {
            MessageResponseDTO response = new MessageResponseDTO();
            try
            {
                var ShopifyProducts = await _shopify.GetAllProducts();

                if (ShopifyProducts != null)
                {
                    var localProducts = await _context.ColaArticulo.Select(x => x.SKU).ToListAsync();
                    List<Variant> variants = new List<Variant>();

                    foreach (var item in ShopifyProducts.products)
                    {
                        variants.AddRange(item.variants); 
                    }

                    var newProducts = (from A in variants
                                      where !localProducts.Contains(A.sku)
                                      select new ColaArticulos
                                      {
                                          SKU = A.sku,
                                          ProductId = A.product_id.ToString(),
                                          Cantidad = A.inventory_quantity,
                                          FechaRegistro = DateTime.Now,
                                          FechaActualizacion = A.updated_at,
                                          Sincronizado = true
                                      }).ToList();

                    if (newProducts.Any())
                    {
                        await _context.ColaArticulo.AddRangeAsync(newProducts);
                        await _context.SaveChangesAsync();
                    }

                }

                response.Message = _sharedMessagesLocalizer.GetString("Success");
                response.Type = "1";
                response.Date = DateTime.Now;

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = null != ex.InnerException ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost("SyncProducts")]
        public async Task<ActionResult> SyncProducts()
        {
            MessageResponseDTO response = new MessageResponseDTO();

            try
            {
                var sincProducts = await _context.ColaArticulo.Where(x => !x.Sincronizado).ToListAsync();

                if (sincProducts != null)
                {
                    VariantsDTO updateProduct;
                    foreach (var product in sincProducts)
                    {
                        updateProduct = await _shopify.GetProductVariant(product.ProductId, product.SKU);

                        foreach (var variant in updateProduct.Variants)
                        {
                            await _shopify.UpdateProductVariant(variant, (int)product.Cantidad);
                            product.Sincronizado = true;
                            product.FechaActualizacion = DateTime.Now;
                            _context.Update(product);
                            await _context.SaveChangesAsync();
                        }
                    }
                   
                }

                response.Message = _sharedMessagesLocalizer.GetString("Success");
                response.Type = "1";
                response.Date = DateTime.Now;

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = null != ex.InnerException ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost("SyncOrders")]
        public async Task<ActionResult> SyncOrders()
        {
            MessageResponseDTO response = new MessageResponseDTO();
            try
            {
                var orders = await _shopify.GetAllOrdersPaid();
                var localsOrders = await _context.Orden.Select(x => x.NumeroOrden).ToListAsync();

                if (orders.orders.Where(x => !localsOrders.Contains(x.order_number)).Any())
                {
                    foreach (var item in orders.orders)
                    {
                        Orden newOrder = new Orden()
                        {
                            NumeroOrden = item.order_number,
                            SubTotal = item.subtotal_price,
                            Total = item.total_price,
                            TotalImpuestos = item.total_tax,
                            FechaCreacion = DateTime.Now,
                            FechaOrden = item.updated_at
                        };

                        await _context.Orden.AddAsync(newOrder);
                        await _context.SaveChangesAsync();

                        Cliente newClient = new Cliente()
                        {
                            Nombre = item.customer.first_name,
                            Apellido = item.customer.last_name,
                            Direccion = item.customer.default_address.address1,
                            Email = item.customer.email,
                            Telefono = string.IsNullOrEmpty(item.customer.phone) ? "0" : item.customer.phone,
                            OrdenId = newOrder.OrdenId
                        };

                        await _context.Cliente.AddAsync(newClient);
                        await _context.SaveChangesAsync();

                        var products = (from A in item.line_items
                                        select new Articulos()
                                        {
                                            OrdenId = newOrder.OrdenId,
                                            SKU = A.sku,
                                            Precio = A.price,
                                            Cantidad = A.quantity,
                                            SubTotal = A.quantity * A.price,
                                            TotalImpuestos = A.tax_lines.Sum(x => (x.price * x.rate) / 100),
                                            Total = (A.price + A.tax_lines.Sum(x => (x.price * x.rate) / 100) - A.total_discount) * A.quantity,
                                            Nombre = A.name
                                        }).ToList();

                        await _context.Articulo.AddRangeAsync(products);
                        await _context.SaveChangesAsync();

                    }


                }

                response.Message = _sharedMessagesLocalizer.GetString("Success");
                response.Type = "1";
                response.Date = DateTime.Now;

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = null != ex.InnerException ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
