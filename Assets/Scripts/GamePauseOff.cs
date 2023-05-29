using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GamePauseOff : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private float rotationLimit = 40;
    [SerializeField] private float rotationSpeed = 15;

    private bool rotate = false;

    public GameObject BossExplainPanel;
    public GameObject PlayerUI;

    void FixedUpdate()
    {
        float targetRotate = rotate ? rotationLimit : 0f;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(targetRotate, 0, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
    }

    public void PasueOff()
    {
        Time.timeScale = 1;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        rotate = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        rotate = false;
        PasueOff();
        BossExplainPanel.SetActive(false);
        PlayerUI.SetActive(true);
    }
}