using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] Transform cubePrefab;

    private IEnumerator Start()
    {
        Debug.Assert(cubePrefab != null, "큐브가 없습니다!!");
        for (int i = 0; i < 5; i++)
        {
            Instantiate(cubePrefab,
                        Vector3.zero + Vector3.right * i,
                        Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
