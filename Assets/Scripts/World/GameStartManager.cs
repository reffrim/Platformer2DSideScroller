using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    float FlashingTimer;
    SpriteRenderer PressEnterRenderer;

    private void Start()
    {
        PressEnterRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FlashingTimer += Time.deltaTime * 60;    

        if (FlashingTimer > 30)
        {
            PressEnterRenderer.enabled = !PressEnterRenderer.enabled;
            FlashingTimer = 0;
        }

        if(Input.GetButtonDown("Start") && PlayerState.Instance.LifeCondition == LifeCondition.Dead)
        {
            WorldManager.Score = 0;
            SceneManager.LoadScene("MainScene");
        }

    }
}
