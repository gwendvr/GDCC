using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> players; // Liste des joueurs en jeu
    public int pointsForLastSurvivor = 3; // Points attribués au dernier survivant
    
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

    // pour le tableau de fin 
    void UpdateScoreDisplay()
    {
        
        string displayText = "";
        foreach (var player in players)
        {
            if (player.TryGetComponent<Score>(out Score scoreComponent))
            {
                displayText += player.name + ": " + scoreComponent.ScorePlayer + "\n";
            }
        }
    }

    public void TwoPlayer()
    {
        players.RemoveAt(3);
        players.RemoveAt(2);
    }
    public void ThreePlayer()
    {
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
                SceneManager.LoadScene("VictoryScene"); 
                break; 
            }
        }
    }
}
