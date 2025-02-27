using System;
using TechTalk.SpecFlow;
using TEST_MVC2.Data;
using TEST_MVC2.Models;
using FluentAssertions;

namespace ReqnrollProject2.StepDefinitions
{
    [Reqnroll.Binding]
    public sealed class ClienteEditStepDefinitions
    {
        private readonly ClienteDataAccessLayer _clienteDAL;
        private Cliente _cliente;
        private int _clienteId;
        private Cliente _clienteEditado;

        public ClienteEditStepDefinitions()
        {
            _clienteDAL = new ClienteDataAccessLayer(); // Capa de acceso a datos
        }

        [Reqnroll.Given("un cliente existente en la base de datos")]
        public void GivenUnClienteExistenteEnLaBaseDeDatos(Reqnroll.Table table)
        {
            var row = table.Rows[0]; // Tomar la primera fila de datos
            _cliente = new Cliente
            {
                Cedula = row["Cedula"],
                Apellidos = row["Apellidos"],
                Nombres = row["Nombres"],
                FechaNacimiento = DateTime.Parse(row["FechaNacimiento"]),
                Mail = row["Mail"],
                Telefono = row["Telefono"],
                Direccion = row["Direccion"],
                Estado = bool.Parse(row["Estado"])
            };

            // Insertar el cliente en la BD y guardar el ID generado
            _clienteId = _clienteDAL.InsertCliente(_cliente);
        }

        [Reqnroll.When("modifico los datos del cliente")]
        public void WhenModificoLosDatosDelCliente(Reqnroll.Table table)
        {
            var row = table.Rows[0]; // Tomar la fila de datos nuevos
            _clienteEditado = new Cliente
            {
                Codigo = _clienteId, // Se usa el mismo ID
                Cedula = row["Cedula"],
                Apellidos = row["Apellidos"],
                Nombres = row["Nombres"],
                FechaNacimiento = DateTime.Parse(row["FechaNacimiento"]),
                Mail = row["Mail"],
                Telefono = row["Telefono"],
                Direccion = row["Direccion"],
                Estado = bool.Parse(row["Estado"])
            };

            _clienteDAL.UpdateCliente(_clienteEditado);
        }

        [Reqnroll.Then("los datos del cliente deben haberse actualizado en la base de datos")]
        public void ThenLosDatosDelClienteDebenHaberseActualizadoEnLaBaseDeDatos()
        {
            var clienteBD = _clienteDAL.GetClienteById(_clienteId);
            clienteBD.Should().NotBeNull();
            clienteBD.Cedula.Should().Be(_clienteEditado.Cedula);
            clienteBD.Apellidos.Should().Be(_clienteEditado.Apellidos);
            clienteBD.Nombres.Should().Be(_clienteEditado.Nombres);
            clienteBD.FechaNacimiento.Should().Be(_clienteEditado.FechaNacimiento);
            clienteBD.Mail.Should().Be(_clienteEditado.Mail);
            clienteBD.Telefono.Should().Be(_clienteEditado.Telefono);
            clienteBD.Direccion.Should().Be(_clienteEditado.Direccion);
            clienteBD.Estado.Should().Be(_clienteEditado.Estado);
        }
    }
}
