[System.Serializable]

public class PreguntaRespuesta
{
    public string Pregunta; // Pregunta a mostrar en la UI

    public string[] Respuestas; // Respuestas posibles a la pregunta

    public int RespuestaCorrecta; // Índice de la respuesta correcta (1, 2, 3 o 4)
}
