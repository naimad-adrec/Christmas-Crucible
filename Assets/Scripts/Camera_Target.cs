using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Target : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform trans;
    [SerializeField] private float threshold;

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (trans.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + trans.position.x, threshold + trans.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + trans.position.y, threshold + trans.position.y);

        this.transform.position = targetPos;
    }
}
