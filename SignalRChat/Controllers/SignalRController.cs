using Microsoft.AspNetCore.Mvc;
using SignalRChat.Client.Features.SignalR;

namespace SignalRChat.Controllers;

[ApiController]
[Route("[controller]")]
public class SignalRController : ControllerBase
{
    private readonly SignalRClient _signalRClient;

    public SignalRController(SignalRClient signalRClient)
    {
        _signalRClient = signalRClient;
    }

    [HttpGet("StartHub")]
    public async Task<IActionResult> StartHub()
    {
        if (!_signalRClient.IsConnected)
            await _signalRClient.Start();

        return Ok();
    }

    [HttpGet("Send")]
    public async Task<IActionResult> Send()
    {
        if (_signalRClient.IsConnected)
            await _signalRClient.Send();

        else return BadRequest("La conexión está cerrada");

        return Ok();
    }
}

