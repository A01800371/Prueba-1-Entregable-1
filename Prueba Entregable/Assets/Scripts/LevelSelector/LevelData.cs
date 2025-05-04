/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Este scipt asiste al Selector de niveles al hacer publicos una serie de criterios  |
| que le permiten a un objeto dentro de Unity "MenuManager" Obtener los datos neces- |
| arios para poder visualizar la información de un nivel dentro de algún Selector de |
| niveles.                                                                           |
|                                                                                    |
| Autor: Daniel Díaz Romero                                                          |
\*_________________________________________________________________________________*/

using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]

public class LevelData
{
    public string levelName;
    [SerializeField] public LocalizedString description;
    public Sprite previewImage;
    public string sceneName;
    public string introSceneName;

}
