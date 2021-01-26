// GENERATED AUTOMATICALLY FROM 'Assets/_Project/Inputs/CameraControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CameraControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraControls"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""a121b79a-241a-465d-9091-b5ccb4486304"",
            ""actions"": [
                {
                    ""name"": ""MoveCameraUp"",
                    ""type"": ""Button"",
                    ""id"": ""1aa1ab86-2f36-4411-9c5b-1968d836112b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCameraDown"",
                    ""type"": ""Button"",
                    ""id"": ""49e6cc5b-5de7-44a3-9ec8-2116e4ce18e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCameraLeft"",
                    ""type"": ""Button"",
                    ""id"": ""164d925a-70c6-4567-94a2-e54cd65457b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCameraRight"",
                    ""type"": ""Button"",
                    ""id"": ""afcfa294-e82c-44fa-9840-40fabdc71de1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraLeft"",
                    ""type"": ""Button"",
                    ""id"": ""6d00df04-e664-407e-af2f-30a546237ca2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraRight"",
                    ""type"": ""Button"",
                    ""id"": ""5b0adafa-870f-4ea9-b6cc-92cee3cdab7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseScrollY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""40f8dcc8-9e9c-447f-a92c-ebfb19149603"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuickFocus"",
                    ""type"": ""Button"",
                    ""id"": ""9ccce6f2-0fcb-4a3e-826c-d695aa66ed06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9e7dbd9c-22b9-4cd4-a15c-05275e754344"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38903331-8a66-4751-873b-98e11105d51f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da8da05b-2c03-46c4-bc88-bafa2c59d954"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d453a7e-0838-4fbd-be4b-22ec2c49d788"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""760075e0-9066-4af1-a1d9-7c3536a922e3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9054db28-3f2e-4772-bab9-e67b2c906593"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""583599a1-6640-4796-808a-4ce3772691df"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e2644f9-3f97-46b8-b382-49a0fca2f53a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCameraRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88e58581-b2f0-4f32-84a4-9801dd70b2ee"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64a6f997-c42b-489d-b9a9-f2702c8ee2e6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""119d50bd-3532-4e46-bc1a-82ef446091c1"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c76810f-267d-4a76-a60f-c7e132d8694c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickFocus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MoveCameraUp = m_Camera.FindAction("MoveCameraUp", throwIfNotFound: true);
        m_Camera_MoveCameraDown = m_Camera.FindAction("MoveCameraDown", throwIfNotFound: true);
        m_Camera_MoveCameraLeft = m_Camera.FindAction("MoveCameraLeft", throwIfNotFound: true);
        m_Camera_MoveCameraRight = m_Camera.FindAction("MoveCameraRight", throwIfNotFound: true);
        m_Camera_RotateCameraLeft = m_Camera.FindAction("RotateCameraLeft", throwIfNotFound: true);
        m_Camera_RotateCameraRight = m_Camera.FindAction("RotateCameraRight", throwIfNotFound: true);
        m_Camera_MouseScrollY = m_Camera.FindAction("MouseScrollY", throwIfNotFound: true);
        m_Camera_QuickFocus = m_Camera.FindAction("QuickFocus", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MoveCameraUp;
    private readonly InputAction m_Camera_MoveCameraDown;
    private readonly InputAction m_Camera_MoveCameraLeft;
    private readonly InputAction m_Camera_MoveCameraRight;
    private readonly InputAction m_Camera_RotateCameraLeft;
    private readonly InputAction m_Camera_RotateCameraRight;
    private readonly InputAction m_Camera_MouseScrollY;
    private readonly InputAction m_Camera_QuickFocus;
    public struct CameraActions
    {
        private @CameraControls m_Wrapper;
        public CameraActions(@CameraControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCameraUp => m_Wrapper.m_Camera_MoveCameraUp;
        public InputAction @MoveCameraDown => m_Wrapper.m_Camera_MoveCameraDown;
        public InputAction @MoveCameraLeft => m_Wrapper.m_Camera_MoveCameraLeft;
        public InputAction @MoveCameraRight => m_Wrapper.m_Camera_MoveCameraRight;
        public InputAction @RotateCameraLeft => m_Wrapper.m_Camera_RotateCameraLeft;
        public InputAction @RotateCameraRight => m_Wrapper.m_Camera_RotateCameraRight;
        public InputAction @MouseScrollY => m_Wrapper.m_Camera_MouseScrollY;
        public InputAction @QuickFocus => m_Wrapper.m_Camera_QuickFocus;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @MoveCameraUp.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraUp;
                @MoveCameraUp.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraUp;
                @MoveCameraUp.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraUp;
                @MoveCameraDown.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraDown;
                @MoveCameraDown.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraDown;
                @MoveCameraDown.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraDown;
                @MoveCameraLeft.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraLeft;
                @MoveCameraLeft.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraLeft;
                @MoveCameraLeft.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraLeft;
                @MoveCameraRight.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraRight;
                @MoveCameraRight.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraRight;
                @MoveCameraRight.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCameraRight;
                @RotateCameraLeft.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraLeft;
                @RotateCameraLeft.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraLeft;
                @RotateCameraLeft.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraLeft;
                @RotateCameraRight.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraRight;
                @RotateCameraRight.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraRight;
                @RotateCameraRight.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraRight;
                @MouseScrollY.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseScrollY;
                @MouseScrollY.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseScrollY;
                @MouseScrollY.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseScrollY;
                @QuickFocus.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnQuickFocus;
                @QuickFocus.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnQuickFocus;
                @QuickFocus.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnQuickFocus;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCameraUp.started += instance.OnMoveCameraUp;
                @MoveCameraUp.performed += instance.OnMoveCameraUp;
                @MoveCameraUp.canceled += instance.OnMoveCameraUp;
                @MoveCameraDown.started += instance.OnMoveCameraDown;
                @MoveCameraDown.performed += instance.OnMoveCameraDown;
                @MoveCameraDown.canceled += instance.OnMoveCameraDown;
                @MoveCameraLeft.started += instance.OnMoveCameraLeft;
                @MoveCameraLeft.performed += instance.OnMoveCameraLeft;
                @MoveCameraLeft.canceled += instance.OnMoveCameraLeft;
                @MoveCameraRight.started += instance.OnMoveCameraRight;
                @MoveCameraRight.performed += instance.OnMoveCameraRight;
                @MoveCameraRight.canceled += instance.OnMoveCameraRight;
                @RotateCameraLeft.started += instance.OnRotateCameraLeft;
                @RotateCameraLeft.performed += instance.OnRotateCameraLeft;
                @RotateCameraLeft.canceled += instance.OnRotateCameraLeft;
                @RotateCameraRight.started += instance.OnRotateCameraRight;
                @RotateCameraRight.performed += instance.OnRotateCameraRight;
                @RotateCameraRight.canceled += instance.OnRotateCameraRight;
                @MouseScrollY.started += instance.OnMouseScrollY;
                @MouseScrollY.performed += instance.OnMouseScrollY;
                @MouseScrollY.canceled += instance.OnMouseScrollY;
                @QuickFocus.started += instance.OnQuickFocus;
                @QuickFocus.performed += instance.OnQuickFocus;
                @QuickFocus.canceled += instance.OnQuickFocus;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface ICameraActions
    {
        void OnMoveCameraUp(InputAction.CallbackContext context);
        void OnMoveCameraDown(InputAction.CallbackContext context);
        void OnMoveCameraLeft(InputAction.CallbackContext context);
        void OnMoveCameraRight(InputAction.CallbackContext context);
        void OnRotateCameraLeft(InputAction.CallbackContext context);
        void OnRotateCameraRight(InputAction.CallbackContext context);
        void OnMouseScrollY(InputAction.CallbackContext context);
        void OnQuickFocus(InputAction.CallbackContext context);
    }
}
