// GENERATED AUTOMATICALLY FROM 'Assets/ToolkitStuff/ConsoleInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ConsoleInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ConsoleInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ConsoleInputActions"",
    ""maps"": [
        {
            ""name"": ""Console"",
            ""id"": ""73a388fa-3046-4968-8a16-94a165ad307c"",
            ""actions"": [
                {
                    ""name"": ""New action1"",
                    ""type"": ""Button"",
                    ""id"": ""c1b06771-2df9-4775-ad51-552e65319d78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""49d205f9-2238-43cf-9b49-937598e15f59"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Console
        m_Console = asset.FindActionMap("Console", throwIfNotFound: true);
        m_Console_Newaction1 = m_Console.FindAction("New action1", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Console
    private readonly InputActionMap m_Console;
    private IConsoleActions m_ConsoleActionsCallbackInterface;
    private readonly InputAction m_Console_Newaction1;
    public struct ConsoleActions
    {
        private @ConsoleInputActions m_Wrapper;
        public ConsoleActions(@ConsoleInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction1 => m_Wrapper.m_Console_Newaction1;
        public InputActionMap Get() { return m_Wrapper.m_Console; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ConsoleActions set) { return set.Get(); }
        public void SetCallbacks(IConsoleActions instance)
        {
            if (m_Wrapper.m_ConsoleActionsCallbackInterface != null)
            {
                @Newaction1.started -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnNewaction1;
                @Newaction1.performed -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnNewaction1;
                @Newaction1.canceled -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnNewaction1;
            }
            m_Wrapper.m_ConsoleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction1.started += instance.OnNewaction1;
                @Newaction1.performed += instance.OnNewaction1;
                @Newaction1.canceled += instance.OnNewaction1;
            }
        }
    }
    public ConsoleActions @Console => new ConsoleActions(this);
    public interface IConsoleActions
    {
        void OnNewaction1(InputAction.CallbackContext context);
    }
}
