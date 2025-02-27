using System;
using TechTalk.SpecFlow;
using TEST_MVC2.Data;
using TEST_MVC2.Models;
using FluentAssertions;

namespace ReqnrollProject2.StepDefinitions
{
    [Reqnroll.Binding]
    public sealed class ClienteInsertStepDefinitions
    {
        private readonly ClienteDataAccessLayer _clienteDAL;
        private Cliente _cliente;
        private int _resultado;

        public ClienteInsertStepDefinitions()
        {
            _clienteDAL = new ClienteDataAccessLayer(); // Capa de acceso a datos
        }

        [Reqnroll.Given("los siguientes datos del cliente")]
        public void GivenLosSiguientesDatosDelCliente(Reqnroll.Table table)
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
        }

        [Reqnroll.When("intento registrar el cliente en la base de datos")]
        public void WhenIntentoRegistrarElClienteEnLaBaseDeDatos()
        {
            _resultado = _clienteDAL.InsertCliente(_cliente);
        }

        [Reqnroll.Then("el cliente debe existir en la base de datos con los mismos datos")]
        public void ThenElClienteDebeExistirEnLaBaseDeDatosConLosMismosDatos()
        {
            var clienteBD = _clienteDAL.GetClienteById(_resultado);
            clienteBD.Should().NotBeNull();
            clienteBD.Cedula.Should().Be(_cliente.Cedula);
            clienteBD.Apellidos.Should().Be(_cliente.Apellidos);
            clienteBD.Nombres.Should().Be(_cliente.Nombres);
            clienteBD.FechaNacimiento.Should().Be(_cliente.FechaNacimiento);
            clienteBD.Mail.Should().Be(_cliente.Mail);
            clienteBD.Telefono.Should().Be(_cliente.Telefono);
            clienteBD.Direccion.Should().Be(_cliente.Direccion);
            clienteBD.Estado.Should().Be(_cliente.Estado);
        }
    }
}
