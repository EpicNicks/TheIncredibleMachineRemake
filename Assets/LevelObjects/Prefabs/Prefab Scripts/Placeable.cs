using UnityEngine;

public abstract class Placeable : MonoBehaviour
{
    private Vector3 originalScale;

    public bool interactable;
    public float pulseSpeed = 1.0f;
    public virtual void Place()
    {
        transform.localScale = originalScale;
    }

    public virtual void Unplace()
    {

    }

    protected void OnStart()
    {
        originalScale = transform.localScale;
    }

    protected void OnLateUpdate()
    {
        if (interactable)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        DraggableWrapperGeneric dwg = hit.collider.GetComponent<DraggableWrapperGeneric>();
                        dwg.ReGrab();
                    }
                    Pulse();
                }
            }
        }
    }

    private void Pulse()
    {
        float next = Mathf.PingPong(Time.time * pulseSpeed, 0.2f);
        transform.localScale = originalScale + new Vector3(next, next, next);
    }
}
