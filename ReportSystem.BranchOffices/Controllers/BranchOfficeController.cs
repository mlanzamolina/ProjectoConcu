using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportSystem.BranchOffices.Controllers
{
    public class BranchOfficeController : Controller
    {
        // GET: BranchOfficeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BranchOfficeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BranchOfficeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BranchOfficeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BranchOfficeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BranchOfficeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BranchOfficeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BranchOfficeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
