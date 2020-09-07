using UnityEngine;
using System.Collections;

// HANDLES PARALLAXING EFFECT IN HORIZONTAL X AXIS ONLY!
public class ParallaxingHorizontal : MonoBehaviour
{

    public Transform[] backgrounds;         // Array of all the 2D Z-space objects to be parallaxed.
    private float[] parallaxScales;         // The proportion of the camera's movement to move the backgrounds by.
    public float smoothing = 1f;            // How smooth the parallax will be.

    public Transform cam;                   // Choose which camera paralax applies to.
    private Vector3 previousCamPosX;        // The position of the camera in previous frame.


    void Start()
    {
        // The previous camera position corresponding with the current camera position.
        previousCamPosX = cam.position;

        // Asigning coresponding parallaxing for multiple layers.
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }


    void Update()
    {

        // For each background in the array...
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Parallax is the opposite of the camera movement because the previous frame is multiplied by the scale.
            float parallax = (previousCamPosX.x - cam.position.x) * parallaxScales[i];

            // Set a target x position which is the current position + the parallax.
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target position which is the background's current position with it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Move between current position and the target position using lerp.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set the previousCamPos to the camera's position at the end of the frame.
        previousCamPosX = cam.position;
    }
}