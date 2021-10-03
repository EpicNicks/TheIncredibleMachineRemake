using UnityEngine;
using UnityEngine.UI;

public class ToyDrawerSelector : MonoBehaviour
{
    private int instantiated;
    public int Instantiated
    {
        get => instantiated;
        set
        {
            instantiated = value;
            text.text = (amount - Instantiated).ToString();
        }
    }

    [SerializeField]
    private Button btn;

    public GameObject draggableWrapperPrefab;
    [SerializeField]
    private int amount = 3;
    public GridGenerator gridGenerator;
    public TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
        if (text == null)
        {
            text = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
        text.text = amount.ToString();

        if (gridGenerator == null)
        {
            gridGenerator = FindObjectOfType<GridGenerator>();
        }
    }

    public void OnClick()
    {
        if (Instantiated < amount)
        {
            Instantiated++;
            GameObject go = Instantiate(draggableWrapperPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)), Quaternion.identity);
            DraggableWrapperGeneric wrapper = go.GetComponentInChildren<DraggableWrapperGeneric>();
            go.GetComponent<Placeable>().interactable = true;
            wrapper.enabled = true;
            wrapper.toyDrawerSelector = this;
            gridGenerator.displayGrid = true;
        }
    }

    public void ToggleEnabled(bool enabled)
    {
        btn.enabled = enabled;
    }

}
