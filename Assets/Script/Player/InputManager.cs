using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{

    public event Action<Vector2> OnControlOnDown;
    public event Action<Vector2> OnControlDown;
    public event Action OnControlDownWithOutParam;
    public event Action<Vector2> OnControlUp;

    private EventSystem eventSystem;

    private bool isTouch = false;

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !eventSystem.IsPointerOverGameObject())
        {
            OnControlDown?.Invoke(Input.mousePosition);
            OnControlDownWithOutParam?.Invoke();


        }
        
        if(Input.GetMouseButton(0) && !eventSystem.IsPointerOverGameObject())
        {
            OnControlOnDown?.Invoke(Input.mousePosition);
           
            
        }

        

        if(Input.GetMouseButtonUp(0) && !eventSystem.IsPointerOverGameObject())
        {
            OnControlUp?.Invoke(Input.mousePosition);
        }
    }
}
