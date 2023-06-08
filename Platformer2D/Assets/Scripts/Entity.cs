using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public virtual void GetDamage()
    {

    }

    public virtual void Die()
    {
        
    }

    protected IEnumerator OnHurt(SpriteRenderer spriteRenderer)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.3f);

        Color hurtColor = new Color(1f, 0.4f, 0.4f);
        Color normalColor = new Color(1f, 1f, 1f);

        spriteRenderer.color = hurtColor;
        yield return waitForSeconds;
        spriteRenderer.color = normalColor;
        yield return waitForSeconds;
        spriteRenderer.color = hurtColor;
        yield return waitForSeconds;
        spriteRenderer.color = normalColor;
        yield return waitForSeconds;
    }
}
