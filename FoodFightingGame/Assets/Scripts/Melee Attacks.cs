using UnityEditor.UIElements;
using UnityEngine;

public class MeleeAttacks : MonoBehaviour
{
    public BoxCollider2D attackHitboxBoxCollider;
    public float AttackSizeX;
    public float AttackSizeY;
    public float HitBoxLingeringDuration;

    public TagField tag;

    public void CreateHitox()
    {
        attackHitboxBoxCollider.gameObject.SetActive(true);
        attackHitboxBoxCollider.size = new Vector2 (AttackSizeX, AttackSizeY);
        Invoke(nameof(DeleteHitbox), HitBoxLingeringDuration);
    }
    public void DeleteHitbox()
    {
        attackHitboxBoxCollider.gameObject.SetActive(false);
    }
}
