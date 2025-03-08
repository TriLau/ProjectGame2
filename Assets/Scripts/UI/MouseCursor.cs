using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : Singleton<MouseCursor>
{
    public SpriteRenderer rend;
    public Sprite sprite;

    void Start()
    {
        //Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;


    }
}
