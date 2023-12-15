using System.Linq;
using UnityEngine;

public static class Util
{
    public static void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        Application.Quit(0);
    }

    public static void SetResolutionFromString(string resolutionString)
    {
        int[] resolution = new int[2];
        var res = resolutionString.Split('x');

        if (res.Length > 2)
        {
            Debug.LogError($"Resolution string split into '{res.Length}', not 2.");
            return;
        }

        if (!int.TryParse(res[0], out resolution[0]) || !int.TryParse(res[1], out resolution[1]))
        {
            string test = "";

            res.ToList().ForEach(x => test += x + " ");

            Debug.LogError($"Could not parse resolution string -- '{test}'");
            return;
        }
        
        Screen.SetResolution(resolution[0], resolution[1], Screen.fullScreen);
        PlayerPrefs.SetString("Resolution", resolutionString);
    }

    public static void SetQualityFromString(string qualityString)
    {
        int qual = QualitySettings.names.ToList().IndexOf(qualityString);

        if (qual == -1)
        {
            Debug.LogError($"'{qualityString}', not in QualitySetting names list");
            return;
        }
        
        QualitySettings.SetQualityLevel(qual);

        PlayerPrefs.SetString("Quality", QualitySettings.names[qual]);
    }
}
