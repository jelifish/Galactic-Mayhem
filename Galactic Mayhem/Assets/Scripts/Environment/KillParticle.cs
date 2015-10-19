using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour {
		private ParticleSystem ps;
		
		
		public void Start() 
		{
			ps = GetComponent<ParticleSystem>();
		}
		
		public void Update() 
		{
			if(ps)
			{
				if(!ps.IsAlive())
				{
					Destroy(gameObject);
				}
			}
		}
}
