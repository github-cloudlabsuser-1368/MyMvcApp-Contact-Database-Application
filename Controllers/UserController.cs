using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Linq;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index()
    {
        // Return the list of users to the view
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Find the user by id
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Return the Create view
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }
        // Assign a new Id
        user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
        userlist.Add(user);
        return RedirectToAction("Index");
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Retrieve the user to edit
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        return RedirectToAction("Index");
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("DeleteConfirmed")]
    public ActionResult DeleteConfirmed(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            userlist.Remove(user);
        }
        return RedirectToAction("Index");
    }

    // GET: User/Search
    public ActionResult Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return View("Index", userlist);
        }
        var results = userlist
            .Where(u => (!string.IsNullOrEmpty(u.Name) && u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(query, StringComparison.OrdinalIgnoreCase)))
            .ToList();
        return View("Index", results);
    }
}
