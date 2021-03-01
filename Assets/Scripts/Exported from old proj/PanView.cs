using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanView : MonoBehaviour
{
    [SerializeField, Range(1f, 300f)] float cameraSpeed = 20f;
    [SerializeField] Vector2 maxBound; //TODO - calculate bounds in dependency of bounding box
    [SerializeField] Vector2 minBound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        if(Input.mousePosition.x >= Screen.width * 0.95f) {
            direction.x = 1f;
        }
        if (Input.mousePosition.x <= Screen.width * 0.05f)
        {
            direction.x = -1f;
        }
        if (Input.mousePosition.y >= Screen.height * 0.95f)
        {
            direction.y = 1f;
        }
        if (Input.mousePosition.y <= Screen.height * 0.05f)
        {
            direction.y = -1f;
        }



        transform.Translate(direction * cameraSpeed * Time.deltaTime, Space.World);
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, minBound.x, maxBound.x),
            Mathf.Clamp(transform.position.y, minBound.y, maxBound.y),
            transform.position.z
        );
    }
}
