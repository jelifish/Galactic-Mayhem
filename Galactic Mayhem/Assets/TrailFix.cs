using UnityEngine;
using System.Collections;

public class TrailFix : MonoBehaviour {

    public TrailRenderer trail;
    // Use this for initialization
    void Start()
    {
        trail.sortingLayerName = "Background";
        trail.sortingOrder = 2;

    }
    //void OnDisable() {
    //    trail.enabled = false;
    //}
    //void OnEnable()
    //{
    //    trail.enabled = true;
    //}
}
