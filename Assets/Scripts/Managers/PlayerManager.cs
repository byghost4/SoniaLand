using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public CameraManager cameraManager;

    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }


    public void MovePlayer()
    {
        switch (cameraManager.cameraCtrlState)
        {
            case CameraManager.CameraCtrlState.isCtrl:
                IsEyeCtrlMove();
                break;
            case CameraManager.CameraCtrlState.NoCtrl:
                IsEyeCtrlMove();
                break;
            default:
                break;
        }
    }

    private void NoEyeCtrlMove()
    {

    }

    private void IsEyeCtrlMove()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        if (H != 0 || V != 0)
        {
            transform.Translate(new Vector3(H, 0, V) * Time.deltaTime * moveSpeed, Space.World);
        }
        else
        {
            //没有移动
        }
    }
}
