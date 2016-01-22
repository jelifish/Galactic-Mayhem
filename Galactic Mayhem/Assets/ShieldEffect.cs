using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldEffect : MonoBehaviour {

    // public List<Vector4> positionList = new List<Vector4>();
    public List<ShieldEffectNode> nodes = new List<ShieldEffectNode>();
    public MeshRenderer meshR;

    [SerializeField] int maxPositions = 16;

    [System.Serializable]
    public class ShieldEffectNode
    {
        public Vector3 pos = Vector3.zero;
        public float startIntensity = 2;
        public float minRadius = 1;
        public float maxRadius = 3;

        public float t = 0;
        public float maxLifeTime = 1;
        
        public void Step(float deltaT)
        {
            t += deltaT;
        }
        public float intensity
        {
            get
            {
                return Mathf.Lerp(startIntensity, 0, t / maxLifeTime);
            }
        }
        public float radius
        {
            get
            {
                return Mathf.Lerp(minRadius, maxRadius, t / maxLifeTime);
            }
        }

        public bool free
        {
            get
            {
                return t >= maxLifeTime;
            }
        }
    }

    void Update()
    {
        // Add(transform.position + Random.onUnitSphere * 2, Random.Range(1f, 5f), Random.Range(1f, 3f), Random.Range(1f, 2f));

        for (int i = 0; i < nodes.Count && i < maxPositions; i++)
        {
            ShieldEffectNode node = nodes[i];
            if (!node.free)
            {
                node.Step(Time.deltaTime);

                meshR.material.SetVector("_Position" + i.ToString(), new Vector4(node.pos.x, node.pos.y, node.pos.z, node.radius));
                meshR.material.SetFloat("_Intensity" + i.ToString(), node.intensity);
            }
        }

        meshR.material.SetFloat("_Scale", transform.lossyScale.x);

        // Never rotate
        transform.rotation = Quaternion.identity;
    }

    public void Add(Vector3 impactPosition, float maxIntensity, float radius, float maxLifeTime)
    {
        // Convert from cardinal to spherical
        Vector3 localPos = (impactPosition - transform.position).normalized;

        Debug.DrawLine(transform.position, impactPosition, Color.red, 1f);

        ShieldEffectNode node = null;
        if (nodes.Count < maxPositions)
        {
            // Add a new node
            node = new ShieldEffectNode();
            nodes.Add(node);
            
        }
        else
        {
            // Find an unused node
            node = nodes.Find(x => x.free == true);
        }

        if (node != null)
        {
            node.pos = localPos;

            node.maxRadius = radius;
            node.minRadius = 0;

            node.t = 0;
            node.maxLifeTime = maxLifeTime;

            node.startIntensity = maxIntensity;
        }
    }
}
