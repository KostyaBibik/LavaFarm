using UnityEngine;

namespace Db
{
    public static class AnimatorHashKeys
    {
        public static readonly int MoveHash = Animator.StringToHash("Move");
        public static readonly int PickUpHash = Animator.StringToHash("PickUp");
        public static readonly int ChopHash = Animator.StringToHash("Chop");
        public static readonly int MowHash = Animator.StringToHash("Mow");
    }
}