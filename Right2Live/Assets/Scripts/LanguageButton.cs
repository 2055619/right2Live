using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choixFrancais() {
        LocalizationManager._currentLanguage = LocalizationManager.Languages.French;
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void choixAnglais() {
        LocalizationManager._currentLanguage = LocalizationManager.Languages.English;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
