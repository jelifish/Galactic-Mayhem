using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum Activation { Button, Action, Automatic };
public enum SkillType { MaterialType, ControlType, GuardType, AdvanceType, AuraType };
//public enum SkillLevel { Lv1, Lv2, Lv3};

//=== 001 Compressed Bolts =============================
public class Skill001 : Skill {
	public override void init(){
		skillName = "Compressed Bolts";
		skillType = SkillType.MaterialType;
		skillNum = 1;
	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill001Attr> ();
	}
	public override void attachSpecialAttributes(){
	}
}
public class Skill001Attr : Interactable{
	public override void mouseUpFire(){
		timeResume ();
	}
	public override void mouseDownFire(){
		timeSlow ();
		StartCoroutine (fire ());
	}

	
	void Start(){
	}

	IEnumerator fire()
	{
		yield return new WaitForSeconds (.03f);
		initialSpeed = 5;
		while (true) {
			for(int i=0;i<35;i++){
                GameObject temp = ObjectPool.pool.GetPooledObject();
                temp.transform.position = this.transform.position;
                temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f));
                Skillf.f.AddForce(temp, Skillf.lowForce);
				yield return new WaitForSeconds (.01f);
			}
			break;
		}
		OnDestroy ();
		//this.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds (5f);
		//Destroy (this.gameObject);

	}
}
//=== 002 Cone Bolts =============================
public class Skill002 : Skill {
	public override void init(){
		skillName = "Cone Bolts";
		skillType = SkillType.MaterialType;
		skillNum = 2;
	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill002Attr> ();

	}
	public override void attachSpecialAttributes(){
		spawn.AddComponent<Skill011Attr> ();
	}
}
public class Skill002Attr : Interactable{
	void Start(){
	}
	public override void mouseUpFire(){
		timeResume ();
	}
	public float timeMultiplier = 1;

	public override void mouseDownFire(){
		timeSlow ();
		StartCoroutine (fire ());
	}
	IEnumerator fire()
	{
		yield return new WaitForSeconds (.03f);
		initialSpeed = 15;
		while (true) {
			for(int i=0;i<50;i++){
				//GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f)));
				GameObject temp = ObjectPool.pool.GetPooledObject();
				temp.transform.position = this.transform.position;
				temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f));
				Skillf.f.AddForce(temp, Skillf.highForce);
				yield return new WaitForSeconds (.03f);
			}
			break;
		}
		OnDestroy (); //call at the very end to resume the time
		//this.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds (3f);
		//Destroy (this.gameObject);
	}
}
//=== 003 Machine Gun =============================
public class Skill003 : Skill {
	public override void init(){
		skillName = "Machine Gun";
		skillType = SkillType.MaterialType;
		skillNum = 3;
	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill003Attr> ();
		spawn.GetComponent<Skill003Attr> ().projectile = this.projectile;
	}
	public override void attachSpecialAttributes(){
		spawn.AddComponent<Skill011Attr> ();
		spawn.GetComponent<Skill011Attr> ().projectile = this.projectile;
	}
}
public class Skill003Attr : Interactable{
	public override void mouseUpFire(){
		if (isTimeSlowed) {
			Time.timeScale += 0.5F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			isTimeSlowed = false;
		}
	}
	public override void mouseDownFire(){
		if (!isTimeSlowed) {	
			Time.timeScale -= 0.5F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			isTimeSlowed = true;
		}
		StartCoroutine (fire ());
	}
	

	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color(152/255.0F,203/255.0F,74/255.0F,255f));
	}

	IEnumerator fire()
	{
		yield return new WaitForSeconds (.03f);
		initialSpeed = 25;
		while (true) {
			for(int i=0;i<25;i++){
				GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f)));
				temp.GetComponent<Mover>().speed = Random.Range(initialSpeed*.9f, initialSpeed*1.1f);
				if(GetComponent<SpawnedWeapon>().towardsObject!=null){
					Vector3 targetVector = (GetComponent<SpawnedWeapon>().towardsObject.transform.position - temp.transform.position).normalized;
					temp.GetComponent<Rigidbody>().AddForce(targetVector * 200);
				}
				yield return new WaitForSeconds (.1f);
			}
			break;
		}
		if (isTimeSlowed) {
			Time.timeScale += 0.5F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			isTimeSlowed = false;
		}
		//this.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds (5f);
		//Destroy (this.gameObject);
	}
}
//=== 004 Cluster Bolt =============================
public class Skill004 : Skill {
	public override void init(){
		skillName = "Cluster Bolt";
		skillType = SkillType.MaterialType;
		skillNum = 4;
		//coolDown = 3f; //cooldown can be initialized at this time.
	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill004Attr> ();
		
	}
	public override void attachSpecialAttributes(){
	}
}
public class Skill004Attr : Interactable{
	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color(152/255.0F,203/255.0F,74/255.0F,255f));
	}
	public bool mouseUp = false;
	public override void mouseUpFire(){
		timeResume ();
		mouseUp = true;

	}
	public float timeMultiplier = 1;
	
	public override void mouseDownFire(){
		timeSlow ();
		StartCoroutine (fire ());
	}

	IEnumerator fire()
	{
		List<GameObject> bullets = new List<GameObject>();

		yield return new WaitForSeconds (.03f);
		initialSpeed = 15;
		while (true) {
			for(int i=0;i<25;i++){
				//GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f)));
				GameObject temp = ObjectPool.pool.GetPooledObject();

				temp.transform.position = new Vector3(transform.position.x+Random.insideUnitCircle.x, transform.position.y + Random.insideUnitCircle.y,0);
				temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(0, 360));
				temp.transform.localScale = temp.transform.localScale * 2;

				bullets.Add(temp);
				//Skillf.f.AddForce(temp, Skillf.highForce*Random.Range(.9f, 1.1f)*2);
				if(mouseUp){
                    break;
				}
				yield return new WaitForSeconds (.05f);


			}
			yield return new WaitForSeconds (.01f);
			break;
		}
		while (true) {
			if(mouseUp){
                mouseUp = false;
                Skillf.f.ForceTowardsDirection(bullets,this.transform.position,targetPosition, Skillf.highForce*Random.Range(.9f, 1.1f)*2);
				break;
			}
			yield return new WaitForSeconds (.03f);

		}


		OnDestroy (); //call at the very end to resume the time
		//this.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds (5f);
		//Destroy (this.gameObject);
	}
}
//=== 011 Accelerator =============================
public class Skill011 : Skill {
	public override void init(){
		skillName = "Accelerator";
		skillType = SkillType.ControlType;
		skillNum = 11;

	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill011Attr> ();
	}
	public override void attachSpecialAttributes(){
	}
}
public class Skill011Attr : Interactable{

