using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButtonAmo : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool Pressed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
