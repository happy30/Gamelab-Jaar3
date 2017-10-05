using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationEffects : MonoBehaviour
{

	public void Strobe()
    {
        GameObject.Find("Canvas").GetComponent<ConversationUI>().Strobe();
    }

    public void Black()
    {
        GameObject.Find("Canvas").GetComponent<ConversationUI>().Black();
    }

    public void FadeOutBlack()
    {
        GameObject.Find("Canvas").GetComponent<ConversationUI>().FadeOutBlack();
    }


}
