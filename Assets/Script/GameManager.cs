using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private Player myplayer;

    public GameObject optionScreen;


    public Slider bgmSlider;
    public Slider soundSlider;

    public GameObject restartScreen;
    public TextMeshProUGUI restartTextMesh;
    public GameObject clearScreen;
    public TextMeshProUGUI clearTextMesh;

    public Button restartButton;
    public Button exitButton;

    public Button clearRestartButton;
    public Button clearExitButton;

    private bool isOption;
    private bool isGameActive;


    public CameraMove cameraMove;

    private GameObject canvas;
    private GameObject camera;
    private GameObject eventSyetem;




    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameObject gameObjectToKeep = GameObject.Find("Canvas");

        DontDestroyOnLoad(gameObjectToKeep);
        canvas = GameObject.Find("Canvas");
        camera = GameObject.Find("Main Camera");
        eventSyetem = GameObject.Find("EventSystem");

    }
    public bool IsOptionActive()
    {

        return isOption;
    }

    void Start()
    {
        Cursor.visible = false;
        optionScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player");
        myplayer = player.GetComponent<Player>();
        cameraMove = FindObjectOfType<CameraMove>();
        isOption = false;
        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(RestartButtonUp);
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitButtonUp);
        clearRestartButton = GameObject.Find("ClearRestartButton").GetComponent<Button>();
        clearRestartButton.onClick.AddListener(RestartButtonUp);
        clearExitButton = GameObject.Find("ClearExitButton").GetComponent<Button>();
        clearExitButton.onClick.AddListener(ExitButtonUp);
        restartTextMesh.text = $"½Ç ÆÐ";
        clearTextMesh.text = $"Clear";
        restartScreen.SetActive(false);
        clearScreen.SetActive(false);
        isGameActive = true;


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive == true)
        {
            OptionScreen();
        }
        if (myplayer.isPlayerDying == true)
        {
            Dying();
        }
        if(myplayer.isClear == true)
        {
            Ending();
        }
    }

        void OptionScreen()
        {
            if (!isOption)
            {
                isOption = true;
                Cursor.visible = true;
                optionScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                cameraMove.sensitivityX = 0;
                cameraMove.sensitivityY = 0;
            }
            else
            {
                isOption = false;
                Cursor.visible = false;
                optionScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            cameraMove.sensitivityX = 2;
            cameraMove.sensitivityY = 2;

        }
        }
    void Dying()
    {
        
            restartScreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
            cameraMove.sensitivityX = 0;
            cameraMove.sensitivityY = 0;
        
    }
    void Ending()
    {

        clearScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
        cameraMove.sensitivityX = 0;
        cameraMove.sensitivityY = 0;

    }
    void RestartButtonUp()
    {
        myplayer.isPlayerDying = false;
        SceneManager.LoadScene("Title");
        Destroy(gameObject);
        Destroy(player);
        Destroy(canvas);
        Destroy(camera);
        isOption = false;

        Time.timeScale = 1;
        cameraMove.sensitivityX = 2;
        cameraMove.sensitivityY = 2;
    }
    void ExitButtonUp()
    {
        Application.Quit();
    }

}

