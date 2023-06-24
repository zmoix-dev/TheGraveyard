using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectivesManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveDisplay;
    List<Objective> activeObjectives = new List<Objective>();

    void Start() {
        AddObjective(Objectives.crypt);
    }

    public void AddObjective(Objective objective) {
        if (!activeObjectives.Contains(objective)) {
            activeObjectives.Add(objective);
        }
        DisplayObjectives();
    }

    public void CompleteObjective(Objective objective){
        activeObjectives.Remove(objective);
        DisplayObjectives();
    }

    private void DisplayObjectives()
    {
        objectiveDisplay.text = GetObjectivesDisplayText();
    }

    public string GetObjectivesDisplayText() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Objectives\n");
        foreach(Objective o in activeObjectives) {
            stringBuilder.Append(o.ToString());
            stringBuilder.Append("\n");
        }
        return stringBuilder.ToString();
    }
}
