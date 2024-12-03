using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ActivateTrigger : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public MissionTrigger missionTrigger;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogueArray;
    public string nextSceneName;

    private bool missionCompleted = false;
    private int currentDialogueIndex = 0;

    private void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (objectToActivate != null && !objectToActivate.activeSelf)
                objectToActivate.SetActive(true);

            if (objectToDeactivate != null && objectToDeactivate.activeSelf)
                objectToDeactivate.SetActive(false);

            if (!missionCompleted && missionTrigger != null)
            {
                missionTrigger.CompleteMission();
                missionCompleted = true;
            }

            if (dialoguePanel != null)
                dialoguePanel.SetActive(true);

            ShowNextDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogueArray.Length)
        {
            dialogueText.text = dialogueArray[currentDialogueIndex];
            currentDialogueIndex++;
            Invoke(nameof(ShowNextDialogue), 3f);
        }
        else
        {
            EndDialogueSequence();
        }
    }

    private void EndDialogueSequence()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }
}
