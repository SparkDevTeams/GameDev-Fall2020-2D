using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Slider : MonoBehaviour
{
    private HealthManager hm;
    [SerializeField]
    private Image gauge, holder, border;
    [SerializeField]
    private int borderOffset = 1 , sizeMult = 1;
    [SerializeField]
    private Color damage = Color.red , normal = Color.yellow;
    private int currentX = 100;


    public void Awake()
    {
        //Get reference to player's HP
        hm = FindObjectOfType<PlayerState>().gameObject.GetComponent<HealthManager>();
        currentX = hm.getShieldHealth();
        UpdateGauges();
    }

    public void FixedUpdate()
    {
        if (currentX < hm.getShieldHealth())
        {
            currentX++;
            gauge.color = normal;
        }
        else if (currentX > hm.getShieldHealth())
        {
            currentX--;
            gauge.color = damage;
        }
        else {
            gauge.color = normal;
        }

        UpdateGauges();
    }

    public void UpdateGauges()
    {
        if (hm != null)
        {
            gauge.rectTransform.sizeDelta = new Vector2( currentX * sizeMult, gauge.rectTransform.sizeDelta.y);
            holder.rectTransform.sizeDelta = new Vector2( hm.MaxHP * sizeMult, holder.rectTransform.sizeDelta.y);
            border.rectTransform.sizeDelta = new Vector2( ( hm.MaxHP * sizeMult) + borderOffset, border.rectTransform.sizeDelta.y);
        }
    }

}
