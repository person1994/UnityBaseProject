﻿using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.PlayMaker
{
    [TaskDescription("Sends an event to a PlayMaker FSM. The task will return success immediately.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=67")]
    [TaskIcon("BehaviorDesigner/Third Party/PlayMaker/Editor/PlayMakerIcon.png")]
    [TaskCategory("PlayMaker")]
    public class SendEvent : Action
    {
        [Tooltip("The GameObject that the PlayMaker FSM component is attached to")]
        public SharedGameObject playMakerGameObject;
        [Tooltip("The name of the FSM component. This allows you to have multiple FSM components on a single GameObject")]
        public SharedString FsmName = "FSM";
        [Tooltip("The name of the event to send to the FSM within PlayMaker")]
        public SharedString eventName = "FsmEvent";

        // A cache of the PlayMakerFSM to prevent having to look
        private HutongGames.PlayMaker.Fsm playMakerFSM;

        public override void OnStart()
        {
            // Find the correct PlayMakerFSM based on the name.
            var playMakerComponents = playMakerGameObject.Value != null ? playMakerGameObject.Value.GetComponents<PlayMakerFSM>() : gameObject.GetComponents<PlayMakerFSM>();
            if (playMakerComponents != null && playMakerComponents.Length > 0) {
                playMakerFSM = playMakerComponents[0].Fsm;
                //  We don't need the FsmName if there is only one PlayMakerFSM component
                if (playMakerComponents.Length > 1) {
                    for (int i = 0; i < playMakerComponents.Length; ++i) {
                        if (playMakerComponents[i].FsmName.Equals(FsmName.Value)) {
                            // Cache the result when we have a match and stop looping.
                            playMakerFSM = playMakerComponents[i].Fsm;
                            break;
                        }
                    }
                }
            }

            // We can't do much if there isn't a PlayMakerFSM.
            if (playMakerFSM == null) {
                Debug.LogError(string.Format("Unable to find PlayMaker FSM {0}{1}", FsmName, (playMakerGameObject.Value != null ? string.Format(" attached to {0}", playMakerGameObject.Value.name) : "")));
            }
        }
        
        public override TaskStatus OnUpdate()
        {
            if (playMakerFSM != null && !eventName.Value.Equals("")) {
                playMakerFSM.Event(eventName.Value);
            }
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            // Reset the public properties back to their original values
            playMakerGameObject = null;
            FsmName = "FSM";
            eventName = "FsmEvent";
        }
    }
}