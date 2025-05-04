/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Este es un script sencillo que permite la carga de un Quiz al presionar un botón.  |
| Se buscará el quiz en el Proyecto y dependiendo del ID asignado se cargará dicha   |
| escena con el quiz. Este script depende completamente de que todos los Quizes      |
| contengan un número que le identifique (ID), justo como está implementado en el    |
| proyecto.                                                                          |
|                                                                                    |
| Autor: Daniel Díaz Romero                                                          |
\*_________________________________________________________________________________*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void OpenQuiz(int quizId)
    {
        string quizName = "Quiz " + quizId;
        SceneManager.LoadScene(quizName);
    }
}
