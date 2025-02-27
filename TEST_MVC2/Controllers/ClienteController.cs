using Microsoft.AspNetCore.Mvc;
using TEST_MVC2.Data;
using TEST_MVC2.Models;

namespace TEST_MVC2.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDataAccessLayer objClienteDAL = new ClienteDataAccessLayer();
        public IActionResult Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes = objClienteDAL.GetClientes().ToList();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                objClienteDAL.InsertCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Cliente cliente = objClienteDAL.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                objClienteDAL.UpdateCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Cliente cliente = objClienteDAL.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            objClienteDAL.DeleteCliente(id);
            return RedirectToAction("Index");
        }

    }
}
