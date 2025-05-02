using UnityEngine.Localization; 
using System.Collections.Generic; 

[System.Serializable]
public class PreguntaRespuesta
{
    public int dbId;   
    public LocalizedString Pregunta;
    public LocalizedString[] Respuestas; 
    public int RespuestaCorrecta;
}


