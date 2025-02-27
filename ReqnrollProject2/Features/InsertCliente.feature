Feature: InsertCliente
  Como usuario del sistema
  Quiero registrar un cliente en la base de datos
  Para poder gestionar sus datos correctamente

  @insert_cliente
  Scenario: Insertar un nuevo cliente exitosamente
    Given los siguientes datos del cliente
      | Cedula    | Apellidos         | Nombres         | FechaNacimiento | Mail              | Telefono   | Direccion | Estado |
      | 173456789 | apellido Generico | Nombre generico | 12/09/2001      | correo@@gmail.com | 0912345679 | Conocoto  | True   |
    When intento registrar el cliente en la base de datos
    Then el cliente debe existir en la base de datos con los mismos datos
