using UnityEngine;

public class ToyboxToggle : MonoBehaviour
{
    private bool isVisible = false;
    private Vector3 startPos;

    public RectTransform buttonTransform;
    public RectTransform toyboxPanelTransform;

    private void Awake()
    {
        if (buttonTransform == null)
        {
            buttonTransform = GetComponent<RectTransform>();
        }
        if (toyboxPanelTransform == null)
        {
            toyboxPanelTransform = GameObject.Find("ToySelectionPanel").GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        startPos = new Vector3(toyboxPanelTransform.rect.width - buttonTransform.rect.width, 0);
        Debug.Log(startPos);
        if (toyboxPanelTransform)
        {
            toyboxPanelTransform.localPosition += startPos;
        }
    }

    public void ToggleVisibility()
    {
        if (isVisible)
        {
            toyboxPanelTransform.localPosition += startPos;
        }
        else
        {
            toyboxPanelTransform.localPosition -= startPos;
        }
        isVisible = !isVisible;
    }

    /// <summary>
    /// Call one of these when the player should not be allowed to interact with the UI;
    /// </summary>
    public void Hide()
    {
        toyboxPanelTransform.position = new Vector3(toyboxPanelTransform.rect.width, 0);
    }

    public void Disable()
    {
        toyboxPanelTransform.gameObject.SetActive(false);
    }
}
