using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/// <summary>
/// this is for testing purposes only, remove later
/// </summary>
[Obsolete]
public class UConsole : MonoBehaviour
{
    
    // ui toolkit
    private TextField inputField;
    private ListView  listView;
    //private inputfie prefabScale;
    //
    private UIDocument    uiDocument;
    private VisualElement rootVisualElement;

    public List<string> items;
    private static VisualElement MakeItem()
    {
        var label = new Label();
        label.name = "history-item-label";
        var stylecolor = new StyleColor();
        stylecolor.value   = Color.cyan;
        label.style.color  = stylecolor;
        //label.style.height = 12;
        return label;
    }



    private void OnEnable()
    {
        uiDocument        = GetComponent<UIDocument>();
        rootVisualElement = uiDocument.rootVisualElement;
        
        inputField          = rootVisualElement.Q<TextField>("console-inputfield");
        listView            = rootVisualElement.Q<ListView>("console-history-listview");
        listView.itemHeight = 16;
        // Create some list of data, here simply numbers in interval [1, 1000]
        const int itemCount = 2;
        items     = new List<string>(itemCount);
        for (int i = 1; i <= itemCount; i++)
            items.Add("i.ToString()");

        // The "makeItem" function will be called as needed
        // when the ListView needs more items to render
        //Func<VisualElement> makeItem = () => new Label();
        //VisualElement MakeItem() => new Label();
        
        // As the user scrolls through the list, the ListView object
        // will recycle elements created by the "makeItem"
        // and invoke the "bindItem" callback to associate
        // the element with the matching data item (specified as an index in the list)
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];
        
        //var listView = container.Q<ListView>();
        listView.makeItem      = MakeItem;
        listView.bindItem      = bindItem;
        listView.itemsSource   = items;
        listView.selectionType = SelectionType.Single;
        
        /*// Callback invoked when the user double clicks an item
        listView.onItemsChosen += Debug.Log;
        // Callback invoked when the user changes the selection inside the ListView
        listView.onSelectionChange += Debug.Log;
        
        
        inputField.RegisterCallback<FocusEvent>(evt => logtext());*/
        //myElement.RegisterCallback<KeyDownEvent>((evt) => Debug.Log(evt.keyCode))
        //inputField.RegisterCallback<FocusInEvent>(evt => { Input.imeCompositionMode  = IMECompositionMode.On; });
        //inputField.RegisterCallback<FocusOutEvent>(evt => { Input.imeCompositionMode = IMECompositionMode.Auto; });
        // Mirror value of uxml field into the C# field.
        /*uxmlVector3Field.RegisterCallback<ChangeEvent<Vector3Field>>((evt) =>
        {
            spread = evt.newValue;
        });*/
    }

    void logtext()
    {
        Debug.Log("VAR");
    }

    private void Update()
    {


        if (Keyboard.current.enterKey.isPressed)
        {
            //inputField.panel.focusController.focusedElement == inputField;
            if (inputField.panel.focusController.focusedElement  == inputField)
            {
                if (inputField.value != null)
                {
                    items.Add(inputField.value);
                    //inputField.Clear();
                    inputField.value = null;
                    listView.Refresh();
                    listView.ScrollToItem(items.Count-1);
                }
            }
        }
        //inputField.Focus();
        
  
        /*
        if (inputField.text != null)
        {
            Debug.Log(inputField.text);
        }*/
    }
}
