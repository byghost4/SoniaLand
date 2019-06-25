using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager : MonoBehaviour
{
    private PlayerManager playerManager;



    [SerializeField]
    //与目标物体距离距离
    [Range(0.5f,15)]
    private float distance;

    private const float MAX_DISTANCE = 15;
    private const float MIN_DISTANCE = 0.5f;

    [SerializeField]
    private float smoothTime = 0.3f;
    private Vector3 smoothValue = Vector3.zero;


    private void Start()
    {
        InitSet();
        SetCameraPos();
    }

    private void Update()
    {
        SetCameraDistance();
    }

    private void FixedUpdate()
    {
        transform.LookAt(playerManager.transform);
        transform.position = Vector3.SmoothDamp(transform.position,SetCameraPos(),ref smoothValue, smoothTime);
    }

    private void InitSet()
    {
        SetPlayer();
    }


    private PlayerManager SetPlayer()
    {
        if(playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
        return playerManager;
    }


    private Vector3 SetCameraPos()
    {
        Vector3 v = (transform.position - playerManager.transform.position).normalized;
        Vector3 pos = distance * v + playerManager.transform.position;
        return pos;
    }

    private void SetCameraDistance()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Debug.Log("放大");
            if (distance <= MIN_DISTANCE)
                return;
            distance -= 0.5f;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distance >= MAX_DISTANCE)
                return;
            distance += 0.5f;
        }
    }

    private void SetCameraConfig(float distance)
    {
        this.distance = distance;
    }
}