	public List<GameObject> bullets = new List<GameObject>();

	
	public override void mouseUpFire(){
		Timef.f.SpeedTime (2f);
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5);
		//Debug.Log (transform.position); //spawn position
		//Debug.Log (targetPosition); //spawn position
		//int i = 0;

		foreach (Collider col in hitColliders) {
			if(col.tag == "Bullet"){
				bullets.Add(col.gameObject);
			}
		}
		StartCoroutine (Blast ());
	}

	public override void mouseDownFire(){

		Timef.f.SlowTime (2f);
		//Time.fixedDeltaTime = 0.02F * Time.timeScale;

	}

    void OnEnable() {
        bullets = new List<GameObject>();
    }
	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color(247/255.0F,216/255.0F,66/255.0F,255f));
	}
	
	IEnumerator Blast()
	{
		//yield return new WaitForSeconds (0.2f);

		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.lowForce);
		yield return new WaitForSeconds (0.1f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.medForce);
		yield return new WaitForSeconds (0.1f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.highForce);
		yield return new WaitForSeconds (0.1f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.highForce);
		yield return new WaitForSeconds (0.1f);
        
        OnDestroy();
    }
}
//=== 012 Collapse =============================
public class Skill012 : Skill {
	public override void init(){
		skillName = "Collapse";
		skillType = SkillType.ControlType;
		skillNum = 12;
		
	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill012Attr> ();
	}
	public override void attachSpecialAttributes(){
	}
}
public class Skill012Attr : Interactable{
	
	public List<GameObject> bullets = new List<GameObject>();
    void OnEnable()
    {
        bullets = new List<GameObject>();
    }

    public override void mouseUpFire(){
        StartCoroutine(Blast());

    }
	public override void mouseDownFire(){
		Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 5);
		foreach (Collider col in hitColliders) {
			if(col.tag == "Bullet"){
				bullets.Add(col.gameObject);
			}
		}

