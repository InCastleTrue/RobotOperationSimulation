using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Button startButton;
    public Button endButton;

    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(StartButtonUp);
        endButton = GameObject.Find("ExitButton").GetComponent<Button>();
        endButton.onClick.AddListener(EndButtonUp);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartButtonUp()
    {
        SceneManager.LoadScene("Lab");
        
    }
    void EndButtonUp()
    {
        Application.Quit();

    }
}
