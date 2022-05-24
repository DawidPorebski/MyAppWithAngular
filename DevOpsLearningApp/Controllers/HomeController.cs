using AutoFixture;
using DevOpsLearningApp.Database;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsLearningApp.Controllers;

public class HomeController : Controller
{
    private readonly MyAppContext _context;
    private readonly Fixture _fixture;

    public HomeController(MyAppContext context)
    {
        _context = context;
        _fixture = new Fixture();
    }

    public IActionResult Index()
    {
        var rows = _context.MyTable
            .OrderByDescending(m => m.Id)
            .Take(10)
            .ToList();

        return View(rows);
    }

    public IActionResult AddRow()
    {
        var row = new MyEntity
        {
            Text = _fixture.Create<string>()
        };

        _context.Add(row);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult RemoveAllRows()
    {
        var rows = _context.MyTable.ToList();
        _context.MyTable.RemoveRange(rows);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}