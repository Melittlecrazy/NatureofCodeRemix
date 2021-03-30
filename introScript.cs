using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introScript : MonoBehaviour
{
    //And then we need to create a walker
    private Walker walkeri5;

    // Start is called before the first frame update
    void Start()
    {
        // Create the walker
        walkeri5 = new Walker();
    }

    // Update is called once per frame
    void Update()
    {
        //Have the walker move
        walkeri5.step();
        walkeri5.CheckEdges();
    }
}

public class Walker
{
    //GameObject
    Vector2 location;

    // The window limits
    private Vector2 minimumPos, maximumPos;

    // Range over which height varies.
    float heightScale = .7f;
    float widthScale = 1.0f;

    // Distance covered per second along X axis of Perlin plane.
    float xScale = 1.0f;
    float yScale = .5f;


    // Start is called before the first frame update
    // Gives the class a GameObject to draw on the screen
    public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public Walker()
    {
        findWindowLimits();
        location = Vector2.zero;
        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    public void step()
    {

        float width = widthScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);
        float height = heightScale * Mathf.PerlinNoise(0.0f, Time.time * yScale);
        Vector3 pos = mover.transform.position;
        pos.y = height;
        pos.x = width;
        mover.transform.position = pos;


    }

    public void CheckEdges()
    {
        location = mover.transform.position;
        if ((location.x > maximumPos.x) || (location.x < minimumPos.x))
        {
            reset();
        }

        if ((location.y > maximumPos.y) || (location.y < minimumPos.y))
        {
            reset();
        }
        mover.transform.position = location;
    }

    void reset()
    {
        location = Vector2.zero;
        heightScale = 5;
        widthScale = 10;
    }

    private void findWindowLimits()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the minimum and maximum position for the screen
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}


