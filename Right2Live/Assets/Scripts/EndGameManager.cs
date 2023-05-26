using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    private TextMeshProUGUI _guiText;
    public static bool isWin = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        _guiText = GetComponent<TextMeshProUGUI>();
        
        EndGame();
    }
    
    // Choisir le texte à afficher en fonction de la langue
    public void EndGame()
    {
        if (isWin)
        {
            if (LocalizationManager._currentLanguage == LocalizationManager.Languages.English)
            {
                _guiText.text = "Victory";
            }
            else if (LocalizationManager._currentLanguage == LocalizationManager.Languages.French)
            {
                _guiText.text = "Victoire";
            }
        }
        else
        {
            if (LocalizationManager._currentLanguage == LocalizationManager.Languages.English)
            {
                _guiText.text = "Defeat";
            }
            else if (LocalizationManager._currentLanguage == LocalizationManager.Languages.French)
            {
                _guiText.text = "Défaite";
            }
        }
    }
}
