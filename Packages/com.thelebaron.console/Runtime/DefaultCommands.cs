using thelebaron.console.Commands;
using UnityEngine;

namespace thelebaron.console
{ 
    public class DefaultCommands : MonoBehaviour
    {
        void Start() 
        {
            ConsoleCommandsDatabase.RegisterCommand(QuitCommand.name, QuitCommand.description, QuitCommand.usage, QuitCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(HelpCommand.name, HelpCommand.description, HelpCommand.usage, HelpCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(LoadCommand.name, LoadCommand.description, LoadCommand.usage, LoadCommand.Execute);
        }
    }
}
