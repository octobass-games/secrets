using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public TMP_Text Name;
    public Button Button;
    public Image Image;

    public void Setup(ItemDefinition definition, Action<ItemDefinition> onSelection)
    {
        Name.text = definition.Name;
        Image.sprite = definition.Sprite;
        Button.onClick.AddListener(() => onSelection(definition));
    }
}
