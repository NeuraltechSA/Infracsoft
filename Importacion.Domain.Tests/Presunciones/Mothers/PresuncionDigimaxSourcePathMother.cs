using Bogus;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Importacion.Domain.Tests.Presunciones.Mothers
{
    public class PresuncionDigimaxSourcePathMother
    {
        public static string Create()
        {
            var faker = new Faker();
            var maxSpeed = $"{faker.Random.Number(60, 100):D3}";
            var measuredSpeed = $"{faker.Random.Number(101, 140):D3},{faker.Random.Number(0, 99)}";
            var date = $"{faker.Date.Past():dd-MM-yyyy}";
            var time = $"{faker.Date.Past():HH.mm.ss}";
            
            return $"/{faker.Company.CompanyName()}/{faker.Date.Past():yyyy/MM/dd}/{faker.Address.FullAddress()}#{date}#{time}#{maxSpeed}#{measuredSpeed}.zip";
        }

        public static string CreateInvalidFormat()
        {
            var faker = new Faker();

            return $"/{faker.Company.CompanyName()}/{faker.Date.Past():yyyy/MM/dd}/{faker.Address.FullAddress()}#{faker.Random.String2(10)}.zip";

        }
    }
}
