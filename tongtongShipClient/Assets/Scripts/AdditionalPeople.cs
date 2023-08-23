using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalPeople : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyItself());
    }

    // People disappear 12 seconds later
    IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
