using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLIArgs : MonoBehaviour
{
    private void Awake()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        int quality = 0;

        for(byte i = 0; i < args.Length; i++)
        {
            if (args[i] == "--quality")
            {
                quality = System.Convert.ToInt32(args[i + 1]);
            }
        }

        QualitySettings.SetQualityLevel(quality);
    }
}
