using Microsoft.AspNetCore.Mvc;
using NmapApi.Business;
using NmapApi.Models;

namespace NmapApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NmapController : ControllerBase
{
    private readonly INmapProcessingTasks _nmapProcTasks;

    public NmapController(INmapProcessingTasks nmapProcTasks) =>
        _nmapProcTasks = nmapProcTasks;

    // Can change this to a POST in the future to pass in request object to specify
    // various flags and options for running our nmap command. 
    [HttpGet("SendNmapRequest")]
    public async Task<ActionResult<ApiResponse>> SendNmapRequest(string hostName)
    {
        return await _nmapProcTasks.SendNmapRequest(hostName);
    }

    [HttpGet("SendNmapRequestWithReport")]
    public async Task<ActionResult<ScanReport>> SendNmapRequestWithReport(string hostName)
    {
        return await _nmapProcTasks.SendNmapRequestWithReport(hostName);
    }

    [HttpGet("GetAllNmapResults")]
    public async Task<ActionResult<ApiResponse>> GetAllNmapResults()
    {
        return await _nmapProcTasks.GetAllNmapResults();
    }

    [HttpGet("GetNmapResultsByHostname")]
    public async Task<ActionResult<ApiResponse>> GetNmapResultsByHostname(string hostName)
    {
        return await _nmapProcTasks.GetNmapResultsByHostname(hostName);
    }

    [HttpGet("GetMostRecentNmapResult")]
    public async Task<ActionResult<ApiResponse>> GetMostRecentNmapResult(string hostName)
    {
        return await _nmapProcTasks.GetMostRecentNmapResult(hostName);
    }
}