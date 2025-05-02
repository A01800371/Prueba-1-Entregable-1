using UnityEngine;

[System.Serializable]
public class RespuestaUsuario
{
    public PreguntaRespuesta pregunta;
    public bool esCorrecta;
    public int dbId;

    public RespuestaUsuario(PreguntaRespuesta pregunta, bool esCorrecta)
    {
        this.dbId = pregunta.dbId; // Asignar el ID de la pregunta
        this.pregunta = pregunta;
        this.esCorrecta = esCorrecta;
    }
}
