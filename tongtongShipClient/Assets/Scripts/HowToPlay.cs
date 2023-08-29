using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Image imageToMove; // Reference to your UI Image
    public float moveSpeed = 500f; // Speed of movement

    private Vector3 startPosition; // Store the start position
    private Vector3 endPosition; // Store the end position
    public bool movingToLeft = false; // State flag

    void Start()
    {
        startPosition = imageToMove.rectTransform.position;
        endPosition = new Vector3(startPosition.x - 2000, startPosition.y, startPosition.z); // Adjust the offset as needed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            movingToLeft = !movingToLeft; // Toggle state
        }

        float step = moveSpeed * Time.deltaTime;

        if (movingToLeft)
        {
            // Move towards the end position
            imageToMove.rectTransform.position = Vector3.MoveTowards(imageToMove.rectTransform.position, endPosition, step);
        }
        else
        {
            // Move towards the start position
            imageToMove.rectTransform.position = Vector3.MoveTowards(imageToMove.rectTransform.position, startPosition, step);
        }
    }
}
