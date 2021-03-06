﻿/**
 * Thunder Aerospace Corporation's Life Support for Kerbal Space Program.
 * Originally Written by Taranis Elsu.
 * This version written and maintained by JPLRepo (Jamie Leighton)
 * 
 * (C) Copyright 2013, Taranis Elsu
 * (C) Copyright 2016, Jamie Leighton
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 * 
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 * 
 * Non-commercial - You may not use this work for commercial purposes.
 * 
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using UnityEngine;
using KSP.Localization;

namespace Tac
{
    class SpaceCenterManager : MonoBehaviour//, Savable
    {
        void Awake()
        {
            this.Log("Awake");
        }

        void Start()
        {
            this.Log("Start, new game = " + TacLifeSupport.Instance.gameSettings.IsNewSave);
            if (HighLogic.CurrentGame.Parameters.CustomParams<TAC_SettingsParms>().enabled)
            {
                //Disabled this Pop-Up - Think most players known by now.
                /*if (TacLifeSupport.Instance.gameSettings.IsNewSave)
                {
                    this.Log("New save detected!");
                    //TACMenuAppLToolBar.onAppLaunchToggle();
                    Vector2 anchormin = new Vector2(0.5f, 0.5f);
                    Vector2 anchormax = new Vector2(0.5f, 0.5f);
                    string msg = Localizer.Format("#autoLOC_TACLS_00036"); //#autoLOC_TACLS_00036 = TAC LS Config Settings are now available via the KSP Settings - Difficulty Options Window.                          
                    string title = Localizer.Format("#autoLOC_TACLS_00037");  //#autoLOC_TACLS_00037 = TAC Life Support
                    UISkinDef skin = HighLogic.UISkin;
                    DialogGUIBase[] dialogGUIBase = new DialogGUIBase[1];
                    dialogGUIBase[0] = new DialogGUIButton(Localizer.Format("#autoLOC_417274"), delegate { });  // #autoLOC_417274 = Ok
                    PopupDialog.SpawnPopupDialog(anchormin, anchormax,
                        new MultiOptionDialog("TACReminder", msg, title, skin, dialogGUIBase), false, HighLogic.UISkin, true,
                        string.Empty);
                    //TacLifeSupport.Instance.gameSettings.IsNewSave = false;
                }*/
                TacLifeSupport.Instance.gameSettings.IsNewSave = false;
                AddLifeSupport als = new AddLifeSupport();
                als.run();
            }
            else
            {
                Destroy(this);
            }
        }
        
        void OnDestroy()
        {
            this.Log("OnDestroy");
        }
    }
}
