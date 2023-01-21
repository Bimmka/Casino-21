using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
public class LeaderboardManager : MonoBehaviour
{
    private int leaderboardID = 10783; // APIKEY - dev_18c8c2155f4944a98b154fd386223f39
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    public TMP_InputField playerNameInputfield;
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }
    
    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return FetchTopHighscoresRoutine();
    }
    
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if(response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    
    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
            if(response.success)
            {
                Debug.Log("Succesfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name"+response.Error);
            }
        });
    }
    
    public IEnumerator SubmitScoreRoutine(int scoreToUpload) // отправка поинтов на сервер
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
         {
             if(response.success)
             {
                 Debug.Log("Successfully uploaded score");
                 done = true;
             }
             else
             {
                 Debug.Log("Failed" + response.Error);
                 done = true;
             }
         });
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighscoresRoutine() // получение списка из 10 чемпионов
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboardID, 10, 0, (response) =>
        {
            if(response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if(members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
