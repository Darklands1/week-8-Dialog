using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText; //assign in the inspector
    public Button choicePrefab; //assign a prefab with a button in the inspector
    public Transform choicePanel; // the parent panel for choice buttons

    private Queue<DialogNode> nodesQueue = new Queue<DialogNode>();

    private DialogNode currentNode;

    public void StartDialog(Dialog dialog)
    {
        nodesQueue.Clear();

        DialogNode startNode = dialog.startNode;

        foreach(var alternative in dialog.alternativeDialogs)
        {
            if(!string.IsNullOrEmpty(alternative.flagRequired) && GameStateManager.Instance.GetFlag(alternative.flagRequired))
            {
                startNode = alternative.alternativeStartNode;
                break;
            }
        }

        nodesQueue.Enqueue(startNode);
        

        DisplayNextNode();
    }

    public void EndDialog()
    {
        dialogText.text = "The end of the conversation.";
    }

    private void DisplayNextNode()
    {
        if (nodesQueue.Count == 0)
        {
            EndDialog();
            return;
        }

        currentNode = nodesQueue.Dequeue();
        dialogText.text = currentNode.dialogText;

        foreach (Transform child in choicePanel)
        {
            Destroy(child.gameObject);
        }

        foreach(DialogChoice choice in currentNode.choices)
        {
            Button choiceButton = Instantiate(choicePrefab, choicePanel);
            choiceButton.GetComponentInChildren<Text>().text = choice.choiceText;
            choiceButton.onClick.AddListener(delegate { MakeChoice(choice); });
        }
    }

    private void MakeChoice(DialogChoice choice)
    {
        foreach (Transform child in choicePanel)
        {
            Button button = child.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            Destroy(child.gameObject);
        }

        if(!string.IsNullOrEmpty(choice.flagToSet))
        {
            GameStateManager.Instance.SetFlag(choice.flagToSet, choice.flagValue);
        }

        if(choice.nextNode !=null)
        {
            nodesQueue.Enqueue(choice.nextNode);
        }

        DisplayNextNode();
    }
}
