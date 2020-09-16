using UnityEngine;
using System.Collections;
using thelebaron.console.Commands;

/// <summary>
/// LOAD command. Load the specified scene by name.
/// </summary>

public static class TestCommand
{
    public static readonly string name = "Test";
    public static readonly string description = "Logs a test phrase to the console.";
    public static readonly string usage = "Test";

    public static string Execute(params string[] args)
    {
        var objs = UnityEngine.Object.FindObjectOfType(typeof(Camera));
        var cam  = objs as Camera;
        
        Debug.Log(cam);
        
        Debug.Log("All work and no play makes Jack a dull boy.");
        
        return "Testing... All work and no play makes Jack a dull boy.";
    }


}
