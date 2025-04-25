/*
    * Autor: Daniel Díaz
*/

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
