using System;
using Xunit;
using CitasApp.Infrastructure.Repositories;

namespace CitasApp.Tests
{
    public class CitaFactoryTests
    {
        [Fact]
        public void TestCrearRepositorioJson_RetornaInstanciaValida()
        {
            // Arrange & Act
            var repo = new JsonCitaRepository("citas.json");

            // Assert
            Assert.NotNull(repo);
        }
    }
}