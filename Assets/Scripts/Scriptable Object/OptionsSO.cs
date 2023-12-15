using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/OptionsScriptableObject", order = 1)]
public class OptionsSO : ScriptableObject
{
    [Range(-40, 20)]
    public float masterVolume = 0f;

    [Range(-40, 20)]
    public float musicVolume = 0f;
    
    [Range(-40, 20)]
    public float soundVolume = 0f;
}