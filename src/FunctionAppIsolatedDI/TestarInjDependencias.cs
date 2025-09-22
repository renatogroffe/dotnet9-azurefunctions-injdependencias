using FunctionAppIsolatedDI.Implementations;
using FunctionAppIsolatedDI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppIsolatedDI;

public class TestarInjDependencias
{
    private readonly ILogger<TestarInjDependencias> _logger;
    private readonly TesteInjecao _objTesteInjecao;
    private readonly ITesteA _testeA;
    private readonly ITesteB _testeB;
    private readonly TesteC _testeC;

    public TestarInjDependencias(ILoggerFactory loggerFactory,
        TesteInjecao objTesteInjecao,
        ITesteA testeA,
        ITesteB testeB,
        TesteC testeC)
    {
        _logger = loggerFactory.CreateLogger<TestarInjDependencias>();
        _objTesteInjecao = objTesteInjecao;
        _testeA = testeA;
        _testeB = testeB;
        _testeC = testeC;
    }

    [Function(nameof(TestarInjDependencias))]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        _logger.LogInformation(
            $"C# HTTP trigger: executando o método Run da Function {nameof(TestarInjDependencias)}...");
        var headers = string.Join("; " + Environment.NewLine,
            req.Headers.Select(h => $"{h.Key}: {h.Value}"));
        _logger.LogInformation("Request Headers - " +
            $"{nameof(TestarInjDependencias)}:{Environment.NewLine}{headers}");

        return new OkObjectResult(_objTesteInjecao
            .RetornarValoresInjecao(_testeA, _testeB, _testeC));
    }
}