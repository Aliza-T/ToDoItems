using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskClassWork.Properties;
using System.Web.Mvc;
using TaskClassWork.Models;
using TaskClassWork.Data;

namespace TaskClassWork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            IEnumerable<ToDoItems> ToDoList = manager.GetToDoList();
            return View(ToDoList);
        }
       
        public ActionResult Categories()
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            IEnumerable<Category> Categories = manager.GetCategories();
            return View(Categories);
        }
        public ActionResult Edit(int id)
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            Category c = manager.CatInfo(id);
            return View(c);
        }
        public ActionResult AddToDoItem()
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
             return View(manager.GetCategories());
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ToDoItems ToDoItem)
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            manager.AddToDoItem(ToDoItem);
            return Redirect("/Home/Index");
        }
        [HttpPost]
        public ActionResult MarkAsCompleted(int id)
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            manager.MarkAsCompleted(id);
            return Redirect("/Home/Index");
        }
        [HttpPost]
        public ActionResult Update(Category category)
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            manager.UpdateCategory(category);
            return Redirect("/Home/Categories");
        }
        [HttpPost]
        public ActionResult AddCategoryForm(Category c)
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            manager.AddCategory(c);
            return Redirect("/Home/Categories");
        }

        public ActionResult SpecificCategory(int catId)
        {
            ToDoManager manager = new ToDoManager(Settings.Default.ConStr);
            ViewModel vm = new ViewModel
            {
                Name = manager.CatInfo(catId).Name,
                toDoItems = manager.GetBasedOnCat(catId)
            };
         
            return View(vm);
        }
      
    }
}