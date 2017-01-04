using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Produtos")]
    public class ProductsController : ApiController
    {
        private dbtesteEntities db = new dbtesteEntities();

        // GET: api/Products
        [AcceptVerbs("GET")]
        [Route("ConsultarProdutos")]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products/5
        [AcceptVerbs("GET")]
        [Route("ConsultarProdutoPorCodigo/{id:int}")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProductAsync(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [AcceptVerbs("PUT")]
        [Route("AlterarProduto/{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductAsync(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [AcceptVerbs("POST")]
        [Route("IncluirProduto")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProductAsync(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ( product.Id == 0)
                {
                    var idMax = (from p in db.Products
                                 orderby p.Id descending
                                 select p).First();
                    product.Id = idMax.Id + 1;
                }

            db.Products.Add(product);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Id))
                {
                    return Json(new { Message = "ja existe" } );
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
            return Json(new { Message = "Incluido com Sucesso" });
        }

        // DELETE: api/Products/5
        [AcceptVerbs("DELETE")]
        [Route("ExcluirProduto/{id:int}")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProductAsync(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}