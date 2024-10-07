using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private Vector2 _knockBack = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: See if can be hit.
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveryKnockBack = transform.parent.localScale.x > 0 ? _knockBack : new Vector2(-_knockBack.x, _knockBack.y);
            //TODO: Hit the target.
            bool goToHit = damageable.Hit(_attackDamage, deliveryKnockBack);
            if (goToHit)
            {
                Debug.Log(collision.name + "Attacked" + _attackDamage);            
            }
        }
    }
}
