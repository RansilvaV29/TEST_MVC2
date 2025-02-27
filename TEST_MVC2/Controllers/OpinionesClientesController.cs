using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TEST_MVC2.Data;
using TEST_MVC2.Models;

namespace TEST_MVC2.Controllers
{
    public class OpinionesClientesController : Controller
    {
        OpinionesClientesDataAccessLayer objOpinionDAL = new OpinionesClientesDataAccessLayer();
        ClienteDataAccessLayer objClienteDAL = new ClienteDataAccessLayer();
        ProductoDataAccessLayer objProductoDAL = new ProductoDataAccessLayer();

        public IActionResult Index()
        {
            List<OpinionesClientes> opiniones = objOpinionDAL.GetOpiniones().ToList();
            return View(opiniones);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Clientes = new SelectList(objClienteDAL.GetClientes(), "Codigo", "Nombres");
            ViewBag.Productos = new SelectList(objProductoDAL.GetProductos(), "ProductoID", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(OpinionesClientes opinion)
        {
            // Depuración: Ver los valores recibidos en la Consola de Salida
            Console.WriteLine($"Codigo: {opinion.Codigo}");
            Console.WriteLine($"ProductoID: {opinion.ProductoID}");
            Console.WriteLine($"Calificacion: {opinion.Calificacion}");
            Console.WriteLine($"Comentario: {opinion.Comentario}");
            Console.WriteLine($"Fecha: {opinion.Fecha}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState no es válido:");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }

                ViewBag.Clientes = new SelectList(objClienteDAL.GetClientes(), "Codigo", "Nombres", opinion.Codigo);
                ViewBag.Productos = new SelectList(objProductoDAL.GetProductos(), "ProductoID", "Nombre", opinion.ProductoID);

                return View(opinion);
            }

            objOpinionDAL.InsertOpinion(opinion);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            OpinionesClientes opinion = objOpinionDAL.GetOpinionById(id);
            if (opinion == null)
            {
                return NotFound();
            }
            return View(opinion);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            objOpinionDAL.DeleteOpinion(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            OpinionesClientes opinion = objOpinionDAL.GetOpinionById(id);
            if (opinion == null)
            {
                return NotFound();
            }

            ViewBag.Clientes = objClienteDAL.GetClientes();
            ViewBag.Productos = objProductoDAL.GetProductos();
            return View(opinion);
        }

        [HttpPost]
        public IActionResult Edit(OpinionesClientes opinion)
        {
            if (ModelState.IsValid)
            {
                objOpinionDAL.UpdateOpinion(opinion);
                return RedirectToAction("Index");
            }
            ViewBag.Clientes = objClienteDAL.GetClientes();
            ViewBag.Productos = objProductoDAL.GetProductos();
            return View(opinion);
        }
    }
}
