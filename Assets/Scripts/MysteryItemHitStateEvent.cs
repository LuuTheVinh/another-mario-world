using UnityEngine;
using System.Collections;

public class MysteryItemHitStateEvent : StateMachineBehaviour {

    public GameObject _mushroomPrefab;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject item = animator.gameObject.GetComponent<MysteryItem>()._itemPrefab;
        Item.ItemType itemtype = item.GetComponent<Item>()._type;
        bool init = false;
        switch (itemtype)
        {
            case Item.ItemType.Flygon:
            case Item.ItemType.Coin:
                init = true;
                break;
            case Item.ItemType.Leaf:
                Animator mario_animator = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Animator>();
                if (mario_animator.GetInteger("status")==(int) Mario.eMarioStatus.SMALL)
                {
                    animator.gameObject.GetComponent<MysteryItem>()._itemPrefab = _mushroomPrefab;
                    itemtype = Item.ItemType.Mushroom;
                }
                else
                {
                    init = true;
                }
                break;
        }
        if (init == true)
        {
            initItem(item, animator.gameObject.transform.parent);
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject item = animator.gameObject.GetComponent<MysteryItem>()._itemPrefab;
        Item.ItemType itemtype = item.GetComponent<Item>()._type;
        bool init = false;
        switch (itemtype)
        {
            case Item.ItemType.FireFlower:
                Animator mario_animator = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Animator>();
                if (mario_animator.GetInteger("status")==(int) Mario.eMarioStatus.SMALL)
                {
                    item = _mushroomPrefab;
                    itemtype = Item.ItemType.Mushroom;
                    init = true;
                }
                else
                {
                    init = true;
                }
                break;
            case Item.ItemType.Mushroom:

            case Item.ItemType.Amazing_Star:
                init = true;
                break;
        }

        if (init == true)
        {
            initItem(item, animator.gameObject.transform.parent);
        }
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

    private void initItem(GameObject source, Transform parent)
    {
        GameObject obj = (GameObject)Object.Instantiate(
            source,
            new Vector3(0, 1, 1),
            parent.rotation
            );
        obj.transform.parent = parent;
    }
}