		//StartCoroutine (Blast ());
	}

	
	void Start(){
		
	}
	
	IEnumerator Blast()
	{
		yield return new WaitForSeconds (.03f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds (0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds (0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.medForce);
        yield return new WaitForSeconds (0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.highForce);
        //Destroy (this.gameObject);
        yield return new WaitForSeconds (0.1f);
        OnDestroy();
    }
}
//=== 013 Black Hole Explosion =============================
public class Skill013 : Skill {
	public override void init(){
		skillName = "Black Hole";
		skillType = SkillType.ControlType;
		skillNum = 13;
		
	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill013Attr> ();
	}
	public override void attachSpecialAttributes(){

	}
}
public class Skill013Attr : Interactable{
	
	public List<GameObject> bullets = new List<GameObject>();
    void OnEnable()
    {
        bullets = new List<GameObject>();
    }


    public override void mouseUpFire(){
		Timef.f.SpeedTime (2f);
		Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 20); 
		foreach (Collider col in hitColliders) {
			if(col.tag == "Bullet"){
				bullets.Add(col.gameObject);
			}
		}
		StartCoroutine (Blast ());
	}
	
	public override void mouseDownFire(){
		Timef.f.SlowTime (2f);
		
	}
	
	
	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color(247/255.0F,216/255.0F,66/255.0F,255f));
	}
	
	IEnumerator Blast()
	{
		
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.highForce);
		yield return new WaitForSeconds (0.5f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.lowForce);
		yield return new WaitForSeconds (0.5f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.lowForce);
		yield return new WaitForSeconds (0.5f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.medForce);
		yield return new WaitForSeconds (0.5f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.medForce);
		yield return new WaitForSeconds (0.5f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.medForce);
		yield return new WaitForSeconds (0.2f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.lowForce);
		yield return new WaitForSeconds (0.1f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.lowForce);
		yield return new WaitForSeconds (0.1f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.lowForce);
		yield return new WaitForSeconds (0.1f); 
		Skillf.f.ForceTowardsPoint (bullets,targetPosition,Skillf.lowForce);

		yield return new WaitForSeconds (0.1f);
		Skillf.f.Freeze(bullets);
		Skillf.f.ExplosiveForceRandom50 (bullets, targetPosition, Skillf.highForce*7);
		
		yield return new WaitForSeconds (0.5f);
        //Destroy (this.gameObject);
        OnDestroy();
        yield return new WaitForSeconds (0.1f);
		//		
	}
}
//=== 031 Missile =============================
public class Skill031 : Skill {
	public override void init(){
		skillName = "Missile";
		skillType = SkillType.AdvanceType;
		skillNum = 31;
	}
	public override void attachAttributes(){
		spawn.AddComponent<Skill031Attr> ();
	}
	public override void attachSpecialAttributes(){
		spawn.AddComponent<Skill011Attr> ();
	}
}
public class Skill031Attr : Interactable{
    //public List<GameObject> bullets = new List<GameObject>();
    public List<GameObject> missiles; // initialize all global lists -like things in OnEnable

	public GameObject bolt = Resources.Load ("Projectiles/Bolt")as GameObject;

	public override void mouseUpFire(){
		Timef.f.SpeedTime (2f);
	}
	public override void mouseDownFire(){
		Timef.f.SlowTime (2f);
		StartCoroutine (fire ());
	}
	

	void OnEnable(){
        missiles = new List<GameObject>();
    }
    void Start() {
        this.projectile = Resources.Load("Projectiles/Missile") as GameObject;
    }


