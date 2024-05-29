using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;

    void Start()
    {
        // Calculate the screen bounds in world space
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void LateUpdate()
    {
        // Get the current position of the character
        Vector3 viewPos = transform.position;

        // Clamp the position within the screen bounds
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x, screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y, screenBounds.y);

        // Update the character's position
        transform.position = viewPos;
    }
}
