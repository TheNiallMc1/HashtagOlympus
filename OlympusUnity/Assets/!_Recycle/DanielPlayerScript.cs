// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class DanielPlayerScript : MonoBehaviour
// {
//     public ConeAoE coneAoePrefab;
//     Camera mainCam;
//
//     private DanielTestingKeys testKeys;
//     private bool key1;
//     private bool key2;
//     private bool key3;
//
//     public bool leftClick;
//     public bool rightClick;
//
//     private Vector2 mousePosition;
//
//     public bool selectionModeActive = false;
//
//     // public Combatant.TargetType abilityHitCheck;
//     AbilityExampleST singleTargetAbility;
//     Combatant playerCombatant;
//
//     private void Awake()
//     {
//         testKeys = new DanielTestingKeys();
//         testKeys.Enable();
//
//         testKeys.TestKeys.TestKey1.started += ctx => key1 = true;
//         testKeys.TestKeys.TestKey2.started += ctx => key2 = true;
//         testKeys.TestKeys.TestKey3.started += ctx => key3 = true;
//         testKeys.TestKeys.LeftClick.started += ctx => leftClick = true;
//         testKeys.TestKeys.RightClick.started += ctx => rightClick = true;
//
//         testKeys.TestKeys.MousePosition.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();
//
//         testKeys.TestKeys.TestKey1.canceled -= ctx => key1 = false;
//         testKeys.TestKeys.TestKey2.canceled -= ctx => key2 = false;
//         testKeys.TestKeys.TestKey3.canceled -= ctx => key3 = false;
//         testKeys.TestKeys.LeftClick.canceled -= ctx => leftClick = false;
//         testKeys.TestKeys.RightClick.canceled -= ctx => rightClick = false;
//     }
//
//     // Start is called before the first frame update
//     void Start()
//     {
//         mainCam = Camera.main;
//         playerCombatant = transform.GetComponent<Combatant>();
//         singleTargetAbility = transform.GetComponent<AbilityExampleST>();
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (selectionModeActive)
//         {
//             GraphicAssist(singleTargetAbility);
//
//
//             if (rightClick)
//             {
//                 leftClick = false;
//                 rightClick = false;
//
//                 Ray ray = mainCam.ScreenPointToRay(mousePosition);
//                 RaycastHit hit;
//
//
//                 if (Physics.Raycast(ray, out hit, 100))
//                 {
//                     Combatant enemyCombatant = hit.transform.gameObject.GetComponent<Combatant>();
//                     if(enemyCombatant != null)
//                     {
//                         foreach (Combatant.eTargetType eTargetType in singleTargetAbility.abilityCanHit)
//                         {
//                             if (enemyCombatant.targetType == eTargetType)
//                             {
//                                 Debug.Log("Attacked " + hit.transform.gameObject.name);
//                                 enemyCombatant.TakeDamage(singleTargetAbility.damage + playerCombatant.attackDamage);
//                                 selectionModeActive = false;
//                             }
//                         }
//                     }
//                 }
//             }
//         }
//
//         if (key1)
//         {
//             key1 = false;
//             ConeAoE coneAoE = Instantiate(coneAoePrefab, transform.position, Quaternion.identity, transform);
//         }
//
//     }
//
//     private void GraphicAssist(AbilityExampleST STability)
//     {
//         // Makes things with this TargetType stand out visually
//     }
//
//     public void EnterSelectionMode()
//     {
//         leftClick = false;
//         rightClick = false;
//         selectionModeActive = true;
//
//         print("Selection mode activated");
//     }
// }
