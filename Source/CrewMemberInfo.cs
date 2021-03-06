﻿/**
 * Thunder Aerospace Corporation's Life Support for Kerbal Space Program.
 * Written by Taranis Elsu.
 * 
 * (C) Copyright 2013, Taranis Elsu
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

using System;
using System.Linq;

namespace Tac
{
    public class CrewMemberInfo
    {
        public const string ConfigNodeName = "CrewMemberInfo";

        public readonly string name;
        public double lastUpdate;
        public double lastFood;
        public double lastWater;
        public string vesselName;
        public Guid vesselId;
        public bool vesselIsPreLaunch;  //True if the vessel this kerbal is on is PreLaunch status
        public bool hibernating;  //True if Kerbal is hibernating (rather than dead)
        public bool DFfrozen;  //True if DeepFreeze Mod is installed and this kerbal is frozen
        public bool recoverykerbal;  //True if this kerbal is part of a Recover Contract
        public ProtoCrewMember.KerbalType crewType;
        public readonly double respite = UnityEngine.Random.Range(60, 600);

        public CrewMemberInfo(string crewMemberName, string vesselName, Guid vesselId, double currentTime)
        {
            name = crewMemberName;
            lastUpdate = currentTime;
            lastFood = currentTime;
            lastWater = currentTime;
            this.vesselName = vesselName;
            this.vesselId = vesselId;
            this.vesselIsPreLaunch = true;
            hibernating = false;
            DFfrozen = false;
            recoverykerbal = false;
            crewType = ProtoCrewMember.KerbalType.Crew;
            if (HighLogic.CurrentGame != null)
            {
                if (HighLogic.CurrentGame.CrewRoster.Exists(name))
                {
                    ProtoCrewMember kerbal = HighLogic.CurrentGame.CrewRoster[name];
                    if (kerbal != null)
                    {
                        crewType = kerbal.type;
                    }
                }
            }
        }

        public static CrewMemberInfo Load(ConfigNode node)
        {
            string name = Utilities.GetValue(node, "name", "Unknown");
            double lastUpdate = Utilities.GetValue(node, "lastUpdate", 0.0);
            string vesselName = Utilities.GetValue(node, "vesselName", "Unknown");
            Guid vesselId;
            if (node.HasValue("vesselId"))
            {
                vesselId = new Guid(node.GetValue("vesselId"));
            }
            else
            {
                vesselId = Guid.Empty;
            }

            CrewMemberInfo info = new CrewMemberInfo(name, vesselName, vesselId, lastUpdate);
            info.vesselIsPreLaunch = Utilities.GetValue(node, "vesselIsPreLaunch", true);
            info.lastFood = Utilities.GetValue(node, "lastFood", lastUpdate);
            info.lastWater = Utilities.GetValue(node, "lastWater", lastUpdate);
            info.hibernating = Utilities.GetValue(node, "hibernating", false);
            info.DFfrozen = Utilities.GetValue(node, "DFFrozen", false);
            info.recoverykerbal = Utilities.GetValue(node, "recoverykerbal", false);
            info.crewType = Utilities.GetValue(node, "crewType", info.crewType);
            return info;
        }

        public ConfigNode Save(ConfigNode config)
        {
            ConfigNode node = config.AddNode(ConfigNodeName);
            node.AddValue("name", name);
            node.AddValue("lastUpdate", lastUpdate);
            node.AddValue("lastFood", lastFood);
            node.AddValue("lastWater", lastWater);
            node.AddValue("vesselName", vesselName);
            node.AddValue("vesselId", vesselId);
            node.AddValue("vesselIsPreLaunch", vesselIsPreLaunch);
            node.AddValue("hibernating", hibernating);
            node.AddValue("DFFrozen", DFfrozen);
            node.AddValue("recoverykerbal", recoverykerbal);
            node.AddValue("crewType", crewType);
            return node;
        }
    }
}
