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

    /// <summary>
    /// Sends a hostname/IP address to be searched for valid open ports.
    /// </summary>
    /// <param name="hostName">The hostname or IP address used for nmap scans</param>
    /// <returns>
    /// Returns an api response with list of nmap scan results if there are any. Returns non-successful response object
    /// if there are errors or invalid requests made.
    /// </returns>
    [HttpGet("SendNmapRequest")]
    public async Task<ActionResult<ApiResponse>> SendNmapRequest(string hostName)
    {
        // Can change this to a POST in the future to pass in request object to specify
        // various flags and options for running our nmap command. 
        return await _nmapProcTasks.SendNmapRequest(hostName);
    }

    /// <summary>
    /// Sends a hostname/IP address to be searched for valid open ports. Compares most recent Nmap scan to 
    /// current to discover if any new ports have opened or closed.
    /// </summary>
    /// <param name="hostName">The hostname or IP address used for nmap probing</param>
    /// <returns>
    /// Returns the most recent scan history including newly opened/closed ports.
    /// </returns>
    [HttpGet("SendNmapRequestWithHistoryReport")]
    public async Task<ActionResult<ScanReport>> SendNmapRequestWithHistoryReport(string hostName)
    {
        return await _nmapProcTasks.SendNmapRequestWithReport(hostName);
    }

    /// <summary>
    /// Gets all successful nmap scans.
    /// </summary>
    /// <returns>
    /// Returns an api response with list of all nmap scan results. Returns non-successful response object
    /// if there are errors.
    /// </returns>
    [HttpGet("GetAllNmapResults")]
    public async Task<ActionResult<ApiResponse>> GetAllNmapResults()
    {
        return await _nmapProcTasks.GetAllNmapResults();
    }

    /// <summary>
    /// Gets all successful nmap scans sent for a specific hostname/IP address
    /// </summary>
    /// <param name="hostName">The hostname or IP address used for nmap scans</param>
    /// <returns>
    /// Returns an api response with list of all nmap scan results filtered by provided hostname. Returns 
    /// non-successful response object if there are errors.
    /// </returns>
    [HttpGet("GetNmapResultsByHostname")]
    public async Task<ActionResult<ApiResponse>> GetNmapResultsByHostname(string hostName)
    {
        return await _nmapProcTasks.GetNmapResultsByHostname(hostName);
    }

    /// <summary>
    /// Gets the most recently saved nmap result for a specified hostname.
    /// </summary>
    /// <param name="hostName">The hostname or IP address used for nmap scans</param>
    /// <returns>
    /// Returns an api response with the nmap result for specified hostname. Returns 
    /// non-successful response object if there are errors.
    /// </returns>
    [HttpGet("GetMostRecentNmapResult")]
    public async Task<ActionResult<ApiResponse>> GetMostRecentNmapResult(string hostName)
    {
        return await _nmapProcTasks.GetMostRecentNmapResult(hostName);
    }
}