using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderboardManager : MonoBehaviour
{
    public List<GameObject> players;
    public List<GameObject> ScoreLines;
    public List<(string, int)> scoreboard = new List<(string, int)>();
    public List<Text> names;
    public List<Text> scores;
    public GameObject templateLb;
    public int nbrPlayer = 4;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void UpdateScore()
    {
        scoreboard.Clear();
        foreach (GameObject player in players)
        {
            int score = player.GetComponent<Score>().ScorePlayer;
            int id = player.GetComponent<PlayerController>().ID + 1;
            string name = "Player " + id.ToString();
            scoreboard.Add((name, score));
        }

        scoreboard.Sort((x, y) => y.Item2.CompareTo(x.Item2));

        GameData.Scoreboard = new List<(string, int)>(scoreboard);
        GameData.NbrPlayer = nbrPlayer;

        for (int i = 0; i < scoreboard.Count; i++)
        {
            var item = scoreboard[i];

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

    public void TwoPlayer()
    {
        nbrPlayer = 2;
        players.RemoveAt(3);
        players.RemoveAt(2);
        names.RemoveAt(3);
        names.RemoveAt(2);
        scores.RemoveAt(3);
        scores.RemoveAt(2);
        ScoreLines[2].SetActive(false);
        ScoreLines[3].SetActive(false);
        UpdateScore();
    }

    public void ThreePlayer()
    {
        nbrPlayer = 3;
        players.RemoveAt(3);
        names.RemoveAt(3);
        scores.RemoveAt(3);
        ScoreLines[3].SetActive(false);
        UpdateScore();
    }

    public void Next()
    {
        templateLb.SetActive(false);
    }
}
