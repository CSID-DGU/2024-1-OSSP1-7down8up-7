using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject pauseMenu;
    //추가->인벤토리
    public GameObject inventory;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        inventory= GameObject.Find("Inventory");
        inventory.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }else if (Input.GetKeyDown(KeyCode.E)&& !pauseMenu.activeSelf)  //추가->인벤토리
        {
            if (inventory.activeSelf)
            {
               
                inventory.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                inventory.SetActive(true);
                inventory.GetComponent<Inventory>().FreshSlot();
                Time.timeScale = 0;
                
            }

             
        }
               
           
            
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void GoToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
}