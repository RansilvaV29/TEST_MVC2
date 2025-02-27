Feature: EditarCliente
  Como usuario del sistema
  Quiero modificar los datos de un cliente en la base de datos
  Para mantener la información actualizada

  @edit_cliente
  Scenario: Editar un cliente exitosamente
    Given un cliente existente en la base de datos
      | Cedula    | Apellidos         | Nombres         | FechaNacimiento | Mail              | Telefono   | Direccion | Estado |
      | 173456789 | apellido Generico | Nombre generico | 12/09/2001      | correo@@gmail.com | 0912345679 | Conocoto  | True   |
    When modifico los datos del cliente
      | Cedula    | Apellidos      | Nombres | FechaNacimiento | Mail                | Telefono   | Direccion    | Estado |
      | 172323232 | Generico Perez | Pepito  | 01/01/2000      | generico2@gmail.com | 0999999999 | Quito Centro | False  |
    Then los datos del cliente deben haberse actualizado en la base de datos
