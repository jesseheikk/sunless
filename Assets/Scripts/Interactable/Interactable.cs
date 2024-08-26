using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject dialog;
    [SerializeField] Text dialogText;
    [SerializeField] protected string[] dialogs;

    protected int currentDialogIndex = 0;
    protected bool isPlayerInRange = false;
    protected bool isDialogActive = false;
    protected float textSpeed = 0.025f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = false;
            EndDialog();
        }
    }

    protected void StartDialog()
    {
        dialog.SetActive(true);
        isDialogActive = true;
        StartCoroutine(ShowText(dialogs[currentDialogIndex]));
    }

    protected void EndDialog()
    {
        StopAllCoroutines();
        if (dialogText)
        {
            dialogText.text = "";
        }
        if (dialog)
        {
            dialog.SetActive(false);
        }
        isDialogActive = false;
    }

    protected IEnumerator ShowText(string text)
    {
        dialogText.text = "";
        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
