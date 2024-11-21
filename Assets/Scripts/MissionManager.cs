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

            
            missionDescription.text = "Mission: Avisa a las Heroinas antes de que acabe el tiempo";
            missionDescription.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            UpdateTimerUI();

            
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
            
            timerRunning = false;
            interactSymbol.SetActive(false); 
            timerText.text = "¡Misión Completada!";
            missionDescription.text = "";

            
            StartCoroutine(HideMissionUI());

            
        }
    }

    private IEnumerator HideMissionUI()
    {
        yield return new WaitForSeconds(2f);
        timerText.gameObject.SetActive(false);
        missionDescription.gameObject.SetActive(false);
    }
}
