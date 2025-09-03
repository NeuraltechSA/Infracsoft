using Renombrado.Fuentes.Domain.Contracts;
using Renombrado.Fuentes.Domain.Entities;
using SharedKernel.Domain.Contracts;
using Renombrado.Fuentes.Domain.Services;
using Renombrado.Fuentes.Domain.ValueObjects;
using Renombrado.Fuentes.Domain.Exceptions;
using Renombrado.Fuentes.Domain.Events;
using System.Collections.Generic;

namespace Renombrado.Fuentes.Application.CreateFuenteFtp
{
    public class CreateFuenteFtpUseCase
    {
        private readonly IFuenteRepository _fuenteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public CreateFuenteFtpUseCase(IFuenteRepository fuenteRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _fuenteRepository = fuenteRepository;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public async Task Execute(CreateFuenteFtpDTO request)
        {
            var fuente = FuenteFtp.Create(request.Id, request.Nombre, request.Descripcion, request.Host, request.Port, request.Username, request.Password);
            await _fuenteRepository.Create(fuente);
            await _eventBus.Publish(fuente.PullDomainEvents());
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
