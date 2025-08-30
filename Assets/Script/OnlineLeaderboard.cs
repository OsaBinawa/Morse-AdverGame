using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class OnlineLeaderboard : MonoBehaviour
{
    private string scriptURL = "https://script.google.com/macros/s/AKfycbxpWl-sBhhuWq3t-e53cgwAPH1XPSqJDpuIGVwKzM0EQz--C11kdzYrDESLJ-aH5VxEQQ/exec"; // paste from Google Apps Script deploy

    // Upload score (call from GameOver)
    public void UploadScore(string playerName, string dropdown, int score, string username)
    {
        StartCoroutine(PostScore(playerName, dropdown, score, username));
    }


    private IEnumerator PostScore(string playerName, string dropdown, int score, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", playerName);
        form.AddField("dropdown", dropdown);
        form.AddField("score", score);
        form.AddField("username", username);   // ✅ send as username

        using (UnityWebRequest www = UnityWebRequest.Post(scriptURL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("❌ Upload failed: " + www.error);
            else
                Debug.Log("✅ Upload response: " + www.downloadHandler.text);
        }
        Debug.Log($"⬆️ Sending to sheet: name={playerName}, dropdown={dropdown}, score={score}, username={username}");

    }


    // Inspector button will call this
    public void WipeLeaderboard()
    {
        StartCoroutine(SendWipeRequest());
    }

    private IEnumerator SendWipeRequest()
    {
        // ✅ Send wipe command via query string
        string url = scriptURL + "?wipe=true";

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(url, ""))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("❌ Wipe failed: " + www.error);
            else
                Debug.Log("🧹 Wipe response: " + www.downloadHandler.text);
        }
    }

}
