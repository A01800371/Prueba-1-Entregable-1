using UnityEngine;

[System.Serializable]
public class RespuestaUsuario
{
    public PreguntaRespuesta pregunta;
    public bool esCorrecta;

    public RespuestaUsuario(PreguntaRespuesta pregunta, bool esCorrecta)
    {
        this.pregunta = pregunta;
        this.esCorrecta = esCorrecta;
    }
}
