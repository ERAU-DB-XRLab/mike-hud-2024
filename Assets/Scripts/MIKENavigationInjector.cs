using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MIKENavigationInjector : MonoBehaviour
{

    private EventSystem es;
    public MIKEButton b;

    // Start is called before the first frame update
    void Awake()
    {
        es = GetComponent<EventSystem>();
        es.SetSelectedGameObject(b.gameObject);
    }

    public void UIMove(MoveDirection dir)
    {
        AxisEventData data = new AxisEventData(EventSystem.current);
        data.moveDir = dir;
        data.selectedObject = EventSystem.current.currentSelectedGameObject;

        ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
    }

}
