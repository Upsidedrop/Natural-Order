using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    int zoom = 3;
    Camera Camera;
    Vector3 velocity;
    // Update is called once per frame
    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetAxis("Zoom") > 0)
        {
            Zoom(true);
        }
        else if (Input.GetAxis("Zoom") < 0)
        {
            Zoom(false);
        }
        DelayedMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void Zoom(bool ZoomIn)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (ZoomIn)
        {
            if (zoom > 1)
            {

                zoom--;
                Camera.orthographicSize = Mathf.Pow(2, zoom);
                transform.position = new(
            mousePos.x - (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x),
            mousePos.y - (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y),
            -10);
            }

        }
        else
        {
            if (zoom < 6)
            {
                zoom++;
                Camera.orthographicSize = Mathf.Pow(2, zoom);
                transform.position = new(
            mousePos.x - (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x),
            mousePos.y - (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y),
            -10);
            }

        }



    }
    void DelayedMove(float xDirection, float yDirection)
    {
        float alteredX;
        float alteredY;
        alteredX = Mathf.Lerp(velocity.x, xDirection, 0.25f) * zoom / 5;
        alteredY = Mathf.Lerp(velocity.y, yDirection, 0.25f) * zoom / 5;
        velocity = new(alteredX, alteredY);
    }
    private void FixedUpdate()
    {
        transform.position += velocity;
    }
}
