using Microsoft.AspNetCore.Mvc;
using TEST_MVC2.Data;
using TEST_MVC2.Models;

namespace TEST_MVC2.Controllers
{
    public class ProductoController : Controller
    {
        ProductoDataAccessLayer objProductoDAL = new ProductoDataAccessLayer();

        public IActionResult Index()
        {
            List<Producto> productos = objProductoDAL.GetProductos().ToList();
            return View(productos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                objProductoDAL.InsertProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Producto producto = objProductoDAL.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                objProductoDAL.UpdateProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Producto producto = objProductoDAL.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            objProductoDAL.DeleteProducto(id);
            return RedirectToAction("Index");
        }
    }
}
