
Issue: M�todo PUT de Machine lanza error 500 � Refactor necesario
 Descripci�n del error:

Actualmente, al intentar ejecutar el m�todo PUT de Machine (PUT /api/Machines/{id}), la API responde con un error 500 (Internal Server Error). Este error indica una excepci�n no controlada en el servidor.

 Causa probable:
El m�todo intenta actualizar toda la entidad Machine directamente con EntityState.Modified, pero el objeto recibido incluye propiedades no mapeadas o relaciones como Components que generan conflictos con EF Core.

Esto puede suceder por:

Diferencias entre los campos enviados en el JSON y los esperados en la entidad.

Serializaci�n de propiedades de navegaci�n (Components) que no deben actualizarse desde este endpoint.

El Machine recibido no est� completamente trackeado por el contexto, y eso provoca conflictos de tracking.

 Tareas para resolverlo:
	En MachinesController.cs:
	Reescribir el m�todo PUT para que:

Busque la entidad original desde la base (FindAsync(id)).

Actualice solo las propiedades necesarias una por una.

Evite sobrescribir colecciones como Components.

Devuelva NotFound() si la m�quina no existe.

Devuelva NoContent() si se actualiza correctamente.

En Machine.cs:
 Asegurarse de que las propiedades de navegaci�n (Components) est�n excluidas del modelo recibido en el PUT.

 Considerar marcar la colecci�n como virtual si se usa lazy loading.

Crear un DTO (MachineDto) para PUT:
 Crear una clase MachineDto que contenga solo las propiedades simples de la entidad.

 Usar MachineDto como par�metro del m�todo PUT para controlar los datos que se reciben.