	IEnumerator fire()
	{
		yield return new WaitForSeconds (.03f);
		initialSpeed = 5;

		//while (true) {
		
		//bullets.Add(temp.gameObject);
		StartCoroutine (createMissiles ());		
		while(GetComponent<SpawnedWeapon>().towardsObject.activeSelf){
			foreach(GameObject missile in missiles)
			{
				Vector3 targetVector = (GetComponent<SpawnedWeapon>().towardsObject.transform.position - missile.transform.position).normalized;
				//missile.GetComponent<Rigidbody>().AddForce(targetVector * 25);
				missile.GetComponent<Rigidbody>().velocity = targetVector * 20;
			}
			yield return new WaitForSeconds (.01f);
		}
		yield return new WaitForSeconds (.01f);
		foreach(GameObject missile in missiles)
		{
			missile.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		yield return new WaitForSeconds (.01f);
		
		while (true) {
			for(int i=0;i<700;i++){
				foreach(GameObject missile in missiles)
				{
					//GameObject bolty= (GameObject)Instantiate (bolt, new Vector3(missile.transform.position.x+Random.insideUnitCircle.x, missile.transform.position.y+Random.insideUnitCircle.y,0), this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(0, 360)));
					GameObject temp = ObjectPool.pool.GetPooledObject();
					temp.transform.position = new Vector3(missile.transform.position.x+Random.insideUnitCircle.x, missile.transform.position.y+Random.insideUnitCircle.y,0);
					temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(0, 360));
					//Skillf.f.ExplosiveForce (bolty,missile.transform.position);
					//bolty.GetComponent<ProjectileCollision>().speed = Random.Range(initialSpeed*.9f, initialSpeed*1.1f);

				}
			}
			break;
		}
		yield return new WaitForSeconds (.01f);

		foreach (GameObject missile in missiles) {
			Collider[] hitColliders = Physics.OverlapSphere (missile.transform.position,10);
			//Debug.Log(missile.transform.position);
			List<GameObject> bullets = new List<GameObject> ();
			foreach (Collider col in hitColliders) {
				if (col.tag == "Bullet") {
				
					//col.GetComponentInChildren<RotateTowards> ().towardsObject = this.gameObject;
					bullets.Add (col.gameObject);

				}
			}
			//missile.GetComponent<MeshRenderer> ().enabled = false;
			StartCoroutine(Blast(missile.transform.position,bullets));
			yield return new WaitForSeconds (.1f);
			
            //missiles.Remove(missile);
            Destroy(missile);
            yield return new WaitForSeconds (.01f);
		}


		//StartCoroutine (Blast ());

		yield return new WaitForSeconds (1f);

		OnDestroy ();
		//this.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds (5f);
		//Destroy (this.gameObject);
		
	}
	IEnumerator createMissiles(){
			for (int i=0; i<1; i++) {
			if(GetComponent<SpawnedWeapon>().towardsObject!=null){
				GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler (0f, 0.0f, Random.Range (0, 360)));
				//temp.GetComponent<Mover> ().speed = Random.Range (initialSpeed * .9f, initialSpeed * 1.7f);
			
				missiles.Add (temp);
			}
				yield return new WaitForSeconds (1f);
			}

		yield return new WaitForSeconds (.1f);
		//yield return new WaitForSeconds (.5f);
	}
	IEnumerator Blast(Vector3 blastPos, List<GameObject> bullets)
	{
		yield return new WaitForSeconds (Random.Range(.1f, .5f));


		Skillf.f.ExplosiveForce (bullets,new Vector3(blastPos.x+(Random.insideUnitCircle*2).x, blastPos.y+(Random.insideUnitCircle*2).y,0),Skillf.lowForce);
		Skillf.f.ExplosiveForce (bullets,new Vector3(blastPos.x+(Random.insideUnitCircle*2).x, blastPos.y+(Random.insideUnitCircle*2).y,0),Skillf.lowForce);
		Skillf.f.ExplosiveForce (bullets,new Vector3(blastPos.x+(Random.insideUnitCircle*2).x, blastPos.y+(Random.insideUnitCircle*2).y,0),Skillf.lowForce);
		yield return new WaitForSeconds (.5f);
		Skillf.f.ExplosiveForceRandom50 (bullets,blastPos,Skillf.highForce*2, 10);
		yield return new WaitForSeconds (0.1f);
		
	}


}





//=== Skill Class ===============================
public class Skill : MonoBehaviour{
	public SkillType skillType = SkillType.MaterialType; // this is the type of skill
	public GameObject projectile = null; //this is the projectile that the skill fires
	public PlayerController player; // this is the player object
	public float coolDown = 999f; // FLOAT this is the cooldown of the skill. initialized to 999f. if it is still 999 at the end of init process, default values are chosen.
	public GameObject interactable; //this is the spawned weapon

	//skills exp system
	public int skillLevel = 0;// skills start out at level 0. probably a good idea not to override this
	public int rarity = 1; //the level cap. max level can be initialized to something higher. for every skilllevel, majorlevel++
	public float expInit = 5f;//exp per level is calculated by (expInit * Mathf.Pow(expRatio, x)); where x is the level
	public float expRatio = 1.618034f; //golden ratio curve. slower curves use a smaller value >1. negative curves use values <1. 
	protected float exp = 0; //current exp
	public int calcExpLimit(){
		return (int)(expInit * Mathf.Pow (expRatio, skillLevel ));
	}
	public void addExp(){
		exp += 1;
		if (exp > calcExpLimit ()) {//if exp is greater that the current limit levelup()
			levelUp();
		} 
	}
	public void addExp(int x){
		for (int i = 0; i<x; i++) {
			exp += 1;
			if (exp >= calcExpLimit ()) {//if exp is greater that the current limit levelup()
				levelUp ();
			} 
		}
	}
	public virtual void levelUp(){
		exp = 0; //reset current exp
		skillLevel += 1;

	
	}
	public void rarityUP(){
		rarity += 1;
	}
	
