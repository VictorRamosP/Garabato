using System.Collections;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    private bool isPlayerRange;
    public GameObject excalamacion;
    public TMP_Text dialogueText;
    public GameObject textPanel;
    [SerializeField, TextArea(3,4)] private string[] dialogs;

    private bool dialogueStart;
    private int lineText;
    // Update is called once per frame
    void Update()
    {
        if (isPlayerRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueStart)
            {
                StartDialogue();
            }
            else if(dialogueText.text == dialogs[lineText])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogs[lineText];
            }
        }
    }

    private void StartDialogue()
    {
        dialogueStart = true;
        textPanel.SetActive(true);
        lineText = 0;
        excalamacion.SetActive(false);
        Time.timeScale = 0;
        StartCoroutine(ShowLine());
    }

    private void NextLine()
    {
        lineText++;
        if (lineText<dialogs.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            dialogueStart = false;
            textPanel.SetActive(false);
            excalamacion.SetActive(true);
            Time.timeScale = 1;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char c in dialogs[lineText])
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerRange = false;
        }
    }
}
