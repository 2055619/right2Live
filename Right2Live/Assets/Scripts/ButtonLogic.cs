using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void OnQuitButtonClicked()
    {
        print("Quit button clicked");
        Application.Quit();
    }
}
