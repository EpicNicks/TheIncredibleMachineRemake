using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public GameObject draggableWrapperPrefab;
    [SerializeField]
    private int amount = 3;
    public GridGenerator gridGenerator;
    public TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        if (text == null)
        {
            text = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
        text.text = amount.ToString();
    }

    public void OnClick()
    {
        if (Instantiated < amount)
        {
            Instantiated++;
            GameObject go = Instantiate(draggableWrapperPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)), Quaternion.identity);
            DraggableWrapperGeneric wrapper = go.GetComponent<DraggableWrapperGeneric>();
            wrapper.enabled = true;
            wrapper.toyDrawerSelector = this;
            gridGenerator.displayGrid = true;
        }
    }

}
