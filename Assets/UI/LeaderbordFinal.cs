using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderbordFinal : MonoBehaviour
{
    public LeaderboardManager lm;
    public List<Text> names;
    public List<Text> scores;
    void Start()
    {
        if (lm.nbrPlayer == 3)
        {
            names.RemoveAt(2);
            scores.RemoveAt(2);
        }

        for (int i = 0; i < lm.scoreboard.Count; i++)
        {
            var item = lm.scoreboard[i];

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
