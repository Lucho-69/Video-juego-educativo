using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public RectTransform gameOverMenu;
    public GameObject missionFailedCanvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        missionFailedCanvas.SetActive(true);
    }
}
