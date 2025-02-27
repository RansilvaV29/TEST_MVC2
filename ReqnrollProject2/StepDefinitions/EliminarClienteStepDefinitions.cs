using System;
using TechTalk.SpecFlow;
using TEST_MVC2.Data;
using TEST_MVC2.Models;
using FluentAssertions;

namespace ReqnrollProject2.StepDefinitions
{
    [Reqnroll.Binding]
    public sealed class ClienteDeleteStepDefinitions
    {
        private readonly ClienteDataAccessLayer _clienteDAL;
        private Cliente _cliente;
        private int _clienteId;

        public ClienteDeleteStepDefinitions()
        {
            _clienteDAL = new ClienteDataAccessLayer(); // Capa de acceso a datos
        }

        [Reqnroll.Given("un cliente registrado en la base de datos")]
        public void GivenUnClienteRegistradoEnLaBaseDeDatos(Reqnroll.Table table)
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

        [Reqnroll.When("elimino el cliente de la base de datos")]
        public void WhenEliminoElClienteDeLaBaseDeDatos()
        {
            _clienteDAL.DeleteCliente(_clienteId);
        }

        [Reqnroll.Then("el cliente ya no debe existir en la base de datos")]
        public void ThenElClienteYaNoDebeExistirEnLaBaseDeDatos()
        {
            var clienteBD = _clienteDAL.GetClienteById(_clienteId);
            clienteBD.Should().BeNull("porque el cliente ha sido eliminado de la base de datos");
        }
    }
}
