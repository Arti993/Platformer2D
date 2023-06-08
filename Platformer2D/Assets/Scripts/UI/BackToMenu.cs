using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class BackToMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        MainScreenMenu.Load();
    }
}
