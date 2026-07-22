using System;
using Xunit;
using CitasApp.Infrastructure.Repositories;

namespace CitasApp.Tests
{
    public class CsvCitaRepositoryTests
    {
        [Fact]
        public void TestCrearCsvCitaRepository_RetornaInstanciaValida()
        {
            // Arrange & Act
            var repo = new CsvCitaRepository("citas.csv");

            // Assert
            Assert.NotNull(repo);
        }
    }
}