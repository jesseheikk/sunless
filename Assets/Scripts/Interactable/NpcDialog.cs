using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialog : Interactable
{
    string npcId;

    void Start()
    {
        // Assign a unique ID to this NPC
        npcId = gameObject.name;

        // Check if the dialog state for this NPC exists in PlayerInfo
        if (PlayerInfo.npcDialogStates.ContainsKey(npcId))
        {
            currentDialogIndex = PlayerInfo.npcDialogStates[npcId];
        }
        else
        {
            PlayerInfo.npcDialogStates[npcId] = currentDialogIndex;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Interact"))
        {
            if (!isDialogActive)
            {
                StartDialog();
            }
            else
            {
                ContinueDialog();
            }
        }
    }

    protected void ContinueDialog()
    {
        if (currentDialogIndex < dialogs.Length - 1)
        {
            StopAllCoroutines();
            currentDialogIndex++;
            PlayerInfo.npcDialogStates[npcId] = currentDialogIndex;
            StartCoroutine(ShowText(dialogs[currentDialogIndex]));
        }
        else
        {
            EndDialog();
        }
    }
}
