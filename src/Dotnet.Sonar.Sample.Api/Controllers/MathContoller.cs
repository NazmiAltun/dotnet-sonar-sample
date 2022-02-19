using Microsoft.AspNetCore.Mvc; 
using Dotnet.Sonar.Sample.Services;

namespace Dotnet.Sonar.Sample.Api;

[ApiController]
public class MathController : ControllerBase
{
    private readonly IMathService _mathService;

    public MathController(IMathService mathService)
    {
        _mathService = mathService;
    }

    [HttpGet("/api/math/add/{a}/{b}")]
    public IActionResult Add(int a, int b)
    {
        return Ok(_mathService.Add(a, b));
    }
}
