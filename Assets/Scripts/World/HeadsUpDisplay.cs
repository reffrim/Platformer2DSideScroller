using UnityEngine;
using System.Collections;

public class HeadsUpDisplay : MonoBehaviour
{
    public int Offset;
    public GUIStyle FontStyle = new GUIStyle();
    public GUIStyle PauseFontStyle = new GUIStyle();
    
    int FontSize;
    int PauseFontSize;
    float Accumulator;
    float ScoreToShow;

    void Start()
    {
        Accumulator = 0.5f;
        ScoreToShow = 0;
    }

    private void Update()
    {
        if (ScoreToShow < WorldManager.Score)
        {
            ScoreToShow = Mathf.Lerp(ScoreToShow, WorldManager.Score, Accumulator);
            Accumulator += 0.02f * Time.deltaTime * 60;
        }
        else if (ScoreToShow == WorldManager.Score)
            Accumulator = 0f;
    }

    void OnGUI()
    {
        ScaleFontSize();

        GUI.Label(new Rect(Screen.width / 85f + Offset, Screen.height / 58 + Offset, Screen.width, Screen.height),
            string.Format("SCORE: {0}", ScoreToShow.ToString("0.")),
            FontStyle
            );

        GUI.Label(new Rect(Screen.width / 85f + Offset, Screen.height / 20 + Offset, Screen.width, Screen.height),
            string.Format("LEVEL: {0}", (WorldManager.Level - 3).ToString()),
            FontStyle
            );

        if (Time.timeScale == 0)
        {
            GUI.Label(
          new Rect(Screen.width / 2.3f + Offset, Screen.height / 2.0f + Offset, Screen.width, Screen.height),
          "PAUSE",
          PauseFontStyle
          );
        }
    }

    private void ScaleFontSize()
    {
        FontSize = Screen.width / 60;
        FontStyle.fontSize = FontSize;

        PauseFontSize = Screen.width / 28;
        PauseFontStyle.fontSize = PauseFontSize;
    }

    
}