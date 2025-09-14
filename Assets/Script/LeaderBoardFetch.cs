using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderBoardFetch : MonoBehaviour
{
    [System.Serializable]
    public class LeaderboardEntryData
    {
        public string name;   
        public int score;
    }

    [SerializeField] private string scriptURL = "https://script.google.com/macros/s/AKfycbx4LMjj8T8yJPIBfnLXyx5a0D2ht1dYcDMcDaWtfRDnP_kuDVz_nbrOoxlelNQQ0QA3sQ/exec";

    [Header("Leaderboard UI Lists")]
    [SerializeField] private List<TextMeshProUGUI> nameTexts;
    [SerializeField] private List<TextMeshProUGUI> scoreTexts;
    [SerializeField] private List<TextMeshProUGUI> dropdownTexts;


    private void Awake()
    {
        Debug.Log("LeaderboardManager Awake: fetching leaderboard...");
        FetchLeaderboard();
        StartCoroutine(AutoFetchLeaderboard());
    }
    // ---------------- Upload ----------------
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

    // ---------------- Wipe ----------------
    public void WipeLeaderboard()
    {
        StartCoroutine(SendWipeRequest());
    }

    private IEnumerator SendWipeRequest()
    {
        string url = scriptURL + "?wipe=true";
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("❌ Wipe failed: " + www.error);
            else
                Debug.Log("🧹 Wipe response: " + www.downloadHandler.text);
        }
    }

    // ---------------- Fetch ----------------
    public void FetchLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }

    private IEnumerator GetLeaderboard()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(scriptURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("❌ Error fetching leaderboard: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                Debug.Log("📥 Raw JSON received: " + json);

                try
                {
                    LeaderboardEntryData[] entries = JsonHelper.FromJson<LeaderboardEntryData>(json);
                    Debug.Log($"📊 Parsed {entries.Length} leaderboard entries");
                    DisplayLeaderboard(entries);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("❌ JSON Parse Failed: " + ex.Message);
                    Debug.LogError("Raw text was: " + json);
                }
            }
        }
    }


    private void DisplayLeaderboard(LeaderboardEntryData[] entries)
    {
        int count = Mathf.Min(entries.Length, nameTexts.Count);

        for (int i = 0; i < count; i++)
        {
            nameTexts[i].text = entries[i].name;
            scoreTexts[i].text = entries[i].score.ToString();

            Debug.Log($"✅ Slot {i}: {entries[i].name} - {entries[i].score}");
        }

        // Clear any unused slots
        for (int i = count; i < nameTexts.Count; i++)
        {
            nameTexts[i].text = "-";
            scoreTexts[i].text = "-";
            Debug.Log($"ℹ️ Cleared Slot {i}");
        }
    }

    private IEnumerator AutoFetchLeaderboard()
    {
        while (true)
        {
            FetchLeaderboard();             // Fetch once
            yield return new WaitForSeconds(30f); // Wait 10 seconds, then repeat
        }
    }


    // ---------------- JSON Helper ----------------
    public static class JsonHelper
    {
        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] items;
        }

        public static T[] FromJson<T>(string json)
        {
            string newJson = "{ \"items\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.items;
        }
    }

}
