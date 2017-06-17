using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods {

    public static PlayMakerFSM GetPlayMakerFsm(this GameObject gameObject)
    {
        return gameObject.GetComponent<PlayMakerFSM>();
    }

    public static PlayMakerFSM GetPlayMakerFsmByName(this GameObject gameObject, string fsmName)
    {
        PlayMakerFSM[] playMakerFsms = gameObject.GetComponents<PlayMakerFSM>();

        return playMakerFsms.FirstOrDefault(playMakerFsm => playMakerFsm.FsmName == fsmName);
    }
    
}
