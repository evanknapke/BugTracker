using Microsoft.AspNetCore.Mvc;
using BugTracker.Models;
using BugTracker.Services;

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IssueService _issueService;

        public IssuesController(IssueService issueService)
        {
            _issueService = issueService;
        }

        // GET: api/IssueItems
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<IssueItemDTO>>> GetIssueItems()
        // {
        //     return await _context.IssueItems
        //         .Select(x => ItemToDTO(x))
        //         .ToListAsync();
        // }

        [HttpGet("{id:length(24)}", Name = "GetIssue")]
        public ActionResult<IssueItem> Get(string id)
        {
            var issueItem = _issueService.Get(id);

            if (issueItem == null)
            {
                return NotFound();
            }

            return issueItem;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, IssueItem issueItemIn)
        {
            var issueItem = _issueService.Get(id);
            if (issueItem == null)
            {
                return NotFound();
            }

            _issueService.Update(id, issueItemIn);

            return NoContent();
        }

        [HttpPost]
        public ActionResult<IssueItem> Create(IssueItem issueItem)
        {
            _issueService.Create(issueItem);
            
            return CreatedAtRoute(
                "GetIssue",
                new { id = issueItem.Id.ToString() },
                issueItem);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var issueItem = _issueService.Get(id);

            if (issueItem == null)
            {
                return NotFound();
            }

            _issueService.Remove(issueItem.Id);

            return NoContent();
        }
    }
}