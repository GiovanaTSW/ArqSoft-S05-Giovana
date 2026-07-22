using System;
using Xunit;
using CitasApp.Domain.Models;

namespace CitasApp.Tests
{
    public class MedicoModelTests
    {
        [Fact]
        public void AsignarNombre_Medico_GuardaCorrectamente()
        {
            // Arrange
            var medico = new Medico();

            // Act
            medico.Nombre = "Dra. Smith";

            // Assert
            Assert.Equal("Dra. Smith", medico.Nombre);
        }
    }
}