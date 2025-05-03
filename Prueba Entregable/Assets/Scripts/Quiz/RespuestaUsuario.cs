/* 
    *  RespuestaUsuario.cs
    *  Clase que representa la respuesta de un usuario a una pregunta.
    *  Almacena la pregunta, si la respuesta es correcta y el ID de la pregunta en la base de datos.
    * 
    *  Creado por: Brian Axel 
    */
using UnityEngine;

[System.Serializable]
public class RespuestaUsuario
{
    public PreguntaRespuesta pregunta; // Almacena la pregunta
    public bool esCorrecta; // Indica si la respuesta es correcta o no
    public int dbId; // Almacena el ID de la pregunta en la base de datos

    public RespuestaUsuario(PreguntaRespuesta pregunta, bool esCorrecta) // Constrctor de la clase
    {
        this.dbId = pregunta.dbId; // Asignar el ID de la pregunta
        this.pregunta = pregunta; // Asignar la pregunta
        this.esCorrecta = esCorrecta; // Asignar si la respuesta es correcta o no
    }
}
