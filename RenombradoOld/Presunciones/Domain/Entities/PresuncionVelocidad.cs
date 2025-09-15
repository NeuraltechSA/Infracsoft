using System.Collections.Generic;
using RenombradoOld.Presunciones.Domain.Events.Velocidad;
using RenombradoOld.Presunciones.Domain.ValueObjects.Velocidad;

namespace RenombradoOld.Presunciones.Domain.Entities;

public sealed class PresuncionVelocidad : Presuncion
{
    public PresuncionVelocidadMedida VelocidadMedida { get; private set; }
    public PresuncionVelocidadMaxima VelocidadMaxima { get; private set; }
    public PresuncionCarril Carril { get; private set; }

    public PresuncionVelocidad(
        string id,
        DateTime? fechaHora,
        string lugar,
        string patente,
        float velocidadMedida,
        float velocidadMaxima,
        int carril) : base(id, fechaHora, lugar, patente)
    {
        VelocidadMaxima = new PresuncionVelocidadMaxima(velocidadMaxima);
        VelocidadMedida = new PresuncionVelocidadMedida(velocidadMedida, velocidadMaxima);
        Carril = new PresuncionCarril(carril);
    }

    public static PresuncionVelocidad Create(
        string id,
        DateTime? fechaHora,
        string lugar,
        string patente,
        float velocidadMedida,
        float velocidadMaxima,
        int carril
    )
    {
        var presuncion = new PresuncionVelocidad(id, fechaHora, lugar, patente, velocidadMedida, velocidadMaxima, carril);
        presuncion.RecordDomainEvent(
            new PresuncionVelocidadCreatedDomainEvent
            {
                PresuncionId = id,
                FechaHora = fechaHora,
                Lugar = lugar,
                Patente = patente,
                VelocidadMedida = velocidadMedida,
                VelocidadMaxima = velocidadMaxima,
                Carril = carril
            });
        return presuncion;
    }

    public void Update(
        DateTime? fechaHora,
        string lugar,
        string patente,
        float velocidadMedida,
        float velocidadMaxima,
        int carril
    )
    {
        Update(fechaHora, lugar, patente);
        VelocidadMaxima = new PresuncionVelocidadMaxima(velocidadMaxima);
        VelocidadMedida = new PresuncionVelocidadMedida(velocidadMedida, velocidadMaxima);
        Carril = new PresuncionCarril(carril);
        RecordDomainEvent(
            new PresuncionVelocidadUpdatedDomainEvent
            {
                PresuncionId = Id.Value,
                FechaHora = fechaHora,
                Lugar = lugar,
                Patente = patente,
                VelocidadMedida = velocidadMedida,
                VelocidadMaxima = velocidadMaxima,
                Carril = carril
            });
    }

    public override void Delete()
    {
        RecordDomainEvent(
            new PresuncionVelocidadDeletedDomainEvent
            {
                PresuncionId = Id.Value,
                FechaHora = FechaHora?.Value,
                Lugar = Lugar.Value,
                Patente = Patente.Value,
                VelocidadMedida = VelocidadMedida.Value,
                VelocidadMaxima = VelocidadMaxima.Value,
                Carril = Carril.Value
            });
    }


}