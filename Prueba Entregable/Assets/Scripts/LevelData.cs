/*
    * Autor: Daniel Díaz
*/

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
