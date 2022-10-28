# gsNotas

Gestionar notas y grupos de notas con editor integrado.


>Con fecha 20-oct-22 19.30 he creado esta nueva versión del proyecto usando .NET 6 para aplicación de escritorio con WindowsForms.
>
>Esta sustituye a la versión anterior (**[gsNotasNETF](https://github.com/elGuille-info/gsNotasNETF)**) que usa .NET Framework 4.8.1.

<br>

**Novedades en el diseño de la aplicación y directorios de datos:**

- Los datos de la configuración y de las notas así como las copias de las notas, se guardan en la carpeta ***gsNotas*** dentro de ***%LOCALAPPDATA%*** (C:\\[Usuario]\AppData\Local\gsNotas).

- **En la ficha Opciones** se puede personalizar los colores de los grupos de notas y guardarlos.
  - Esos colores se recuerdan al volver a abrir la aplicación).
    - Los colores se guardan en _ColoresGrupos.json_.

  - Hay 4 grupos de colores diferentes, además de uno aleatorio que siempre asignará los colores de forma aleatoria al iniciarse el programa.<br>
   <br>
   <img width="557" alt="Screenshot 2022-10-27 200306" src="https://user-images.githubusercontent.com/71171321/198365131-6acd3bf6-c71b-43fa-824f-98677b23323f.png">

- **Mediante el botón 'expander' (arriba a la derecha)** se ajusta el tamaño del panel inferior al ocultar el superior.
  - Aunque el panel superior esté oculto, al pulsar en una de las fichas (que siempre estarán visibles) se muestra el panel.

- Se puede pegar texto con el formato usado al guardar las notas y sustituirlo por los caracteres correspondientes.
  - Tecla rápida (Ctrl+Alt+V).

- Al modificar una nota y cambiar de nota o de grupo se sustituye el texto de esa nota (antes se agregaba una nueva nota).
  - Al pulsar F8 no se sustituye la nota actual con el nuevo texto (se añade una nota nueva, pero sin sustituir el texto anterior).
  - Para sustituir el texto de una nota, pulsa Shift+F8 (mayúsculas + F8).

## TODO

Cosillas que me gustaría añadir.

  - Acoplar la ventana a la izquierda o la derecha.
  - Mostrar las notas en una sola fila.
  - Cambiar el alto de los grupos y poder mostrarlos en una fila.
  - Cambiar los colores de los temas claro y oscuro (letra y fondo) en tiempo de ejecución (ahora se puede hacer en tiempo de diseño).
  - Añadir las notas (o las seleccionadas) a Keep de Google.
  - Ídem a Google Drive (las seleccionadas, no todas).
  - Cargar las notas de una de las copias/backup pero no usando importar (con formato NotaUC), si no, para reemplazar el contenido actual.
