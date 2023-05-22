using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] Bonuses;
    public Transform BonusPoint;
    public int ShowingDelay = 3;
    public Animator Animator;

    public enum BonusTypes
    {
        Scissors,
        Cheese,
        Shield,
        Random
    };
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public BonusTypes BonusType = BonusTypes.Random;

    private IEnumerator ShowBonus()
    {
        yield return new WaitForSeconds(ShowingDelay);
        Destroy(gameObject);
        int index = (BonusType == BonusTypes.Random) ? Random.Range(0, Bonuses.Length) : (int)BonusType;

        GameObject bonus = Instantiate(Bonuses[index], BonusPoint.position, BonusPoint.rotation);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            Animator.SetBool("isOpening", true);
            StartCoroutine(ShowBonus());
            ShowBonus();
        }
    }
}
