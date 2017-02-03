using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command{


    
   public Command(){

    }

    public virtual void Execute(GameObject agentCommander) {}

}
public class MoveCommand : Command {
    public override void Execute(GameObject agentCommander) { MovePlayer(agentCommander); }
    
   public void MovePlayer(GameObject p_agentCommander) { Debug.Log("Command MovePlayer() called"); }

}

public class UseAbilityCommand : Command
{
    public override void Execute(GameObject agentCommander) { UseAbility(agentCommander); }
    public void UseAbility(GameObject p_agentCommander) { Debug.Log("Command UseAbility() called"); }

}
public class ApplyEffectCommand : Command
{
    public override void Execute(GameObject agentCommander) { ApplyEffect(agentCommander); }
    public void ApplyEffect(GameObject p_agentCommander) { Debug.Log("Command ApplyEffect() called"); }

}


public class InputBuffer : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*Command BufferInput(){

    }*/
    
}
