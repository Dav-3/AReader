using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script which was used to test the functionality of the surface magnetism solver
/// </summary>
public class PlacementController : MonoBehaviour
{
    public GameObject target;
    public LayerMask mask;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            target.transform.position = hit.point;
            target.transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
        }
    }
}
