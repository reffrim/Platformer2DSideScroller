using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static int Score
    {
        set
        {
            _score = value;
            //Debug.Log(_score);
        }

        get
        {
            return _score;
        }
    }

    public static int Level;
    public static int Difficulty
    {
        get
        {
            return Level / 3;
        }
    }

    static int _score;
    static int _difficulty;
    bool PauseSwitch;

    private void Awake()
    {
        Level = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1; //enable before final build
    }

    // Update is called once per frame
    void Update()
    {
        GamePause();

         if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void GamePause()
    {
        if (Input.GetButtonDown("Pause"))
            PauseSwitch = !PauseSwitch;

        if (PauseSwitch && !GameOverManager.IsGameOver)
        {
            Time.timeScale = 0;
            GameObject.Find("CheeseHead").GetComponent<PlayerController>().enabled = false;
        }
        else if (!PauseSwitch && !GameOverManager.IsGameOver)
        {
            Time.timeScale = 1;
            GameObject.Find("CheeseHead").GetComponent<PlayerController>().enabled = true;
        }
    }
}
