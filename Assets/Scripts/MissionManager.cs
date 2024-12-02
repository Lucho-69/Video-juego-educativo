using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject missionPanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI missionDescription;
    public GameObject missionFailedCanvas;
    public float missionTime = 30f;

    private bool missionStarted = false;
    private bool timerRunning = false;
    private float timeRemaining;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        missionFailedCanvas.SetActive(false);  // Asegúrate de que el canvas de misión fallida esté inactivo al inicio

        missionPanel.SetActive(false);
        missionDescription.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    public void StartMission()
    {
        if (!missionStarted)
        {
            missionStarted = true;
            timeRemaining = missionTime;
            timerRunning = true;

            missionPanel.SetActive(true);
            missionDescription.text = "Mission: Avisa a las Heroínas antes de que acabe el tiempo";
            missionDescription.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);

            UpdateTimerUI();
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timerRunning = false;
                ShowMissionFailed();  // Mostrar mensaje de misión fallida
            }
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = "Tiempo restante: " + Mathf.Ceil(timeRemaining) + "s";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && missionStarted)
        {
            timerRunning = false;
            missionDescription.text = "¡Misión Completada!";

            StartCoroutine(HideMissionUIAfterDelay());
        }
    }

    private void ShowMissionFailed()
    {
        missionPanel.SetActive(false);  // Ocultar la UI de la misión antes de mostrar el mensaje de fallo
        missionDescription.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        missionFailedCanvas.SetActive(true);  // Mostrar el canvas de misión fallida
    }

    private IEnumerator HideMissionUIAfterDelay()
    {
        yield return new WaitForSeconds(5f);  // Esperar 5 segundos antes de ocultar el panel de misión
        missionPanel.SetActive(false);
        missionDescription.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    public void CompleteMission()
    {
        timerRunning = false;
        missionDescription.text = "¡Misión Completada!";
        StartCoroutine(HideMissionUIAfterDelay());
    }

    public void StopMissionTimer()  // Método para detener el temporizador desde otro script
    {
        timerRunning = false;
    }
}
