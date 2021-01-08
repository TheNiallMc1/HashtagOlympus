// GENERATED AUTOMATICALLY FROM 'Assets/_Project/Inputs/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""ca1aa8c8-2a25-456d-82d1-c7aef4d60899"",
            ""actions"": [
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Value"",
                    ""id"": ""4657c9f0-020d-4f00-8cea-252e97b36666"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9b57d5f4-cb26-41b3-9dac-75b2be82bdf1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GodSelection"",
            ""id"": ""ff4b3db5-0ce3-411b-b2c0-ca7552ae84c3"",
            ""actions"": [
                {
                    ""name"": ""CycleThroughGods"",
                    ""type"": ""Button"",
                    ""id"": ""c8ac0f64-bf9d-408c-bc39-dc2de775c593"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b41a8fc-81fb-45b4-8c7a-8fdf2df1ba25"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleThroughGods"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_MouseClick = m_Movement.FindAction("MouseClick", throwIfNotFound: true);
        // GodSelection
        m_GodSelection = asset.FindActionMap("GodSelection", throwIfNotFound: true);
        m_GodSelection_CycleThroughGods = m_GodSelection.FindAction("CycleThroughGods", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_MouseClick;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseClick => m_Wrapper.m_Movement_MouseClick;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @MouseClick.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseClick;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // GodSelection
    private readonly InputActionMap m_GodSelection;
    private IGodSelectionActions m_GodSelectionActionsCallbackInterface;
    private readonly InputAction m_GodSelection_CycleThroughGods;
    public struct GodSelectionActions
    {
        private @PlayerControls m_Wrapper;
        public GodSelectionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CycleThroughGods => m_Wrapper.m_GodSelection_CycleThroughGods;
        public InputActionMap Get() { return m_Wrapper.m_GodSelection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GodSelectionActions set) { return set.Get(); }
        public void SetCallbacks(IGodSelectionActions instance)
        {
            if (m_Wrapper.m_GodSelectionActionsCallbackInterface != null)
            {
                @CycleThroughGods.started -= m_Wrapper.m_GodSelectionActionsCallbackInterface.OnCycleThroughGods;
                @CycleThroughGods.performed -= m_Wrapper.m_GodSelectionActionsCallbackInterface.OnCycleThroughGods;
                @CycleThroughGods.canceled -= m_Wrapper.m_GodSelectionActionsCallbackInterface.OnCycleThroughGods;
            }
            m_Wrapper.m_GodSelectionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CycleThroughGods.started += instance.OnCycleThroughGods;
                @CycleThroughGods.performed += instance.OnCycleThroughGods;
                @CycleThroughGods.canceled += instance.OnCycleThroughGods;
            }
        }
    }
    public GodSelectionActions @GodSelection => new GodSelectionActions(this);
    public interface IMovementActions
    {
        void OnMouseClick(InputAction.CallbackContext context);
    }
    public interface IGodSelectionActions
    {
        void OnCycleThroughGods(InputAction.CallbackContext context);
    }
}
