using TMPro;
using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    public GameObject missionPanel;  
    public TextMeshProUGUI missionDescription; 
    private bool missionActive = false;

    public void StartMission()
    {
        if (!missionActive)
        {
            missionActive = true;
            missionPanel.SetActive(true); 
            missionDescription.text = "Recoge los objetos y prep�rate para la defensa."; 
        }
    }
    public void CompleteMission()
    {
        missionDescription.text = "�Misi�n completada!";
        StartCoroutine(HideMissionDescriptionAfterDelay());
    }

    private System.Collections.IEnumerator HideMissionDescriptionAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        missionDescription.text = "";
        missionPanel.SetActive(false);
    }
}
