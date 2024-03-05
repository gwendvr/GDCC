using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderbordFinal : MonoBehaviour
{
    public List<Text> names;
    public List<Text> scores;
    public GameObject display;
    void Start()
    {

        if (GameData.NbrPlayer == 2)
        {
            names.RemoveAt(2);
            scores.RemoveAt(2);
            display.SetActive(false);
        }
        else
        {
            display.SetActive(true);
        }

        for (int i = 0; i < GameData.Scoreboard.Count; i++)
        {
            var item = GameData.Scoreboard[i];

            if (i < names.Count)
            {
                names[i].text = item.Item1;
            }

            if (i < scores.Count)
            {
                scores[i].text = item.Item2.ToString();
            }
        }

    }
}
