/* 
 * PreguntaRespuesta.cs
 * 
 * Este script define la clase PreguntaRespuesta, que se utiliza para almacenar información sobre preguntas y respuestas en un juego.
 * 
 * Autor: Brian Axel y Erick Owen
 * 
 * Descripción:
 * La clase PreguntaRespuesta contiene un índice de base de datos, una pregunta localizada, un array de respuestas localizadas y el índice de la respuesta correcta.
 * 
 */

using UnityEngine.Localization; 
using System.Collections.Generic; 

[System.Serializable]
public class PreguntaRespuesta
{
    public int dbId;   // Index de la pregunta en la base de datos
    public LocalizedString Pregunta; // Pregunta localizada
    public LocalizedString[] Respuestas;  // Respuestas localizadas
    public int RespuestaCorrecta; // Índice de la respuesta correcta
}


