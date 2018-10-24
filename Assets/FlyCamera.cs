using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour
{

    public static float standardSpeed = 3.0f;
    public static float fastSpeed = 15.0f;
    public static float rotationSpeed = 60.0f;
    public static float orientation = 0.0f;
    public static float positionalSpeed = 7.5f;

    float speed = standardSpeed;

    public GameObject CenterEyeAnchor;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        float primaryIndex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float secondaryIndex = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) || OVRInput.Get(OVRInput.Button.DpadUp) || OVRInput.Get(OVRInput.Button.DpadDown) || OVRInput.Get(OVRInput.Button.DpadLeft) || OVRInput.Get(OVRInput.Button.DpadRight))
        {
            speed = fastSpeed;
        }
        else
        {
            speed = standardSpeed;
        }

        // Dpad Movement
        if (OVRInput.Get(OVRInput.Button.DpadUp) || Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = CenterEyeAnchor.transform.forward * speed;
        }
        else if (OVRInput.GetUp(OVRInput.Button.DpadUp) && (primaryAxis.y == 0.0f && secondaryAxis.x == 0.0f))
        {
            rb.velocity = CenterEyeAnchor.transform.forward * 0.0f;
        }
        if (OVRInput.Get(OVRInput.Button.DpadDown) || Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = CenterEyeAnchor.transform.forward * speed * -1;
        }
        else if (OVRInput.GetUp(OVRInput.Button.DpadDown) && (primaryAxis.y == 0.0f && secondaryAxis.x == 0.0f))
        {
            rb.velocity = CenterEyeAnchor.transform.forward * 0.0f;
        }
        if (OVRInput.Get(OVRInput.Button.DpadRight))
        {
            rb.velocity = CenterEyeAnchor.transform.right * speed;
        }
        else if (OVRInput.GetUp(OVRInput.Button.DpadRight) && (primaryAxis.y == 0.0f && secondaryAxis.x == 0.0f))
        {
            rb.velocity = CenterEyeAnchor.transform.right * 0.0f;
        }
        if (OVRInput.Get(OVRInput.Button.DpadLeft))
        {
            rb.velocity = CenterEyeAnchor.transform.right * speed * -1;
        }
        else if (OVRInput.GetUp(OVRInput.Button.DpadLeft) && (primaryAxis.y == 0.0f && secondaryAxis.x == 0.0f))
        {
            rb.velocity = CenterEyeAnchor.transform.right * 0.0f;
        }

        // Left Analog Stick Movement (Camera Face Movement)
        if (primaryAxis.x != 0.0f || primaryAxis.y != 0.0f)
        {
            rb.velocity = CenterEyeAnchor.transform.forward * speed * primaryAxis.y + CenterEyeAnchor.transform.right * speed * primaryAxis.x;
        }
        else if (primaryAxis.x == 0.0f && primaryAxis.y == 0.0f && (OVRInput.Get(OVRInput.Button.DpadUp) == false && OVRInput.Get(OVRInput.Button.DpadDown) == false && OVRInput.Get(OVRInput.Button.DpadRight) == false && OVRInput.Get(OVRInput.Button.DpadLeft) == false) && rb.velocity.magnitude > 0)
        {
            rb.velocity = CenterEyeAnchor.transform.forward * 0.0f + CenterEyeAnchor.transform.right * 0.0f;
        }

        // Right Analog Stick Player Rotation (This can cause player disorientation)
        if (secondaryAxis.x != 0.0f)
        {
            orientation = orientation + rotationSpeed * secondaryAxis.x * Time.deltaTime;
            rb.rotation = Quaternion.Euler(0, orientation, 0);
        }

        // Triggers Vertical Movement (Game World Vertical Movement)
        if (primaryIndex != 0.0f || secondaryIndex != 0.0f)
        {
            rb.velocity = transform.up * positionalSpeed * (secondaryIndex - primaryIndex);
        }
    }
}
