using UnityEngine;

public abstract class Placeable : MonoBehaviour
{
    private Vector3 originalScale;
    private DraggableWrapperGeneric dwg;

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
        if (dwg == null)
        {
            dwg = GetComponent<DraggableWrapperGeneric>();
        }
    }

    protected void OnLateUpdate()
    {
        if (interactable)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000))
            {

                if (hit.collider.gameObject == gameObject || hit.collider.gameObject.transform.IsChildOf(transform))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
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
