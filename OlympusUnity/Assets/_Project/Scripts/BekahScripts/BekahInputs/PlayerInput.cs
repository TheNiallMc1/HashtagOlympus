// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3ad19387-eeb2-4848-8ca6-3afadce386e2"",
            ""actions"": [
                {
                    ""name"": ""MouseXY"",
                    ""type"": ""Value"",
                    ""id"": ""4b3a0d82-545a-4ff4-bb05-2a51524b2757"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""218465bb-32d5-463a-aba0-dd0accb0da01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseDoubleClick"",
                    ""type"": ""Button"",
                    ""id"": ""d9436363-171f-4248-afca-40cf13926572"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap""
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""a7581608-5b17-4ccd-8e8a-80de795727cf"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""06f473fe-05b6-4877-ab16-23b15ecab5c6"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseXY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0196744a-2831-4d2e-9770-bd6baad3e333"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d45f6ed-7da8-4033-9ba4-c18f06856214"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDoubleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55035abc-7c8e-4c1b-bcde-e4706612378b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""8374cc9c-900f-4906-9073-d287e2017b7c"",
            ""actions"": [
                {
                    ""name"": ""MoveCameraUp"",
                    ""type"": ""Button"",
                    ""id"": ""4acd0f3f-b47e-494e-90a7-37c5b89f315e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCameraDown"",
                    ""type"": ""Button"",
                    ""id"": ""a32aa7cf-d735-4e47-85d1-2f4fff4f9a3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCameraLeft"",
                    ""type"": ""Button"",
                    ""id"": ""6f56b887-9e63-4e42-94ef-04facf5168f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCameraRight"",
                    ""type"": ""Button"",
                    ""id"": ""c48c3ac6-79c9-42ee-9514-15db8ca87c71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraLeft"",
                    ""type"": ""Button"",
                    ""id"": ""8348575a-677d-40e7-b2b1-e8a448d7f303"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraRight"",
                    ""type"": ""Button"",
                    ""id"": ""78a97164-2357-4108-a759-bfce3aa3b3ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomOut"",
                    ""type"": ""Button"",
                    ""id"": ""ba3601de-031e-406f-9b3d-673bf4215d2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomIn"",
                    ""type"": ""Button"",
                    ""id"": ""30e6f811-49fd-48b3-a1d7-fa1cf9135124"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1ce368cf-3d58-4bce-8106-3d20a5c6ee59"",
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
                    ""id"": ""45487597-1c98-426b-936e-e3722acca94c"",
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
                    ""id"": ""70c8a466-63ef-4c92-b342-288736600610"",
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
                    ""id"": ""504d36ac-0501-4748-9493-06bfcd861e8b"",
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
                    ""id"": ""db4d78f6-4cd0-4459-a5bd-b3d4f380ee92"",
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
                    ""id"": ""efb46589-f5ef-4c49-b558-126948e972d1"",
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
                    ""id"": ""de3ae54c-5d3f-4f96-a725-c8400a45d5c5"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6932a04-a8fc-429a-8c07-03eccc7c27d5"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TestingAbilities"",
            ""id"": ""de61b2b7-40ab-4100-9624-64df537df3be"",
            ""actions"": [
                {
                    ""name"": ""UseAbility"",
                    ""type"": ""Value"",
                    ""id"": ""4e235b85-06c4-449f-a726-756057990e39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""67b7a6b4-3138-4159-81b9-be0331932d16"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlacementManager"",
            ""id"": ""aa886c8e-fe19-472a-a735-aaf82a771c04"",
            ""actions"": [
                {
                    ""name"": ""LeftCick"",
                    ""type"": ""Button"",
                    ""id"": ""690ef0fd-993f-4f06-9c49-5f7d169d3a11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Button"",
                    ""id"": ""e7c0fa22-19ac-4d22-840e-703991489294"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3bb74115-8516-427f-8ae4-04b09406cf1a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftCick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9385ceb4-f3b0-4487-89f3-a6385a006dd2"",
                    ""path"": ""<Mouse>/forwardButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MouseXY = m_Player.FindAction("MouseXY", throwIfNotFound: true);
        m_Player_MouseClick = m_Player.FindAction("MouseClick", throwIfNotFound: true);
        m_Player_MouseDoubleClick = m_Player.FindAction("MouseDoubleClick", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MoveCameraUp = m_Camera.FindAction("MoveCameraUp", throwIfNotFound: true);
        m_Camera_MoveCameraDown = m_Camera.FindAction("MoveCameraDown", throwIfNotFound: true);
        m_Camera_MoveCameraLeft = m_Camera.FindAction("MoveCameraLeft", throwIfNotFound: true);
        m_Camera_MoveCameraRight = m_Camera.FindAction("MoveCameraRight", throwIfNotFound: true);
        m_Camera_RotateCameraLeft = m_Camera.FindAction("RotateCameraLeft", throwIfNotFound: true);
        m_Camera_RotateCameraRight = m_Camera.FindAction("RotateCameraRight", throwIfNotFound: true);
        m_Camera_ZoomOut = m_Camera.FindAction("ZoomOut", throwIfNotFound: true);
        m_Camera_ZoomIn = m_Camera.FindAction("ZoomIn", throwIfNotFound: true);
        // TestingAbilities
        m_TestingAbilities = asset.FindActionMap("TestingAbilities", throwIfNotFound: true);
        m_TestingAbilities_UseAbility = m_TestingAbilities.FindAction("UseAbility", throwIfNotFound: true);
        // PlacementManager
        m_PlacementManager = asset.FindActionMap("PlacementManager", throwIfNotFound: true);
        m_PlacementManager_LeftCick = m_PlacementManager.FindAction("LeftCick", throwIfNotFound: true);
        m_PlacementManager_MousePosition = m_PlacementManager.FindAction("MousePosition", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MouseXY;
    private readonly InputAction m_Player_MouseClick;
    private readonly InputAction m_Player_MouseDoubleClick;
    private readonly InputAction m_Player_MousePosition;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseXY => m_Wrapper.m_Player_MouseXY;
        public InputAction @MouseClick => m_Wrapper.m_Player_MouseClick;
        public InputAction @MouseDoubleClick => m_Wrapper.m_Player_MouseDoubleClick;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MouseXY.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseXY;
                @MouseXY.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseXY;
                @MouseXY.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseXY;
                @MouseClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseDoubleClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseDoubleClick;
                @MouseDoubleClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseDoubleClick;
                @MouseDoubleClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseDoubleClick;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseXY.started += instance.OnMouseXY;
                @MouseXY.performed += instance.OnMouseXY;
                @MouseXY.canceled += instance.OnMouseXY;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @MouseDoubleClick.started += instance.OnMouseDoubleClick;
                @MouseDoubleClick.performed += instance.OnMouseDoubleClick;
                @MouseDoubleClick.canceled += instance.OnMouseDoubleClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MoveCameraUp;
    private readonly InputAction m_Camera_MoveCameraDown;
    private readonly InputAction m_Camera_MoveCameraLeft;
    private readonly InputAction m_Camera_MoveCameraRight;
    private readonly InputAction m_Camera_RotateCameraLeft;
    private readonly InputAction m_Camera_RotateCameraRight;
    private readonly InputAction m_Camera_ZoomOut;
    private readonly InputAction m_Camera_ZoomIn;
    public struct CameraActions
    {
        private @PlayerInput m_Wrapper;
        public CameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCameraUp => m_Wrapper.m_Camera_MoveCameraUp;
        public InputAction @MoveCameraDown => m_Wrapper.m_Camera_MoveCameraDown;
        public InputAction @MoveCameraLeft => m_Wrapper.m_Camera_MoveCameraLeft;
        public InputAction @MoveCameraRight => m_Wrapper.m_Camera_MoveCameraRight;
        public InputAction @RotateCameraLeft => m_Wrapper.m_Camera_RotateCameraLeft;
        public InputAction @RotateCameraRight => m_Wrapper.m_Camera_RotateCameraRight;
        public InputAction @ZoomOut => m_Wrapper.m_Camera_ZoomOut;
        public InputAction @ZoomIn => m_Wrapper.m_Camera_ZoomIn;
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
                @ZoomOut.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoomOut;
                @ZoomOut.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoomOut;
                @ZoomOut.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoomOut;
                @ZoomIn.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoomIn;
                @ZoomIn.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoomIn;
                @ZoomIn.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoomIn;
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
                @ZoomOut.started += instance.OnZoomOut;
                @ZoomOut.performed += instance.OnZoomOut;
                @ZoomOut.canceled += instance.OnZoomOut;
                @ZoomIn.started += instance.OnZoomIn;
                @ZoomIn.performed += instance.OnZoomIn;
                @ZoomIn.canceled += instance.OnZoomIn;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // TestingAbilities
    private readonly InputActionMap m_TestingAbilities;
    private ITestingAbilitiesActions m_TestingAbilitiesActionsCallbackInterface;
    private readonly InputAction m_TestingAbilities_UseAbility;
    public struct TestingAbilitiesActions
    {
        private @PlayerInput m_Wrapper;
        public TestingAbilitiesActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @UseAbility => m_Wrapper.m_TestingAbilities_UseAbility;
        public InputActionMap Get() { return m_Wrapper.m_TestingAbilities; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestingAbilitiesActions set) { return set.Get(); }
        public void SetCallbacks(ITestingAbilitiesActions instance)
        {
            if (m_Wrapper.m_TestingAbilitiesActionsCallbackInterface != null)
            {
                @UseAbility.started -= m_Wrapper.m_TestingAbilitiesActionsCallbackInterface.OnUseAbility;
                @UseAbility.performed -= m_Wrapper.m_TestingAbilitiesActionsCallbackInterface.OnUseAbility;
                @UseAbility.canceled -= m_Wrapper.m_TestingAbilitiesActionsCallbackInterface.OnUseAbility;
            }
            m_Wrapper.m_TestingAbilitiesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UseAbility.started += instance.OnUseAbility;
                @UseAbility.performed += instance.OnUseAbility;
                @UseAbility.canceled += instance.OnUseAbility;
            }
        }
    }
    public TestingAbilitiesActions @TestingAbilities => new TestingAbilitiesActions(this);

    // PlacementManager
    private readonly InputActionMap m_PlacementManager;
    private IPlacementManagerActions m_PlacementManagerActionsCallbackInterface;
    private readonly InputAction m_PlacementManager_LeftCick;
    private readonly InputAction m_PlacementManager_MousePosition;
    public struct PlacementManagerActions
    {
        private @PlayerInput m_Wrapper;
        public PlacementManagerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftCick => m_Wrapper.m_PlacementManager_LeftCick;
        public InputAction @MousePosition => m_Wrapper.m_PlacementManager_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_PlacementManager; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlacementManagerActions set) { return set.Get(); }
        public void SetCallbacks(IPlacementManagerActions instance)
        {
            if (m_Wrapper.m_PlacementManagerActionsCallbackInterface != null)
            {
                @LeftCick.started -= m_Wrapper.m_PlacementManagerActionsCallbackInterface.OnLeftCick;
                @LeftCick.performed -= m_Wrapper.m_PlacementManagerActionsCallbackInterface.OnLeftCick;
                @LeftCick.canceled -= m_Wrapper.m_PlacementManagerActionsCallbackInterface.OnLeftCick;
                @MousePosition.started -= m_Wrapper.m_PlacementManagerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlacementManagerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlacementManagerActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlacementManagerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftCick.started += instance.OnLeftCick;
                @LeftCick.performed += instance.OnLeftCick;
                @LeftCick.canceled += instance.OnLeftCick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlacementManagerActions @PlacementManager => new PlacementManagerActions(this);
    public interface IPlayerActions
    {
        void OnMouseXY(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnMouseDoubleClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMoveCameraUp(InputAction.CallbackContext context);
        void OnMoveCameraDown(InputAction.CallbackContext context);
        void OnMoveCameraLeft(InputAction.CallbackContext context);
        void OnMoveCameraRight(InputAction.CallbackContext context);
        void OnRotateCameraLeft(InputAction.CallbackContext context);
        void OnRotateCameraRight(InputAction.CallbackContext context);
        void OnZoomOut(InputAction.CallbackContext context);
        void OnZoomIn(InputAction.CallbackContext context);
    }
    public interface ITestingAbilitiesActions
    {
        void OnUseAbility(InputAction.CallbackContext context);
    }
    public interface IPlacementManagerActions
    {
        void OnLeftCick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
