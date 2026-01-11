using Microsoft.AspNetCore.Mvc;
using NmapApi.Models;
using NmapApi.Services;

namespace NmapApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NmapController : ControllerBase
{
    private readonly NmapService _nmapService;

    public NmapController(NmapService nmapService) =>
        _nmapService = nmapService;

    [HttpGet("SendNmapResonse")]
    public async Task<ActionResult<List<NmapResult>>> SendNmapResonse(string hostName)
    {
        var res = NmapHelper.RunNmapProcess(hostName);

        await _nmapService.CreateAsync(res);

        return res;
    }

    [HttpGet("GetNmapResonses")]
    public async Task<ActionResult<List<NmapResult>>> GetNmapResonses()
    {
        return await _nmapService.GetAsync();
    }
}