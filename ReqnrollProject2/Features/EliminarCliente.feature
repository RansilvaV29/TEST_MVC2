Feature: EliminarCliente
  Como usuario del sistema
  Quiero eliminar un cliente de la base de datos
  Para asegurar que los datos no deseados sean removidos

  @delete_cliente
  Scenario: Eliminar un cliente exitosamente
    Given un cliente registrado en la base de datos
      | Cedula    | Apellidos         | Nombres         | FechaNacimiento | Mail              | Telefono   | Direccion | Estado |
      | 173456789 | apellido Generico | Nombre generico | 12/09/2001      | correo@@gmail.com | 0912345679 | Conocoto  | True   |
    When elimino el cliente de la base de datos
    Then el cliente ya no debe existir en la base de datos
