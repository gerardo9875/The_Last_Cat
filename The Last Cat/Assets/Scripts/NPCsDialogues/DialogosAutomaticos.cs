using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Dialogue
{
    public Sprite sprite;
    [TextArea(4, 6)] public string line;
}

public class DialogosAutomaticos : MonoBehaviour
{
    [SerializeField] Image foto; 
    [HideInInspector] public bool didDialogueStart;
    private int lineaIndex;

    private float typingTime = 0.05f;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Dialogue[] dialogueLine;

    public void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineaIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }
    private IEnumerator ShowLine()
    {
        foto.sprite = dialogueLine[lineaIndex].sprite;
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLine[lineaIndex].line)
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
    private void NexDialogueLine()
    {
        lineaIndex++;
        if (lineaIndex < dialogueLine.Length)
        {
            StartCoroutine(ShowLine());
        }

        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }
    private void Update()
    {
        if (!didDialogueStart || lineaIndex >= dialogueLine.Length) return; 
        if(Input.GetKeyDown(KeyCode.E))
        {
            NexDialogueLine();
        }
    }
}
