using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] double respawnDelay = 0.0;
    [SerializeField] int nbZombieTotal = 5;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playerHead;
    [SerializeField] GameObject player;
    [SerializeField] GameObject HUD;
    [SerializeField] List<GameObject> firstTriggerSpawnpoints;
    [SerializeField] List<GameObject> secondTriggerSpawnpoints;
    double currentTimer;
    private bool isShowing = false;

    private FPSLogic fpsLogic;
    private ShootingLogic shootingLogic;
    // Start is called before the first frame update
    void Start()
    {
        fpsLogic = playerHead.GetComponent<FPSLogic>();
        shootingLogic = playerHead.GetComponentInChildren<ShootingLogic>();
        pauseMenu.SetActive(isShowing);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            changeMenuState();
        }
        winCondition();
    }

    private void changeMenuState() {
            isShowing = !isShowing;
            if (isShowing) {
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
                fpsLogic.setIsPaused(true);
                shootingLogic.setIsPaused(true);
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                fpsLogic.setIsPaused(false);
                shootingLogic.setIsPaused(false);
            }
            HUD.SetActive(!isShowing);
            pauseMenu.SetActive(isShowing);
    }

    public void pauseMenuPlayButton() {
        changeMenuState();
    }

    public void pauseMenuQuitButton() {
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void firstTrigger() {
        for (int i = 0; i < nbZombieTotal; i++)
        {
            for (int i2 = 0; i2 < firstTriggerSpawnpoints.Count; i2++) {
                Instantiate(zombie, firstTriggerSpawnpoints[i2].transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
    public void secondTrigger() {
        for (int i = 0; i < nbZombieTotal; i++)
        {
            for (int i2 = 0; i2 < secondTriggerSpawnpoints.Count; i2++) {
                Instantiate(zombie, secondTriggerSpawnpoints[i2].transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
    
    // au moment que le joueur entre dans la maison, il gagne la partie
    private void winCondition() {
        if (player.transform.position.x >= 114 && player.transform.position.z >= 391)
        {
            EndGameManager.isWin = true;
            SceneManager.LoadScene("EndGame");
        }
    }
}
