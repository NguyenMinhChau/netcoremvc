using source01.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using source01.EF;

namespace source01.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            var dao = new ContentDAO();
            var model = dao.ListAll();
            return View(model.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDAO();
                long res = dao.Insert(model);
                if (res > 0)
                {
                    SetAlert("Thêm sản phẩm thành công", "success");
                    return RedirectToAction("Index","Content");
                }
                else
                {
                    SetAlert("Thêm sản phẩm không thành công", "error");
                    return RedirectToAction("Index","Content");
                }
            }
            SetViewBag();
            SetAlert("Thêm sản phẩm không thành công", "error");
            return RedirectToAction("Index","Content");
        }
        protected void SetAlert(string mess, string type)
        {
            TempData["AlertMessage"] = mess;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDAO();
            var content = dao.GetByID(id);

            SetViewBag(content.CategoryID);
            return View(content);
        }
        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDAO();
                bool res = dao.Update(model);
                if (res)
                {
                    SetAlert("Cập nhật sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    SetAlert("Cập nhật sản phẩm không thành công", "error");
                }
            }
            SetViewBag(model.CategoryID);
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(),"ID","Name", selectedID);
        }
    }
}