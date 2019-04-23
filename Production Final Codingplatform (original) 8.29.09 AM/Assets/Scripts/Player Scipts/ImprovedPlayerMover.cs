using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedPlayerMover : MonoBehaviour
{
    public GameObject Camera;

    private float CameraX;
    private float CameraY;
    //Used for finding the Y angle for claping the camera view
    private float CameraYAngle;
    //-.22 works well
    public float YAngleMin;
    // -.25 works well
    public float YAngleMax;
    public void CameraRotater()
    {
        CameraX = Input.GetAxis("Mouse X");
        //CameraX = Input.GetAxis("4");
        CameraY = -Input.GetAxis("Mouse Y");
        transform.Rotate(0, CameraX, 0);
        CameraYAngle = Camera.transform.rotation.x;
        Camera.gameObject.transform.Rotate(CameraY, 0, 0);
        if (Camera.transform.localRotation.x >= YAngleMax)
        {
            Camera.gameObject.transform.Rotate(-CameraY, 0, 0);
        }
        if (Camera.transform.localRotation.x <= YAngleMin)
        {
            Camera.gameObject.transform.Rotate(-CameraY, 0, 0);
        }
    }
}
