using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(TeamAI))]
public class TeamAIPositionEditor : Editor{

    [System.Serializable]
    public enum EPositions
    {
        offensive,
        defensive,
        faceoff
    }

    
    public EPositions currentPos;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TeamAI ai = (TeamAI)target;
        currentPos = (EPositions) EditorGUILayout.Popup("popup", (int)currentPos, Enum.GetNames(typeof(EPositions)), EditorStyles.popup);
        if (GUILayout.Button("Set Positions") && EditorUtility.DisplayDialog("Set Postitions?", "Set "+currentPos+" positions?", "Set", "Do Not Set"))
        {
            SetPositions(ai, currentPos);
        }
        if (GUILayout.Button("Place at Positions"))
        {
            PlacePositions(ai, currentPos);
        }

    }

    public void SetPositions(TeamAI ai, EPositions pos)
    {
        switch(pos)
        {
            case EPositions.defensive:
                SetDefensivePositions(ai);
                break;
            case EPositions.faceoff:
                SetFaceoffPositions(ai);
                break;
            case EPositions.offensive:
                SetOffensivePositions(ai);
                break;
        }
    }

    public void PlacePositions(TeamAI ai, EPositions pos)
    {
        List<Vector2> positions = new List<Vector2>();
        switch (pos)
        {
            case EPositions.defensive:
                positions = ai.DefensivePositions;
                break;
            case EPositions.faceoff:
                positions = ai.FaceoffPositions;
                break;
            case EPositions.offensive:
                positions = ai.OffensivePositions;
                break;
        }

        for(int i = 0; i < positions.Count; i++)
        {
            ai.Team.mPlayers[i].transform.position = positions[i];
        }
    }

    void SetDefensivePositions(TeamAI ai)
    {
        ai.DefensivePositions = new List<Vector2>();
        for (int i = 0; i < ai.Team.mPlayers.Count; i++)
        {
            ai.DefensivePositions.Add(ai.Team.mPlayers[i].GetPosition());
        }
    }

    void SetOffensivePositions(TeamAI ai)
    {
        ai.OffensivePositions = new List<Vector2>();
        for (int i = 0; i < ai.Team.mPlayers.Count; i++)
        {
            ai.OffensivePositions.Add(ai.Team.mPlayers[i].GetPosition());
        }
    }

    void SetFaceoffPositions(TeamAI ai)
    {
        ai.FaceoffPositions = new List<Vector2>();
        for (int i = 0; i < ai.Team.mPlayers.Count; i++)
        {
            ai.FaceoffPositions.Add(ai.Team.mPlayers[i].GetPosition());
        }
    }
}
