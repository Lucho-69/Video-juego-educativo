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
    public GameObject missionPanel; // Referencia al panel de misi�n
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
        missionPanel.SetActive(false); // Aseg�rate de que el MissionPanel est� desactivado al principio
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

            // Activamos el MissionPanel al iniciar la misi�n
            missionPanel.SetActive(true); // Hacemos visible el panel de misi�n

            missionDescription.text = "Mission: Avisa a las Heroinas antes de que acabe el tiempo";
            missionDescription.gameObject.SetActive(true); // Aseguramos que la descripci�n de la misi�n se vea
            timerText.gameObject.SetActive(true); // Aseguramos que el temporizador sea visible

            UpdateTimerUI();

            // Mover la c�mara hacia el objetivo de la misi�n
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

        // Una vez que la c�mara se mueve al objetivo, mostramos el s�mbolo de interacci�n
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
            // Si el jugador completa la misi�n, detener el temporizador
            timerRunning = false;
            interactSymbol.SetActive(false);

            // Cambiar el texto a "Misi�n Completada!"
            timerText.text = "�Misi�n Completada!";
            missionDescription.text = "";


            StartCoroutine(HideMissionUI());


        }
    }

    private IEnumerator HideMissionUI()
    {
        // Esperar 2 segundos antes de ocultar la UI
        yield return new WaitForSeconds(2f);

        // Desactivar el MissionPanel
        missionPanel.SetActive(false); // Ocultamos todo el panel de la misi�n

        // Tambi�n puedes desactivar los elementos individuales si es necesario
        missionDescription.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

    }
}