	public string skillName = "Nul"; //skillname is set in the child
	public int skillNum = 0;
	public virtual void init(){// called before initialization of spawner
	}
	public virtual void attachAttributes(){ //done in child, assigns to the spawner script a script that determines what the skill will do when clicked
	}
	public virtual void attachSpecialAttributes(){
	}
	IEnumerator interactableCreator() //creates a spawn
	{
		yield return new WaitForSeconds (Random.Range(0f,2f));
		while(true){
			yield return new WaitForSeconds (coolDown);
			//spawnInteractable();
			
		}
	}
	
	public GameObject spawn;//do not mess with this
	public bool isSpecialOn = false;
	
	public void initInteractable(){ //spawner instatiation at the correct random coords
		genXY ();
		spawn = (GameObject)Instantiate (interactable, new Vector3 (x, y, 0f), Quaternion.identity);

		if (isSpecialOn) {
			attachSpecialAttributes ();
			isSpecialOn = false;
		} else {
			attachAttributes ();
		}


		//initialize all known variables
		spawn.GetComponent<Interactable> ().skillType = this.skillType;
		spawn.GetComponent<Interactable> ().coolDown = this.coolDown;
		spawn.GetComponent<Interactable> ().skillName = this.skillName;
		spawn.GetComponent<Interactable> ().player = this.player;

        if (this.skillType == SkillType.MaterialType)
        {
            spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(152 / 255.0F, 203 / 255.0F, 74 / 255.0F, 255f));
        }
        else if (this.skillType == SkillType.ControlType)
        {
            spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(247 / 255.0F, 216 / 255.0F, 66 / 255.0F, 255f));

        }
        else if (this.skillType == SkillType.GuardType)
        {

        }
        else if (this.skillType == SkillType.AdvanceType)
        {
            spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(250 / 255.0F, 130 / 255.0F, 40 / 255.0F, 255f));
        }
        else if (this.skillType == SkillType.AuraType)
        {
        }



        Debug.Log (spawn.GetComponent<Interactable> ().skillType);
	}
	private float x,y;
	private void genXY(){
		float playerX = player.transform.position.x;
		float playerY = player.transform.position.y;
		x = Random.Range (playerX - 10, playerX + 10);
		y = Random.Range (playerY - 10, playerY + 10);
		if (x < playerX + 3 && x > playerX - 3 && y < playerY + 3 && y > playerY - 3) {
			genXY ();
		}
	}

	void OnEnable(){
		initInteractable();
        
		SpawnPool.pool.addSpawn (this.spawn);


	}
	void OnDisable(){
		SpawnPool.pool.removeSpawn (skillName);
	}

	void Awake () {


		if (GameObject.FindWithTag ("Player") != null) {
			player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		} else {
			Debug.LogWarning ("Cannot Find Player");
		}
		interactable = Resources.Load ("SpawnedWeapon")as GameObject;
		projectile = Resources.Load ("Projectiles/Bolt")as GameObject;
		
		init ();//anything else we need to do before creating spawns 
		//StartCoroutine (interactableCreator ());//creates spawns at intervals
		if (coolDown == 999f) {
			if (this.skillType == SkillType.MaterialType) {
				coolDown = 3f;
			} else if (this.skillType == SkillType.ControlType) {

                coolDown = 7f;
			} else if (this.skillType == SkillType.GuardType) {
				coolDown = 7f;
			} else if (this.skillType == SkillType.AdvanceType) {
				coolDown = 10f;
			} else if (this.skillType == SkillType.AuraType) {
				coolDown = 15f;
			}
		}

		//initInteractable();
	}
}
//=== Skill Database =============================

public class SkillSystem: MonoBehaviour {

    public static SkillSystem f = null;


    public GameObject[] activeSkills;
	public Queue<GameObject> materialQueue = new Queue<GameObject> ();
	public Queue<GameObject> controlQueue = new Queue<GameObject> ();
	public Queue<GameObject> guardQueue = new Queue<GameObject> ();
	public Queue<GameObject> advanceQueue = new Queue<GameObject> ();
	public Queue<GameObject> auraQueue = new Queue<GameObject> ();
    public List<GameObject> backpack = new List<GameObject>();
	public int materialLimit = 3;
	public int controlLimit = 2;
	public int guardLimit = 2;
	public int advanceLimit = 2;
	public int auraLimit = 2;

