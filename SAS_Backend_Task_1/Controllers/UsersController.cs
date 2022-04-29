using Microsoft.AspNetCore.Mvc;
using SAS_Backend_Task_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAS_Backend_Task_1
{
    public class UsersController : Controller
    {
        IUserStore _store;

        public UsersController(IUserStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                _store.Add(model);

                return RedirectToAction("Index", "Home");
            }

            return View("Register", model);

        }

        public IActionResult Details(string id)
        {
            var gid = ulong.Parse(id);

            return View("Details", _store.Get(gid));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var gid = ulong.Parse(id);
            return View("Edit", _store.Get(gid));
        }

        [HttpPost]
        public IActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                _store.Edit(model.ID, model);
                return RedirectToAction("Index", "Home");
            }
            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var gid = ulong.Parse(id);
            return View("Delete", _store.Get(gid));
        }

        [HttpPost]
        public IActionResult Delete(UserModel model)
        {
            _store.Remove(model.ID);
            return RedirectToAction("Index", "Home");
        }
    }
}
