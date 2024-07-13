using UnityEngine;

namespace Core
{
    public static class Utils
    {
        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            ;
        }
    }
}