﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class MainMenuButtons : MonoBehaviour
    {

        public void LoadScene(String target)
        {
            // TODO make this async so we can load our scene while the main menu is being displayed
            SceneManager.LoadScene(target);
        }

        public void DisplayOptions()
        {
            Debug.Log("Options");
        }

        public void DisplayCredits()
        {
            Debug.Log("Credits");
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
