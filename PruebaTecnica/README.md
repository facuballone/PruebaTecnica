
Issue: Método PUT de Machine lanza error 500 – Refactor necesario
 Descripción del error:

Actualmente, al intentar ejecutar el método PUT de Machine (PUT /api/Machines/{id}), la API responde con un error 500 (Internal Server Error). Este error indica una excepción no controlada en el servidor.

 Causa probable:
El método intenta actualizar toda la entidad Machine directamente con EntityState.Modified, pero el objeto recibido incluye propiedades no mapeadas o relaciones como Components que generan conflictos con EF Core.

Esto puede suceder por:

Diferencias entre los campos enviados en el JSON y los esperados en la entidad.

Serialización de propiedades de navegación (Components) que no deben actualizarse desde este endpoint.

El Machine recibido no está completamente trackeado por el contexto, y eso provoca conflictos de tracking.

 Tareas para resolverlo:
	En MachinesController.cs:
	Reescribir el método PUT para que:

Busque la entidad original desde la base (FindAsync(id)).

Actualice solo las propiedades necesarias una por una.

Evite sobrescribir colecciones como Components.

Devuelva NotFound() si la máquina no existe.

Devuelva NoContent() si se actualiza correctamente.

En Machine.cs:
 Asegurarse de que las propiedades de navegación (Components) estén excluidas del modelo recibido en el PUT.

 Considerar marcar la colección como virtual si se usa lazy loading.

Crear un DTO (MachineDto) para PUT:
 Crear una clase MachineDto que contenga solo las propiedades simples de la entidad.

 Usar MachineDto como parámetro del método PUT para controlar los datos que se reciben.

