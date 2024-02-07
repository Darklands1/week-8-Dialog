using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogNode
{
    public string dialogText;
    public List<DialogChoice> choices;
}

[System.Serializable]
public class DialogChoice
{
    public string choiceText;
    public DialogNode nextNode;
    public string flagToSet;
    public bool flagValue;
}

[CreateAssetMenu(fileName = "new Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    public DialogNode startNode;
    public List<AlternativeDialog> alternativeDialogs;
}

[System.Serializable]
public class AlternativeDialog
{
    public string flagRequired;
    public DialogNode alternativeStartNode;
}