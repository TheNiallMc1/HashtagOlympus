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
                    ""name"": ""LeftMouseClick"",
                    ""type"": ""Value"",
                    ""id"": ""4657c9f0-020d-4f00-8cea-252e97b36666"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightMouseClick"",
                    ""type"": ""Value"",
                    ""id"": ""b4af97b4-6f78-4d99-9846-fc2bccecd451"",
                    ""expectedControlType"": """",
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
                    ""action"": ""LeftMouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f4bd057-7fca-47bd-b3a1-799e3ebdfed5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMouseClick"",
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
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""248a7339-ad67-4075-9847-f5dc36802958"",
            ""actions"": [
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""539aec00-c7a3-44ce-a0ab-6923234fd568"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""a3ac163c-effc-491f-a1ae-9ff475bfe06c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Value"",
                    ""id"": ""7c3fb4a3-5b32-4abd-be93-ae2705e8fe98"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6176ad73-6378-436c-bde3-bda3ec6a281d"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b63d5ac3-6452-4935-a036-edea23187979"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2837f743-0ab8-49f1-aa04-31fbcc2fd798"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
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
        m_Movement_LeftMouseClick = m_Movement.FindAction("LeftMouseClick", throwIfNotFound: true);
        m_Movement_RightMouseClick = m_Movement.FindAction("RightMouseClick", throwIfNotFound: true);
        // GodSelection
        m_GodSelection = asset.FindActionMap("GodSelection", throwIfNotFound: true);
        m_GodSelection_CycleThroughGods = m_GodSelection.FindAction("CycleThroughGods", throwIfNotFound: true);
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_MousePos = m_Mouse.FindAction("MousePos", throwIfNotFound: true);
        m_Mouse_LeftClick = m_Mouse.FindAction("LeftClick", throwIfNotFound: true);
        m_Mouse_RightClick = m_Mouse.FindAction("RightClick", throwIfNotFound: true);
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
    private readonly InputAction m_Movement_LeftMouseClick;
    private readonly InputAction m_Movement_RightMouseClick;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftMouseClick => m_Wrapper.m_Movement_LeftMouseClick;
        public InputAction @RightMouseClick => m_Wrapper.m_Movement_RightMouseClick;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @LeftMouseClick.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnLeftMouseClick;
                @LeftMouseClick.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnLeftMouseClick;
                @LeftMouseClick.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnLeftMouseClick;
                @RightMouseClick.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRightMouseClick;
                @RightMouseClick.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRightMouseClick;
                @RightMouseClick.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRightMouseClick;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftMouseClick.started += instance.OnLeftMouseClick;
                @LeftMouseClick.performed += instance.OnLeftMouseClick;
                @LeftMouseClick.canceled += instance.OnLeftMouseClick;
                @RightMouseClick.started += instance.OnRightMouseClick;
                @RightMouseClick.performed += instance.OnRightMouseClick;
                @RightMouseClick.canceled += instance.OnRightMouseClick;
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

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_MousePos;
    private readonly InputAction m_Mouse_LeftClick;
    private readonly InputAction m_Mouse_RightClick;
    public struct MouseActions
    {
        private @PlayerControls m_Wrapper;
        public MouseActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePos => m_Wrapper.m_Mouse_MousePos;
        public InputAction @LeftClick => m_Wrapper.m_Mouse_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_Mouse_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @MousePos.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePos;
                @LeftClick.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftClick;
                @RightClick.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IMovementActions
    {
        void OnLeftMouseClick(InputAction.CallbackContext context);
        void OnRightMouseClick(InputAction.CallbackContext context);
    }
    public interface IGodSelectionActions
    {
        void OnCycleThroughGods(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnMousePos(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
    }
}
