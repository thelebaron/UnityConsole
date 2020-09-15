using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace thelebaron.console
{
    /// <summary>
    /// The behavior of the console.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ConsoleController))]
    public class ConsoleController : MonoBehaviour
    {
        private const int            inputHistoryCapacity = 20;
        public        ConsoleUI      ui;
        public        ConsoleHistory history = new ConsoleHistory(inputHistoryCapacity);

        private bool       enabled;
        private Timer timer;
        
        void Awake()
        {
            /* This instantiation causes a bug when Unity rebuilds the project while in play mode
               Solution: move it to class level initialization, and make inputHistoryCapacity a const */
            // inputHistory = new ConsoleInputHistory(inputHistoryCapacity); 
        }
        
        void OnEnable()
        {
            
            Console.OnConsoleLog += ui.AddNewOutputLine;
            ui.onSubmitCommand += ExecuteCommand;
            ui.onClearConsole += history.Clear;

            ui.BindHistory(history);
        }
        

        void OnDisable()
        {
            Console.OnConsoleLog -= ui.AddNewOutputLine;
            ui.onSubmitCommand -= ExecuteCommand;
            ui.onClearConsole -= history.Clear;
        }

        private void Update()
        {
            timer.Update(Time.deltaTime);

            if (Keyboard.current.backquoteKey.isPressed)
            {
                if (timer.CanToggle())
                {
                    timer.Block();
                    ui.ToggleConsole();
                }
            }

            
            if (Keyboard.current.enterKey.isPressed)
            {
                ui.Submit(this);
            }
            
            //else if (Input.GetKeyDown(KeyCode.Escape) && closeOnEscape)
                //ui.CloseConsole();
            else if (Keyboard.current.upArrowKey.isPressed)//(Input.GetKeyDown(KeyCode.UpArrow))
                NavigateInputHistory(true);
            else if (Keyboard.current.downArrowKey.isPressed)//(Input.GetKeyDown(KeyCode.DownArrow))
                NavigateInputHistory(false);
        }

        private void NavigateInputHistory(bool up)
        {
            // disable for now
            /*string navigatedToInput = history.Navigate(up);
            ui.SetInputText(navigatedToInput);*/
        }

        public void ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ');
            string command = parts[0];
            string[] args = parts.Skip(1).ToArray();
        
            Console.Log("> " + input);
            Console.Log(ConsoleCommandsDatabase.ExecuteCommand(command, args));
            history.AddNewInputEntry(input);
        }
        
        
        
        /// <summary>
        /// Timer to stop relentless toggling
        /// </summary>
        public struct Timer
        {
            public float Time;
        
            public void Update(float dt)
            {
                if(Time>0) Time -= dt;
            }

            /// <summary>
            /// Only true if the timer has rundown
            /// </summary>
            /// <returns></returns>
            public bool CanToggle()
            {
                return Time <= 0;
            }

            /// <summary>
            /// Set time to a value to stop input
            /// </summary>
            public void Block()
            {
                Time = 0.15f;
            }
        }
    }


}