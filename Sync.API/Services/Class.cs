using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sync.API.Services
{
    public class Class
    {
        //private void xxx() {

        //    string ShopPath = "/admin/api/2020-04/";
        //    string shopifyAddProduct = "products.json";
        //    string shopifyGetProductFmt = "products.json?title={0}";
        //    string URL = String.Format("https://{0}.myshopify.com{1}", ShopName, ShopPath);
        //    var clientHandler = new HttpClientHandler
        //    {
        //        Credentials = new NetworkCredential(APIkey/*username*/, APIpass/*Password*/),
        //        PreAuthenticate = true
        //    };
        //    HttpClient client = new HttpClient(clientHandler);
        //    client.BaseAddress = new Uri(URL);

        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    // need an access token...
        //    client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", APIpass);

        //    var shopify = new
        //    {
        //        product = new
        //        {
        //            title = "Burton Custom Freestyle 151",
        //            body_html = "<strong>Good snowboard!</strong>",
        //            vendor = "Snowboard",
        //            product_type = "Snowboard",
        //            published = false
        //        }
        //    };

        //    // before we try and add a product, we should query if we already have it?
        //    string shopifyGetProduct = String.Format(shopifyGetProductFmt, Uri.EscapeUriString(shopify.product.title));
        //    HttpResponseMessage getResponse = client.GetAsync(shopifyGetProduct).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        //    if (getResponse.IsSuccessStatusCode)
        //    {
        //        bool foundProduct;
        //        foundProduct = false;
        //        // Parse the response body.
        //        GetProducts curProduct = getResponse.Content.ReadAsAsync<GetProducts>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
        //        if (curProduct != null)
        //        {
        //            foreach (var cp in curProduct.products)
        //            {
        //                Console.WriteLine("found id: {0}", cp.id);
        //                foundProduct = true;
        //            }
        //        }
        //        if (!foundProduct)
        //        {

        //            HttpResponseMessage response = client.PostAsJsonAsync(shopifyAddProduct, shopify).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Parse the response body.
        //                AddProducts newProduct = response.Content.ReadAsAsync<AddProducts>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
        //                if ((newProduct != null) && (newProduct.product != null))
        //                {
        //                    Console.WriteLine("new id: {0}", newProduct.product.id);
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("{0} ({1})", (int)getResponse.StatusCode, getResponse.ReasonPhrase);
        //    }

        //    //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
        //    client.Dispose();

        //}
    }
}
