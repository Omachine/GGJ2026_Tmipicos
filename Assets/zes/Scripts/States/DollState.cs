using UnityEngine;

namespace FiniteStateMachine
{
    public class DollPlayerState : PlayerState
    {
        //[SerializeField] private Transform evilObjects;

        public DollPlayerState(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            player.DollCamera.SetActive(true);
            player.UI.SetActive(true);
            player.UIFP.SetActive(false);
            animator.CrossFade(GroundedHash, crossFadeDuration);
            ActivateEvilObjects(true);
      
        }

        public override void OnExit()
        {
            player.DollCamera.SetActive(false);
            player.UI.SetActive(false);
            player.UIFP.SetActive(true);
            PlayerController.isInDoll = false;
            ActivateEvilObjects(false);
        }

        private void ActivateEvilObjects(bool activate)
        {
            foreach (Transform evil in PlayerController.staticEvilObjects)
            {
                if (evil.gameObject.layer == 8) evil.gameObject.SetActive(activate);
                else
                {
                    foreach(Transform moreEvil in evil)
                    {
                        if(moreEvil.gameObject.layer == 8) moreEvil.gameObject.SetActive(activate);
                        else
                        {
                            foreach (Transform evenMoreEvil in moreEvil)
                            {
                                if (evenMoreEvil.gameObject.layer == 8) evenMoreEvil.gameObject.SetActive(activate);
                            }
                        }
                    }
                }
            }
        }
    }
}