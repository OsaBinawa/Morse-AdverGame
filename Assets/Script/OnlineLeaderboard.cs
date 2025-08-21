using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class OnlineLeaderboard : MonoBehaviour
{
    private string scriptURL = "https://script.google.com/macros/s/AKfycbzfRUFaUnFXbWlHqhOoa5rspPlrSBtSURgsnGrtHnONLEuEFjWKo8GvgtwPv64wSeapng/exec"; // paste from Google Apps Script deploy

    // Upload score (call from GameOver)
    public void UploadScore(string playerName, string dropdown, int score)
    {
        StartCoroutine(PostScore(playerName, dropdown, score));
    }

    private IEnumerator PostScore(string playerName, string dropdown, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", playerName);
        form.AddField("dropdown", dropdown);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(scriptURL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("❌ Upload failed: " + www.error);
            else
                Debug.Log("✅ Upload response: " + www.downloadHandler.text);
        }
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
