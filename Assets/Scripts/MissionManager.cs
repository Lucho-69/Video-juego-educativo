using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private GameManager gameManager;
    public Transform targetNPC;
    public Transform cameraTarget;
    public GameObject interactSymbol;
    public GameObject missionPanel; // Referencia al panel de misión
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI missionDescription;
    public float cameraMoveSpeed = 2f;
    public float missionTime = 30f;

    private bool missionStarted = false;
    private bool timerRunning = false;
    private float timeRemaining;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Desactivamos la UI al inicio
        missionPanel.SetActive(false); // Asegúrate de que el MissionPanel esté desactivado al principio
        missionDescription.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        interactSymbol.SetActive(false);
    }


    public void StartMission()
    {
        if (!missionStarted)
        {
            missionStarted = true;
            timeRemaining = missionTime;
            timerRunning = true;

            // Activamos el MissionPanel al iniciar la misión
            missionPanel.SetActive(true); // Hacemos visible el panel de misión

            missionDescription.text = "Mission: Avisa a las Heroinas antes de que acabe el tiempo";
            missionDescription.gameObject.SetActive(true); // Aseguramos que la descripción de la misión se vea
            timerText.gameObject.SetActive(true); // Aseguramos que el temporizador sea visible

            UpdateTimerUI();

            // Mover la cámara hacia el objetivo de la misión
            StartCoroutine(MoveCameraToTarget());
        }
    }

    private IEnumerator MoveCameraToTarget()
    {
        Camera mainCamera = Camera.main;
        Vector3 targetPosition = cameraTarget.position;
        targetPosition.z = mainCamera.transform.position.z;

        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
            yield return null;
        }

        // Una vez que la cámara se mueve al objetivo, mostramos el símbolo de interacción
        interactSymbol.SetActive(true);
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
                gameManager.TriggerMissionFailed();
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
            // Si el jugador completa la misión, detener el temporizador
            timerRunning = false;
            interactSymbol.SetActive(false);

            // Cambiar el texto a "Misión Completada!"
            timerText.text = "¡Misión Completada!";
            missionDescription.text = "";


            StartCoroutine(HideMissionUI());


        }
    }

    private IEnumerator HideMissionUI()
    {
        // Esperar 2 segundos antes de ocultar la UI
        yield return new WaitForSeconds(2f);

        // Desactivar el MissionPanel
        missionPanel.SetActive(false); // Ocultamos todo el panel de la misión

        // También puedes desactivar los elementos individuales si es necesario
        missionDescription.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

    }
}
