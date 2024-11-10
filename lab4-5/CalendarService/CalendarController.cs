[ApiController]
[Route("api/[controller]")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddEvent([FromBody] Event evt)
    {
        await _calendarService.AddEventAsync(evt);
        return Ok("Event added");
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveEvent([FromBody] Event evt)
    {
        await _calendarService.RemoveEventAsync(evt);
        return Ok("Event removed");
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncEvents()
    {
        await _calendarService.SyncExternalEventsAsync();
        return Ok("Events synced");
    }
}

[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
    private readonly ContentManagementService _contentManagementService;

    public ContentController(ContentManagementService contentManagementService)
    {
        _contentManagementService = contentManagementService;
    }

    [HttpPost("create")]
    public IActionResult CreateContent([FromBody] Event evt)
    {
        var content = _contentManagementService.CreateContent("event", evt);
        content.Display();
        return Ok("Content created");
    }
}
