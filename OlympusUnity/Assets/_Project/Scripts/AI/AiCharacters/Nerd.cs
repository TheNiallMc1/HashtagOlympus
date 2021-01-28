﻿using _Project.Scripts.AI.AiControllers;
using System.Collections.Generic;
using UnityEngine;

public class Nerd : AIBrain
{
    private new readonly List<int> _autoAttackAnimations = new List<int>
        {
            AutoAttack01,
            AutoAttack02,
            AutoAttack03,
            AutoAttack04
        };

    protected void Start()
    {
        base._autoAttackAnimations = _autoAttackAnimations;

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
