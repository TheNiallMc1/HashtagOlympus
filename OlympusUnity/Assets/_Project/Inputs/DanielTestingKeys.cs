// GENERATED AUTOMATICALLY FROM 'Assets/_Project/Inputs/DanielTestingKeys.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DanielTestingKeys : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DanielTestingKeys()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DanielTestingKeys"",
    ""maps"": [
        {
            ""name"": ""TestKeys"",
            ""id"": ""3679c2b6-f7cf-4563-82d5-5d7d46df1087"",
            ""actions"": [
                {
                    ""name"": ""TestKey1"",
                    ""type"": ""Button"",
                    ""id"": ""86176d18-483e-4faa-8b0a-803ca58490bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestKey2"",
                    ""type"": ""Button"",
                    ""id"": ""129a0404-3a21-4c9a-a262-b4d19df3d8d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestKey3"",
                    ""type"": ""Button"",
                    ""id"": ""e58c6392-2396-4ec1-b517-57451922868b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fdb62a16-c235-4c0f-94dc-7452453d6864"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestKey1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3579b4a1-48e8-4ef1-9b90-300d08246544"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestKey2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c8c4978-ed6d-44a9-ba02-2431f636ca00"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestKey3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TestKeys
        m_TestKeys = asset.FindActionMap("TestKeys", throwIfNotFound: true);
        m_TestKeys_TestKey1 = m_TestKeys.FindAction("TestKey1", throwIfNotFound: true);
        m_TestKeys_TestKey2 = m_TestKeys.FindAction("TestKey2", throwIfNotFound: true);
        m_TestKeys_TestKey3 = m_TestKeys.FindAction("TestKey3", throwIfNotFound: true);
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

    // TestKeys
    private readonly InputActionMap m_TestKeys;
    private ITestKeysActions m_TestKeysActionsCallbackInterface;
    private readonly InputAction m_TestKeys_TestKey1;
    private readonly InputAction m_TestKeys_TestKey2;
    private readonly InputAction m_TestKeys_TestKey3;
    public struct TestKeysActions
    {
        private @DanielTestingKeys m_Wrapper;
        public TestKeysActions(@DanielTestingKeys wrapper) { m_Wrapper = wrapper; }
        public InputAction @TestKey1 => m_Wrapper.m_TestKeys_TestKey1;
        public InputAction @TestKey2 => m_Wrapper.m_TestKeys_TestKey2;
        public InputAction @TestKey3 => m_Wrapper.m_TestKeys_TestKey3;
        public InputActionMap Get() { return m_Wrapper.m_TestKeys; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestKeysActions set) { return set.Get(); }
        public void SetCallbacks(ITestKeysActions instance)
        {
            if (m_Wrapper.m_TestKeysActionsCallbackInterface != null)
            {
                @TestKey1.started -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey1;
                @TestKey1.performed -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey1;
                @TestKey1.canceled -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey1;
                @TestKey2.started -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey2;
                @TestKey2.performed -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey2;
                @TestKey2.canceled -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey2;
                @TestKey3.started -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey3;
                @TestKey3.performed -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey3;
                @TestKey3.canceled -= m_Wrapper.m_TestKeysActionsCallbackInterface.OnTestKey3;
            }
            m_Wrapper.m_TestKeysActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TestKey1.started += instance.OnTestKey1;
                @TestKey1.performed += instance.OnTestKey1;
                @TestKey1.canceled += instance.OnTestKey1;
                @TestKey2.started += instance.OnTestKey2;
                @TestKey2.performed += instance.OnTestKey2;
                @TestKey2.canceled += instance.OnTestKey2;
                @TestKey3.started += instance.OnTestKey3;
                @TestKey3.performed += instance.OnTestKey3;
                @TestKey3.canceled += instance.OnTestKey3;
            }
        }
    }
    public TestKeysActions @TestKeys => new TestKeysActions(this);
    public interface ITestKeysActions
    {
        void OnTestKey1(InputAction.CallbackContext context);
        void OnTestKey2(InputAction.CallbackContext context);
        void OnTestKey3(InputAction.CallbackContext context);
    }
}
