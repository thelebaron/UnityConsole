using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using EventSystem = UnityEngine.EventSystems.EventSystem;

namespace Wenzil.Console
{
    /// <summary>
    /// Interface with UI toolkit
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ConsoleController))]
    public class ConsoleUI : MonoBehaviour, IScrollHandler
    {
        public event Action<bool> onToggleConsole;
        public event Action<string> onSubmitCommand;
        public event Action onClearConsole;

        // old ui
        public Scrollbar scrollbar;
        public Text outputText;
        public ScrollRect outputArea;
        public InputField inputField;

        
        // ui toolkit
        private ConsoleHistory consoleHistory;
        private UIDocument     uiDocument;
        private VisualElement  rootVisualElement;
        private TextField      textField;
        //private ListView       listView;
        private ScrollView       scrollView;
        private bool           isConsoleOpen;
        
        private void Awake()
        {
            uiDocument        = GetComponent<UIDocument>();
            rootVisualElement = uiDocument.rootVisualElement;
        
            textField             = rootVisualElement.Q<TextField>("console-inputfield");
            scrollView            = rootVisualElement.Q<ScrollView>("console-history-scrollview");
            //scrollView.Children() = 16;

            /*// Callback invoked when the user double clicks an item
            listView.onItemsChosen += Debug.Log;
            // Callback invoked when the user changes the selection inside the ListView
            listView.onSelectionChange += Debug.Log;
            */
            //listView.RegisterCallback<FocusEvent>(evt => ResizeListToItem());
            Off();
        }



        /// <summary>
        /// Opens or closes the console.
        /// </summary>
        public void ToggleConsole()
        {
            isConsoleOpen = !isConsoleOpen;
            if (isConsoleOpen)
            {
                On();
            }
            if (!isConsoleOpen)
            {
                Off();
            }
        }

        private void OnToggle(bool open)
        {
            /*Show(open);

            if (open)
                inputField.ActivateInputField();*/
            //else

            if (onToggleConsole != null)
                onToggleConsole(open);
        }
        
        /// <summary>
        /// What to do when the user wants to submit a command.
        /// </summary>
        public void OnSubmit(string input)
        {
            if (EventSystem.current.alreadySelecting) // if user selected something else, don't treat as a submit
                return;

            if (input.Length > 0)
            {
                if (onSubmitCommand != null)
                    onSubmitCommand(input);
                //scrollbar.value = 0;
                //ClearInput();
            }

            //inputField.ActivateInputField();
        }

        public void Submit(ConsoleController consoleController)
        {
            //inputField.panel.focusController.focusedElement == inputField;
            if (textField.panel.focusController.focusedElement  == textField)
            {
                if (textField.value != null)
                {
                    //consoleHistory.inputHistory.Add(textField.value);
                    consoleHistory.commandHistory.Add(textField.value);
                    consoleController.ExecuteCommand(textField.value);
                    textField.value = null;
                    //listView.Refresh();
                    //listView.ScrollToItem(consoleHistory.inputHistory.Count-1);
                }
            }
        }
        
        
        /// <summary>
        /// What to do when the user uses the scrollwheel while hovering the console input.
        /// </summary>
        public void OnScroll(PointerEventData eventData)
        {
            scrollbar.value += 0.08f * eventData.scrollDelta.y;
        }

        /// <summary>
        /// Displays the given message as a new entry in the console output.
        /// </summary>
        public void AddNewOutputLine(string line)
        {
            //outputText.text += Environment.NewLine + line;
            // todo figure out how to increase listview height
            var txt       = line;//Environment.NewLine + line;
            int numLines  = txt.Split('\n').Length;
            int newHeight = 16 * numLines;
            
            var itemHeight = newHeight > 16? newHeight : 16;
            var label      = MakeItem() as Label;
            var styleHeight      = label.style.height;
            styleHeight.value  = itemHeight;
            label.style.height = styleHeight;
            label.text         = line;
            scrollView.Add(label);
            
            consoleHistory.inputHistory.Add(line);/*Environment.NewLine +*/
            //scrollView.Refresh();
            //listView.ScrollToItem(consoleHistory.inputHistory.Count);
            Invoke(nameof(ScrollListDown), 0.2f);
            //scrollView.ScrollTo(label);
        }
        
        void ScrollListDown()
        {
            //listView.ScrollToItem(consoleHistory.inputHistory.Count);
            var children = scrollView.Children();
            var child    = children.Last();
            
            scrollView.ScrollTo(child);
        }


        /// <summary>
        /// Writes the given string into the console input, ready to be user submitted.
        /// </summary>
        public void SetInputText(string input) 
        {
            /*inputField.MoveTextStart(false);
            inputField.text = input;
            inputField.MoveTextEnd(false);*/
            textField.value = input;

        }

        private void On()
        {
            rootVisualElement.style.display = DisplayStyle.Flex;
            rootVisualElement.SetEnabled(true);
            textField.Focus();
            textField.SelectRange(0,0);
            textField.ElementAt(0).Focus();
            Invoke(nameof(ClearTextField),0.1f);
            Invoke(nameof(ScrollListDown), 0.2f);
        }

        void ClearTextField()
        {
            textField.value = null;
        }
        /*private void ResizeListToItem()
        {
            var item      = listView.selectedItem as Label;
            
            var txt       = item.text;
            int numLines  = txt.Split('\n').Length;
            int newHeight = 16 * numLines;
            listView.itemHeight = newHeight;
        }*/
        
        private void Off()
        {
            rootVisualElement.style.display = DisplayStyle.None;
            //listView.itemHeight             = 16;
            rootVisualElement.SetEnabled(false);
            textField.value = null;
        }
        
        private static VisualElement MakeItem()
        {
            var label = new Label();
            label.name = "history-item-label";
            var stylecolor = new StyleColor();
            stylecolor.value  = Color.cyan;
            label.style.color = stylecolor;
            //label.style.height = 12;
            return label;
        }

        
        public void BindHistory(ConsoleHistory consoleHistory)
        {
            // As the user scrolls through the list, the ListView object
            // will recycle elements created by the "makeItem"
            // and invoke the "bindItem" callback to associate
            // the element with the matching data item (specified as an index in the list)
            Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = consoleHistory.inputHistory[i];
        
            //var listView = container.Q<ListView>();
            /*listView.makeItem      = MakeItem;
            listView.bindItem      = bindItem;
            listView.itemsSource   = consoleHistory.inputHistory;
            listView.selectionType = SelectionType.Single;*/
            this.consoleHistory         = consoleHistory;
        }


    }
}