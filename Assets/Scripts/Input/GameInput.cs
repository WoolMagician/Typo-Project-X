// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""078a5360-85d3-470e-a12d-51ed791d5143"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8aba1d1c-a9ec-4a55-b9a8-e23faea16182"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4c30242d-b2e7-4aef-a03d-9a6555268c2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""22a5cb76-b777-4df6-ad59-fdcb4b6700c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""ee8cb490-46bc-4eef-8b4c-7c07a545ff5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""c7a91032-0699-4524-9cc5-fc524c66c229"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""1e3b20a0-b3ae-4c26-951d-20527e3d4b18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""36ebe1c2-1ee8-421b-ae0f-7328ffee3d96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f496c3be-9254-4594-9062-ef36a960ea2b"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47fc02e5-ab8d-413b-9dfd-836fb44139d7"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe5cc464-a330-4b0b-88ab-365a7736c975"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""034ebb86-1931-4e17-9c00-ddc4fdf5b1bf"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4410f841-0894-4e32-b48d-43fcdf9a0c61"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""0e5ab433-cf45-4f79-974a-0f6ed672513c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dde006e1-2fe9-4706-935b-729b60768bb7"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2a14b87f-12b7-441c-9d34-176e99c516c0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7e3dda90-001a-4d36-914a-c5dded4e2db0"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1d9afd4b-2886-4edb-9490-4b789e06607f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""49757474-2052-4547-824b-3b29eb0bd63d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogues"",
            ""id"": ""2271d2db-65c9-4f2f-827f-81b57430052f"",
            ""actions"": [
                {
                    ""name"": ""MoveSelection"",
                    ""type"": ""Button"",
                    ""id"": ""090d0555-4baf-48a2-9967-c9e615fbd4f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AdvanceDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""6e6faf0c-09ae-4235-87fb-085319dccfad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9459bb88-1dcc-47e8-a25f-eba06b4a9a54"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""956486e2-d136-42ed-a301-c5d3f7941141"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menus"",
            ""id"": ""b647e522-1b19-4314-af63-8e91dff3216b"",
            ""actions"": [
                {
                    ""name"": ""MoveSelection"",
                    ""type"": ""Value"",
                    ""id"": ""12dbb46c-dcbf-4444-8353-66e2c7ecf594"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""e623b70d-050e-4771-805e-11f3a76ce635"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""bc547574-fddf-481c-a3d4-655e049dbfbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""fd1c2fd2-78d6-4a69-920e-d73a0baef61b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""72ae1862-6d22-4bd1-af6d-078913cfd78c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0a841cdf-e433-418b-aea5-44b126d805bb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Unpause"",
                    ""type"": ""Button"",
                    ""id"": ""b0ed0c07-8d16-4186-8562-4aff31a3884c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeTab"",
                    ""type"": ""Button"",
                    ""id"": ""23f558d9-1c92-4aa9-bf75-eaa218e35ce9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InventoryActionButton"",
                    ""type"": ""Button"",
                    ""id"": ""7e569766-4c95-4a82-ae0e-ca857cac920f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SaveActionButton"",
                    ""type"": ""Button"",
                    ""id"": ""d7bd536b-a374-4339-9b24-4dfeaab328ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetActionButton"",
                    ""type"": ""Button"",
                    ""id"": ""212c531a-1267-4f55-984f-45339eb3acee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""7d1edb47-a874-4509-8820-9ac310faf79b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Button"",
                    ""id"": ""8bc3d1d1-4c74-4a50-a035-2ec3dd6ac146"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""371958e6-1fd5-4c5e-ba50-7b37b0d29e62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseInventory"",
                    ""type"": ""Button"",
                    ""id"": ""95a4bf3f-d5a6-4d92-b3c2-bcb247705492"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d7251914-6c90-4b30-a995-e037953f47b0"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63fb4f99-a584-4262-84c9-de025cddab1f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d18a3b83-0ca0-4f0e-81c3-2cc5925f7680"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b1e6b58-184a-47dc-bb17-69a8dff0fc55"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f6e1f35-b844-40d9-8e86-cfbfadff3b5b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62f604f3-8d7f-4cdb-9acc-2fc10d77080a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InventoryActionButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b98061e-cf67-487f-bbd8-56ddb77302a3"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaveActionButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1443508c-8690-453d-a3ce-dab340f35605"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetActionButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8bca61f-1e6a-4211-8d02-e2fda2842184"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41946fc9-9d06-4e55-b944-cbef7c6ed2d2"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3baeb19-7abe-4e95-9a40-3aef275c3f0f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ee15ae2-1990-4e04-95b3-b28d3d23775e"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99e0b205-76d7-4dce-807c-24ff5a2202eb"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46d8c124-d0c1-4653-96b3-c277e3cef8b7"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Unpause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df01fe97-ace4-4c2d-aec1-24fbce629908"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Unpause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2c161014-20fb-485d-9404-d1a2019d08e9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""99df14fe-979b-4d85-80b0-ee25aa9d48ff"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e27ddfc7-edfc-472f-878c-59814824359f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b028d959-1f31-49ba-883d-ac123cd21973"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""169500e6-4ec6-4992-8aa3-f38e099628de"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8bd11e60-d5e9-435b-b00a-f0008032c89b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""11a4128e-e084-459b-badb-fdb071e8f1a1"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1b26040c-0407-4faf-a8de-a1abe65a3928"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3efd6fa4-2b85-4381-8cb8-6d7745441c51"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8ff05123-2504-419d-b868-0ee6674dd8a6"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        m_Gameplay_OpenInventory = m_Gameplay.FindAction("OpenInventory", throwIfNotFound: true);
        m_Gameplay_Run = m_Gameplay.FindAction("Run", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        // Dialogues
        m_Dialogues = asset.FindActionMap("Dialogues", throwIfNotFound: true);
        m_Dialogues_MoveSelection = m_Dialogues.FindAction("MoveSelection", throwIfNotFound: true);
        m_Dialogues_AdvanceDialogue = m_Dialogues.FindAction("AdvanceDialogue", throwIfNotFound: true);
        // Menus
        m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
        m_Menus_MoveSelection = m_Menus.FindAction("MoveSelection", throwIfNotFound: true);
        m_Menus_Navigate = m_Menus.FindAction("Navigate", throwIfNotFound: true);
        m_Menus_Submit = m_Menus.FindAction("Submit", throwIfNotFound: true);
        m_Menus_Confirm = m_Menus.FindAction("Confirm", throwIfNotFound: true);
        m_Menus_Cancel = m_Menus.FindAction("Cancel", throwIfNotFound: true);
        m_Menus_MouseMove = m_Menus.FindAction("MouseMove", throwIfNotFound: true);
        m_Menus_Unpause = m_Menus.FindAction("Unpause", throwIfNotFound: true);
        m_Menus_ChangeTab = m_Menus.FindAction("ChangeTab", throwIfNotFound: true);
        m_Menus_InventoryActionButton = m_Menus.FindAction("InventoryActionButton", throwIfNotFound: true);
        m_Menus_SaveActionButton = m_Menus.FindAction("SaveActionButton", throwIfNotFound: true);
        m_Menus_ResetActionButton = m_Menus.FindAction("ResetActionButton", throwIfNotFound: true);
        m_Menus_Click = m_Menus.FindAction("Click", throwIfNotFound: true);
        m_Menus_Point = m_Menus.FindAction("Point", throwIfNotFound: true);
        m_Menus_RightClick = m_Menus.FindAction("RightClick", throwIfNotFound: true);
        m_Menus_CloseInventory = m_Menus.FindAction("CloseInventory", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Attack;
    private readonly InputAction m_Gameplay_Interact;
    private readonly InputAction m_Gameplay_OpenInventory;
    private readonly InputAction m_Gameplay_Run;
    private readonly InputAction m_Gameplay_Pause;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputAction @OpenInventory => m_Wrapper.m_Gameplay_OpenInventory;
        public InputAction @Run => m_Wrapper.m_Gameplay_Run;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @OpenInventory.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenInventory;
                @Run.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Dialogues
    private readonly InputActionMap m_Dialogues;
    private IDialoguesActions m_DialoguesActionsCallbackInterface;
    private readonly InputAction m_Dialogues_MoveSelection;
    private readonly InputAction m_Dialogues_AdvanceDialogue;
    public struct DialoguesActions
    {
        private @GameInput m_Wrapper;
        public DialoguesActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveSelection => m_Wrapper.m_Dialogues_MoveSelection;
        public InputAction @AdvanceDialogue => m_Wrapper.m_Dialogues_AdvanceDialogue;
        public InputActionMap Get() { return m_Wrapper.m_Dialogues; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialoguesActions set) { return set.Get(); }
        public void SetCallbacks(IDialoguesActions instance)
        {
            if (m_Wrapper.m_DialoguesActionsCallbackInterface != null)
            {
                @MoveSelection.started -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnMoveSelection;
                @MoveSelection.performed -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnMoveSelection;
                @MoveSelection.canceled -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnMoveSelection;
                @AdvanceDialogue.started -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnAdvanceDialogue;
                @AdvanceDialogue.performed -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnAdvanceDialogue;
                @AdvanceDialogue.canceled -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnAdvanceDialogue;
            }
            m_Wrapper.m_DialoguesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveSelection.started += instance.OnMoveSelection;
                @MoveSelection.performed += instance.OnMoveSelection;
                @MoveSelection.canceled += instance.OnMoveSelection;
                @AdvanceDialogue.started += instance.OnAdvanceDialogue;
                @AdvanceDialogue.performed += instance.OnAdvanceDialogue;
                @AdvanceDialogue.canceled += instance.OnAdvanceDialogue;
            }
        }
    }
    public DialoguesActions @Dialogues => new DialoguesActions(this);

    // Menus
    private readonly InputActionMap m_Menus;
    private IMenusActions m_MenusActionsCallbackInterface;
    private readonly InputAction m_Menus_MoveSelection;
    private readonly InputAction m_Menus_Navigate;
    private readonly InputAction m_Menus_Submit;
    private readonly InputAction m_Menus_Confirm;
    private readonly InputAction m_Menus_Cancel;
    private readonly InputAction m_Menus_MouseMove;
    private readonly InputAction m_Menus_Unpause;
    private readonly InputAction m_Menus_ChangeTab;
    private readonly InputAction m_Menus_InventoryActionButton;
    private readonly InputAction m_Menus_SaveActionButton;
    private readonly InputAction m_Menus_ResetActionButton;
    private readonly InputAction m_Menus_Click;
    private readonly InputAction m_Menus_Point;
    private readonly InputAction m_Menus_RightClick;
    private readonly InputAction m_Menus_CloseInventory;
    public struct MenusActions
    {
        private @GameInput m_Wrapper;
        public MenusActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveSelection => m_Wrapper.m_Menus_MoveSelection;
        public InputAction @Navigate => m_Wrapper.m_Menus_Navigate;
        public InputAction @Submit => m_Wrapper.m_Menus_Submit;
        public InputAction @Confirm => m_Wrapper.m_Menus_Confirm;
        public InputAction @Cancel => m_Wrapper.m_Menus_Cancel;
        public InputAction @MouseMove => m_Wrapper.m_Menus_MouseMove;
        public InputAction @Unpause => m_Wrapper.m_Menus_Unpause;
        public InputAction @ChangeTab => m_Wrapper.m_Menus_ChangeTab;
        public InputAction @InventoryActionButton => m_Wrapper.m_Menus_InventoryActionButton;
        public InputAction @SaveActionButton => m_Wrapper.m_Menus_SaveActionButton;
        public InputAction @ResetActionButton => m_Wrapper.m_Menus_ResetActionButton;
        public InputAction @Click => m_Wrapper.m_Menus_Click;
        public InputAction @Point => m_Wrapper.m_Menus_Point;
        public InputAction @RightClick => m_Wrapper.m_Menus_RightClick;
        public InputAction @CloseInventory => m_Wrapper.m_Menus_CloseInventory;
        public InputActionMap Get() { return m_Wrapper.m_Menus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
        public void SetCallbacks(IMenusActions instance)
        {
            if (m_Wrapper.m_MenusActionsCallbackInterface != null)
            {
                @MoveSelection.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnMoveSelection;
                @MoveSelection.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnMoveSelection;
                @MoveSelection.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnMoveSelection;
                @Navigate.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Submit.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSubmit;
                @Confirm.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnConfirm;
                @Cancel.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancel;
                @MouseMove.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnMouseMove;
                @MouseMove.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnMouseMove;
                @MouseMove.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnMouseMove;
                @Unpause.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnUnpause;
                @Unpause.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnUnpause;
                @Unpause.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnUnpause;
                @ChangeTab.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeTab;
                @ChangeTab.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeTab;
                @ChangeTab.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeTab;
                @InventoryActionButton.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnInventoryActionButton;
                @InventoryActionButton.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnInventoryActionButton;
                @InventoryActionButton.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnInventoryActionButton;
                @SaveActionButton.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSaveActionButton;
                @SaveActionButton.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSaveActionButton;
                @SaveActionButton.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSaveActionButton;
                @ResetActionButton.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnResetActionButton;
                @ResetActionButton.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnResetActionButton;
                @ResetActionButton.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnResetActionButton;
                @Click.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnClick;
                @Point.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnPoint;
                @RightClick.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnRightClick;
                @CloseInventory.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnCloseInventory;
                @CloseInventory.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnCloseInventory;
                @CloseInventory.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnCloseInventory;
            }
            m_Wrapper.m_MenusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveSelection.started += instance.OnMoveSelection;
                @MoveSelection.performed += instance.OnMoveSelection;
                @MoveSelection.canceled += instance.OnMoveSelection;
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @MouseMove.started += instance.OnMouseMove;
                @MouseMove.performed += instance.OnMouseMove;
                @MouseMove.canceled += instance.OnMouseMove;
                @Unpause.started += instance.OnUnpause;
                @Unpause.performed += instance.OnUnpause;
                @Unpause.canceled += instance.OnUnpause;
                @ChangeTab.started += instance.OnChangeTab;
                @ChangeTab.performed += instance.OnChangeTab;
                @ChangeTab.canceled += instance.OnChangeTab;
                @InventoryActionButton.started += instance.OnInventoryActionButton;
                @InventoryActionButton.performed += instance.OnInventoryActionButton;
                @InventoryActionButton.canceled += instance.OnInventoryActionButton;
                @SaveActionButton.started += instance.OnSaveActionButton;
                @SaveActionButton.performed += instance.OnSaveActionButton;
                @SaveActionButton.canceled += instance.OnSaveActionButton;
                @ResetActionButton.started += instance.OnResetActionButton;
                @ResetActionButton.performed += instance.OnResetActionButton;
                @ResetActionButton.canceled += instance.OnResetActionButton;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @CloseInventory.started += instance.OnCloseInventory;
                @CloseInventory.performed += instance.OnCloseInventory;
                @CloseInventory.canceled += instance.OnCloseInventory;
            }
        }
    }
    public MenusActions @Menus => new MenusActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IDialoguesActions
    {
        void OnMoveSelection(InputAction.CallbackContext context);
        void OnAdvanceDialogue(InputAction.CallbackContext context);
    }
    public interface IMenusActions
    {
        void OnMoveSelection(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnMouseMove(InputAction.CallbackContext context);
        void OnUnpause(InputAction.CallbackContext context);
        void OnChangeTab(InputAction.CallbackContext context);
        void OnInventoryActionButton(InputAction.CallbackContext context);
        void OnSaveActionButton(InputAction.CallbackContext context);
        void OnResetActionButton(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnCloseInventory(InputAction.CallbackContext context);
    }
}
