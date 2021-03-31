using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BugTracker.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IssueContext _context;

        public IssuesController(IssueContext context)
        {
            _context = context;
        }

    // GET: api/IssueItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IssueItemDTO>>> GetIssueItems()
    {
        return await _context.IssueItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueItemDTO>> GetIssueItem(long id)
    {
        var issueItem = await _context.IssueItems.FindAsync(id);

        if (issueItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(issueItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIssueItem(long id, IssueItemDTO issueItemDTO)
    {
        if (id != issueItemDTO.Id)
        {
            return BadRequest();
        }

        var issueItem = await _context.IssueItems.FindAsync(id);
        if (issueItem == null)
        {
            return NotFound();
        }

        issueItem.CardName = issueItemDTO.CardName;
        issueItem.IssueText = issueItemDTO.IssueText;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!IssueItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<IssueItemDTO>> CreateIssueItem(IssueItemDTO issueItemDTO)
    {
        var issueItem = new IssueItem
        {
            IssueText = issueItemDTO.IssueText,
            CardName = issueItemDTO.CardName
        };

        _context.IssueItems.Add(issueItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetIssueItem),
            new { id = issueItem.Id },
            ItemToDTO(issueItem));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIssueItem(long id)
    {
        var issueItem = await _context.IssueItems.FindAsync(id);

        if (issueItem == null)
        {
            return NotFound();
        }

        _context.IssueItems.Remove(issueItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool IssueItemExists(long id) =>
        _context.IssueItems.Any(e => e.Id == id);

    private static IssueItemDTO ItemToDTO(IssueItem issueItem) =>
        new IssueItemDTO
        {
            Id = issueItem.Id,
            CardName = issueItem.CardName,
            IssueText = issueItem.IssueText
        };
    }
}




// namespace BugTracker.Contollers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class IssuesController : ControllerBase
//     {
//         static void Main(string[] args)
//         {
//             // DO NOT COMMIT UNTIL MOVE PASSSWORD TO SECRETS: 3y6MuZCQ-ySbmj!
//             MongoClient dbClient = new MongoClient("mongodb+srv://user:3y6MuZCQ-ySbmj!@bugtrackerdb.zy5oo.mongodb.net/?retryWrites=true&w=majority");

//             var dbList = dbClient.ListDatabases().ToList();

//             Console.WriteLine("The list of databases on this server is: ");
//             foreach (var db in dbList)
//             {
//                 Console.WriteLine(db);
//             }
//         }
//     }
// }

// MongoClient dbClient = new MongoClient("mongodb+srv://user:3y6MuZCQ-ySbmj!@bugtrackerdb.zy5oo.mongodb.net/?retryWrites=true&w=majority");

// var dbList = dbClient.ListDatabases().ToList();

// Console.WriteLine("The list of databases on this server is: ");
// foreach (var db in dbList)
// {
//     Console.WriteLine(db);
// }