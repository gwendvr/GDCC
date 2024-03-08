using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> players; // Liste des joueurs en jeu
    public List<Light> LightPlayer;
    public List<GameObject> Ref;
    public List<GameObject> RespawnPoint;
    public int pointsForLastSurvivor = 3; // Points attribués au dernier survivant
    public LeaderboardManager lm;
    public GameObject canvaScore;
    private bool pointsAdded = false;
    
    void Update()
    {
        CheckLastSurvivor();
        if (Input.GetKeyDown(KeyCode.U)) 
        {
            KillPlayer(0);
        }
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            KillPlayer(1);
        }
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            KillPlayer(2);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            KillPlayer(3);
        }
        CheckForWin();
    }
    
    void CheckLastSurvivor()
    {
        if (pointsAdded) return;

        GameObject lastSurvivor = null;
        int survivors = 0;

        foreach (var player in players)
        {
            if (player.activeSelf) 
            {
                survivors++;
                lastSurvivor = player;
            }
        }

        if (survivors == 1) 
        {
            AddPoints(lastSurvivor, pointsForLastSurvivor);
            lm.UpdateScore();
            canvaScore.SetActive(true);
            pointsAdded = true;
        }
    }

    
    void AddPoints(GameObject player, int points)
    {
        if (player.TryGetComponent<Score>(out Score scoreComponent))
        {
            scoreComponent.ScorePlayer += points;
        }
        
    }

    
    void ResetScores()
    {
        foreach (var player in players)
        {
            if (player.TryGetComponent<Score>(out Score scoreComponent))
            {
                scoreComponent.ScorePlayer = 0;
            }
        }
    }

    public void TwoPlayer()
    {
        LightPlayer.RemoveAt(3);
        players.RemoveAt(3);
        LightPlayer.RemoveAt(2);
        players.RemoveAt(2);
    }
    public void ThreePlayer()
    {
        LightPlayer.RemoveAt(3);
        players.RemoveAt(3);
    }
    
    void KillPlayer(int playerIndex)
    {
        if (playerIndex < players.Count) 
        {
            players[playerIndex].SetActive(false); 
        }
    }
    void CheckForWin()
    {
        foreach(GameObject player in players)
        {
            if(player.activeInHierarchy && player.GetComponent<Score>().ScorePlayer >= 10)
            {
                Debug.LogWarning("Jai gagne");
                SceneManager.LoadScene("VictoryScene"); 
                break; 
            }
        }
    }

    public void RestartManche()
    {
        RespawnPoint.Clear();
        foreach (var element in Ref)
        {
            RespawnPoint.Add(element);
        }
        foreach (var player in players)
        {
            int rnd;
            rnd = Random.Range(0, RespawnPoint.Count);
            player.gameObject.SetActive(true);
            player.gameObject.transform.position = RespawnPoint[rnd].transform.position;
            
            RespawnPoint.Remove(RespawnPoint[rnd]);
        }

        pointsAdded = false;
    }
}