	//logic for skill equipment. used for equipping skills and accessing skills in the storage.
	public void equipSkill(GameObject obj){
		if (obj.GetComponent<Skill>().skillType == SkillType.MaterialType) {
			if(materialQueue.Count >=materialLimit){

				GameObject temp = materialQueue.Dequeue ();
                temp.transform.parent = transform.FindChild("Inactive");
                backpack.Add(temp);

            }
            materialQueue.Enqueue (obj);
			obj.transform.parent = transform.FindChild("Material");

		}
		else if (obj.GetComponent<Skill>().skillType == SkillType.ControlType) {

            if (controlQueue.Count >= controlLimit)
            {

                GameObject temp = controlQueue.Dequeue();
                temp.transform.parent = transform.FindChild("Inactive");
                backpack.Add(temp);

            }
            controlQueue.Enqueue(obj);
            obj.transform.parent = transform.FindChild("Control");
        }
		else if (obj.GetComponent<Skill>().skillType == SkillType.GuardType) {
            if (guardQueue.Count >= guardLimit)
            {

                GameObject temp = guardQueue.Dequeue();
                temp.transform.parent = transform.FindChild("Inactive");
                backpack.Add(temp);

            }
            guardQueue.Enqueue(obj);
            obj.transform.parent = transform.FindChild("Guard");
        }
		else if (obj.GetComponent<Skill>().skillType == SkillType.AdvanceType) {
            if (advanceQueue.Count >= advanceLimit)
            {

                GameObject temp = advanceQueue.Dequeue();
                temp.transform.parent = transform.FindChild("Inactive");
                backpack.Add(temp);

            }
            advanceQueue.Enqueue(obj);
            obj.transform.parent = transform.FindChild("Advance");
        }
		else if (obj.GetComponent<Skill>().skillType == SkillType.AuraType) {
            if (auraQueue.Count >= auraLimit)
            {

                GameObject temp = auraQueue.Dequeue();
                temp.transform.parent = transform.FindChild("Inactive");
                backpack.Add(temp);

            }
            auraQueue.Enqueue(obj);
            obj.transform.parent = transform.FindChild("Aura");
        }

	}

    public GameObject makeSkill(string id) {
        GameObject skill = new GameObject("conebolts");
        skill.AddComponent(System.Type.GetType("Skill" + id));
        skill.name = skill.GetComponent<Skill>().skillName;
        return skill;
    }

	GameObject material, control, guard, advance, aura, inactive;
	public void init(){
		activeSkills = new GameObject[6];
		material = new GameObject ("Material");
		control = new GameObject("Control");
		guard = new GameObject ("Guard");
		advance = new GameObject("Advance");
		aura = new GameObject("Aura");
		inactive = new GameObject("Inactive");

		material.transform.parent = this.transform;
		activeSkills[0] = material;
		control.transform.parent = this.transform;
		activeSkills[1] = control;
		guard.transform.parent = this.transform;
		activeSkills[2] = guard;
		advance.transform.parent = this.transform;
		activeSkills[3] = advance;
		aura.transform.parent = this.transform;
		activeSkills[4] = aura;
		inactive.transform.parent = this.transform;
		inactive.SetActive (false);
		activeSkills[5] = inactive;





       
        equipSkill(makeSkill("004"));

		equipSkill (makeSkill("001"));

        equipSkill(makeSkill("012"));
        equipSkill(makeSkill("011"));

        equipSkill(makeSkill("031"));
        //Invoke("swapSkills", 10f);

        //blasterSkill.transform.parent = material.transform.parent;

        //		GameObject blasterSkill2 = Instantiate(new GameObject ("MaterialSkill"));;
        //		blasterSkill.AddComponent<Skill001> ();
        //blasterSkill.transform.parent = material.transform.parent;

        //		equipSkill ((GameObject)Instantiate(blasterSkill2)); 



        Debug.Log (activeSkills [0].GetComponentInChildren<Skill>().skillName);
		//activeSkills [0].AddComponent<Skill001> ();

		//skills.Add (new Skill001 ().init ());
		//		Debug.Log (skills [0].skillName); 
	}
}

//=======utility class=================//
public static class CoroutineUtil
{
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}
}
