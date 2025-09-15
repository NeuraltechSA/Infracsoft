using MassTransit;
using Microsoft.Extensions.Logging;
using RenombradoOld.Fuentes.Domain.Events;
using SharedKernel.Domain.Contracts;
namespace RenombradoOld.Fuentes.Application.CreateFuenteFtp;

public class FuenteFtpCreatedEventConsumer : IConsumer<FuenteFtpCreatedDomainEvent>
{
    private readonly ILogger<FuenteFtpCreatedDomainEvent> _logger;
    public FuenteFtpCreatedEventConsumer(ILogger<FuenteFtpCreatedDomainEvent> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<FuenteFtpCreatedDomainEvent> context)
    {
        _logger.LogInformation("Fuente FTP creada: {FuenteId}", context.Message.FuenteId);
        return Task.CompletedTask;
    }
}
