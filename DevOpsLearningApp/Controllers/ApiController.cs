using AutoFixture;
using DevOpsLearningApp.Database;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsLearningApp.Controllers;

[Route("api")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly MyAppContext _context;
    private readonly Fixture _fixture;

    public ApiController(MyAppContext context)
    {
        _context = context;
        _fixture = new Fixture();
    }

    [HttpGet("rows")]
    public IActionResult GetRows()
    {
        var rows = _context.MyTable
            .OrderByDescending(m => m.Id)
            .Take(10)
            .ToList();

        return Ok(rows);
    }

    [HttpPost("add-row")]
    public IActionResult AddRow()
    {
        var row = new MyEntity
        {
            Text = _fixture.Create<string>()
        };

        _context.Add(row);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("remove-all-rows")]
    public IActionResult RemoveAllRows()
    {
        var rows = _context.MyTable.ToList();
        _context.MyTable.RemoveRange(rows);
        _context.SaveChanges();

        return Ok();
    }
}
