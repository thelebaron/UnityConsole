using thelebaron.console;
using thelebaron.console.Commands;
using UnityEngine;

namespace DefaultNamespace
{
    public class AddCommandTest : MonoBehaviour
    {
        void Start() 
        {
            ConsoleCommandsDatabase.RegisterCommand(TestCommand.name, TestCommand.description, TestCommand.usage, TestCommand.Execute);
        }
    }
}