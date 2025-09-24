using Bogus;

namespace Importacion.Domain.Tests.Presunciones.Mothers;

public class PresuncionDigimaxCompressedFilePasswordMother
{
    public static string Create()
    {
        return new Faker().Random.String2(10);
    }
}
