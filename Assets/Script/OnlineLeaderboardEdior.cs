using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(OnlineLeaderboard))]
[CanEditMultipleObjects]
public class OnlineLeaderboardEdior : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OnlineLeaderboard leaderboard = (OnlineLeaderboard)target;

        GUILayout.Space(10);

        if (GUILayout.Button("🧹 Wipe Leaderboard (Google Sheet)"))
        {
            if (Application.isPlaying)
            {
                leaderboard.WipeLeaderboard();
            }
            else
            {
                Debug.LogWarning("⚠️ Enter Play Mode to use wipe function.");
            }
        }
    }
}
