using UnityEngine;
using System.Collections;

public class MyMinimapResizer : MonoBehaviour
{
    private float itsWidth;
    private float itsHeight;
    public Camera minimapCamera = null;

    private Vector2 lastPos = Vector2.zero;
float absolouteWidth = 300.0f; //in pixels
    float absolouteHeight = 300.0f;
   public  Camera minimapCam;





    // Use this for initialization
    void Start()
    {
        minimapCamera = GetComponent<Camera>();
        //Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 2));
        //minimapCamera.pixelRect = new Rect(Screen.width - 20, Screen.height - 20, 10, 10);

        minimapCam.pixelRect = new Rect(Screen.width/3 - absolouteWidth, Screen.height/3 - absolouteHeight, absolouteWidth, absolouteHeight);
    }

    // Update is called once per frame
    void Update()
    {
        //    itsHeight = 1.0f / 3.0f; //set the minimap height to 1/3 of the screen
        //    float itsScreenHeightInPixels = Screen.height * itsHeight;
        //    float itsScreenWidthInPixels = (itsScreenHeightInPixels / 9.0f) * 16.0f;
        //    itsWidth = itsScreenWidthInPixels / Screen.width;
        //    float anX = (1 - itsWidth) / 2.0f; //centers the minimap
        //    //itsMinimapCamera.rect = new Rect(anX, 0.0f, itsWidth, itsHeight);

        //minimapCamera.ScreenToViewportPoint(new Vector3(0, 1, 2));
        
    }
}