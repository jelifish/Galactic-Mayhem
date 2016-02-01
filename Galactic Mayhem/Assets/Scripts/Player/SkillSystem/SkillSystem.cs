using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//public enum Activation { Button, Action, Automatic };
public enum SkillType { MaterialType, ControlType, BarrierType, BarrageType, AuraType };
public enum Rarity { Common, Rare, Legendary, Godly }
public enum SubType { Blaster, Repeater, Cannon, Force, Flow, Control, Bulwark, Melee, Siege, Missile, Beam, Elemental, Buff, Debuff, Misc, Turret, Auto };
//public enum SkillLevel { Lv1, Lv2, Lv3};

//=== 001 Compressed Energy =============================
public class Skill001 : Skill
{
    public override void init()
    {
        skillName = "Compressed Energy";
        skillDesc = "Fires a tight ball of stars. When nudged the right way can be very damaging.";
        skillType = SkillType.MaterialType;
        subType = SubType.Blaster;
        skillNum = 1;
    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill001Attr>();
    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill001Attr : Interactable
{
    public override void mouseUpFire()
    {
        timeResume();
    }
    public override void mouseDownFire()
    {
        timeSlow();
        StartCoroutine(fire());
    }


    void Start()
    {
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(.03f);
        initialSpeed = 5;
        while (true)
        {
            for (int i = 0; i < 50; i++)
            {
                GameObject temp = ObjectPool.pool.GetPooledObject();
                temp.transform.position = spawnPosition;
                temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f));
                Skillf.f.AddForce(temp, Skillf.lowForce * 1.3f);
                yield return new WaitForSeconds(.05f);
            }
            break;
        }
        OnDestroy();
        yield return new WaitForSeconds(5f);

    }
}
//=== 002 Conical Spurt =============================
public class Skill002 : Skill
{
    public override void init()
    {
        skillName = "Conical Spurt";
        skillDesc = "Blast away your foes with a shower of fire.";

        skillType = SkillType.MaterialType;
        subType = SubType.Blaster;
        skillNum = 2;
    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill002Attr>();

    }
    public override void attachSpecialAttributes()
    {
        spawn.AddComponent<Skill011Attr>();
    }
}
public class Skill002Attr : Interactable
{
    void Start()
    {
    }
    public override void mouseUpFire()
    {
        timeResume();
    }
    public float timeMultiplier = 1;

    public override void mouseDownFire()
    {
        timeSlow();
        StartCoroutine(fire());
    }
    IEnumerator fire()
    {
        yield return new WaitForSeconds(.03f);
        initialSpeed = 15;
        while (true)
        {
            for (int i = 0; i < 50; i++)
            {
                //GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f)));
                GameObject temp = ObjectPool.pool.GetPooledObject();
                temp.transform.position = this.transform.position;
                temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-15.0f, 15.0f));
                Skillf.f.AddForce(temp, Skillf.medForce);
                yield return new WaitForSeconds(.03f);
            }
            break;
        }
        OnDestroy(); //call at the very end to resume the time
                     //this.GetComponent<MeshRenderer> ().enabled = false;
        yield return new WaitForSeconds(3f);
        //Destroy (this.gameObject);
    }
}
//=== 003 Machine Gun =============================
public class Skill003 : Skill
{
    public override void init()
    {
        skillName = "Machine Gun";
        skillDesc = "A fast shooting stream of deadly stars will overwhelm your foes.";
        skillType = SkillType.MaterialType;
        subType = SubType.Repeater;
        skillNum = 3;
    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill003Attr>();
        spawn.GetComponent<Skill003Attr>().projectile = this.projectile;
    }
    public override void attachSpecialAttributes()
    {
        spawn.AddComponent<Skill011Attr>();
        spawn.GetComponent<Skill011Attr>().projectile = this.projectile;
    }
}
public class Skill003Attr : Interactable
{
    public override void mouseUpFire()
    {
        timeResume();
    }
    public override void mouseDownFire()
    {
        timeSlow();
        StartCoroutine(fire());
    }


    void Start()
    {
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(.03f);
        initialSpeed = 5;
        while (true)
        {
            for (int i = 0; i < 30; i++)
            {
                GameObject temp = ObjectPool.pool.GetPooledObject();
                temp.transform.position = spawnPosition;
                temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-0.5f, 0.5f));
                Skillf.f.AddForce(temp, Skillf.highForce * 1.3f);
                yield return new WaitForSeconds(.05f);
            }
            break;
        }
        OnDestroy();
        yield return new WaitForSeconds(5f);

    }
}
//=== 004 Charge Cluster =============================
public class Skill004 : Skill
{
    public override void init()
    {
        skillName = "Charge Cluster";
        skillDesc = "Hold down to charge energy. Then release.";
        skillType = SkillType.MaterialType;
        subType = SubType.Cannon;
        skillNum = 4;
        //coolDown = 3f; //cooldown can be initialized at this time.
    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill004Attr>();

    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill004Attr : Interactable
{
    void Start()
    {
    }
    public override void mouseUpFire()
    {
        timeResume();
        mouseUp = true;

    }
    public float timeMultiplier = 1;

    public override void mouseDownFire()
    {
        timeSlow();
        StartCoroutine(fire());
    }

    IEnumerator fire()
    {
        List<GameObject> bullets = new List<GameObject>();

        yield return new WaitForSeconds(.03f);
        initialSpeed = 15;
        while (true)
        {
            for (int i = 0; i < 25; i++)
            {
                //GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f)));
                GameObject temp = ObjectPool.pool.GetPooledObject();

                temp.transform.position = new Vector3(transform.position.x + Random.insideUnitCircle.x, transform.position.y + Random.insideUnitCircle.y, 0);
                temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(0, 360));
                temp.transform.localScale = temp.transform.localScale * 2;

                bullets.Add(temp);
                //Skillf.f.AddForce(temp, Skillf.highForce*Random.Range(.9f, 1.1f)*2);
                if (mouseUp)
                {
                    break;
                }
                yield return new WaitForSeconds(.1f);


            }
            yield return new WaitForSeconds(.01f);
            break;
        }
        while (true)
        {
            if (mouseUp)
            {
                mouseUp = false;
                Skillf.f.ForceTowardsDirection(bullets, this.transform.position, targetPosition, Skillf.highForce * Random.Range(.9f, 1.1f) * 2);
                break;
            }
            yield return new WaitForSeconds(.03f);

        }


        OnDestroy(); //call at the very end to resume the time
                     //this.GetComponent<MeshRenderer> ().enabled = false;
        yield return new WaitForSeconds(5f);
        //Destroy (this.gameObject);
    }
}
//=== 005 Shooting Star =============================
public class Skill005 : Skill
{
    public override void init()
    {
        skillName = "Shooting Star";
        skillType = SkillType.MaterialType;
        subType = SubType.Cannon;
        skillDesc = "Fires a few but superpowered rounds of hot plasma. Aim for greater accuracy. Autofire Enabled.";
        skillNum = 5;
    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill005Attr>();
    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill005Attr : Interactable
{
    public override void mouseUpFire()
    {
        timeResume();
    }
    public override void mouseDownFire()
    {
        timeSlow();
        StartCoroutine(fire());
    }


    void Start()
    {
    }
    //IEnumerator boost() {
    //}
    IEnumerator fire()
    {
        yield return new WaitForSeconds(.03f);
        initialSpeed = 5;
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(1f);

                Skillf.f.ExplosiveForceRandom50(this.transform.position, Skillf.lowForce / 2, 5);
                for (int j = 0; j < 5; j++)
                {
                    //GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f)));
                    GameObject tamp = ObjectPool.pool.GetPooledObject();
                    tamp.transform.position = new Vector3(transform.position.x + Random.insideUnitCircle.x, transform.position.y + Random.insideUnitCircle.y, 0);
                    tamp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(160, 200));


                    Skillf.f.AddForce(tamp, Skillf.lowForce / 4);
                    tamp.transform.localScale = tamp.transform.localScale;


                }




                GameObject temp = ObjectPool.pool.GetPooledObject();
                temp.transform.position = spawnPosition;
                //Vector3 relativePos = targetPosition - spawnPosition;
                temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, 0.0f);
                //temp.GetComponent<SphereCollider>().radius *= 2;
                temp.transform.localScale = temp.transform.localScale * 1.5f;
                temp.GetComponent<ProjectileCollision>().hull = 3;
                Skillf.f.AddForce(temp, Skillf.highForce * 4f);
                yield return new WaitForSeconds(.03f);
            }











            break;
        }
        OnDestroy();
        yield return new WaitForSeconds(5f);

    }
}
//=== 006 Triple Tap Stream =============================
public class Skill006 : Skill
{
    public override void init()
    {
        skillName = "Triple Tap";
        skillDesc = "Hold your fire to pulse three puffs of stars.";
        skillType = SkillType.MaterialType;
        subType = SubType.Repeater;
        skillNum = 6;
    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill006Attr>(); ///edit this
    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill006Attr : Interactable
{
    public override void mouseUpFire()
    {
        timeResume();
    }
    public override void mouseDownFire()
    {
        timeSlow();
        StartCoroutine(fire());
    }


    void Start()
    {
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(.03f);
        initialSpeed = 5;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    GameObject temp = ObjectPool.pool.GetPooledObject();
                    temp.transform.position = spawnPosition;
                    temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f));
                    Skillf.f.AddForce(temp, Skillf.highForce);
                    yield return new WaitForSeconds(.001f);
                }
                if (mouseUp)
                {
                    break;
                }
                yield return new WaitForSeconds(1f);
            }

            break;
        }
        OnDestroy();
        yield return new WaitForSeconds(5f);

    }
}
//=== 011 Accelerator =============================
public class Skill011 : Skill
{
    public override void init()
    {
        skillName = "Accelerator";
        skillDesc = "Pulls surrounding stars towards target location. Aim Well.";
        skillType = SkillType.ControlType;
        subType = SubType.Force;
        skillNum = 11;

    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill011Attr>();
    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill011Attr : Interactable
{

    public List<GameObject> bullets = new List<GameObject>();


    public override void mouseUpFire()
    {
        Timef.f.SpeedTime(2f);
        Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, 7);
        //Debug.Log (transform.position); //spawn position
        //Debug.Log (targetPosition); //spawn position
        //int i = 0;

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Bullet")
            {
                bullets.Add(col.gameObject);
            }
        }
        StartCoroutine(Blast());
    }

    public override void mouseDownFire()
    {

        Timef.f.SlowTime(2f);
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;

    }

    public override void onEnable()
    {
        bullets = new List<GameObject>();
    }
    void Start()
    {
    }

    IEnumerator Blast()
    {
        //yield return new WaitForSeconds (0.2f);

        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.medForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.highForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.highForce);
        yield return new WaitForSeconds(0.1f);

        OnDestroy();
    }
}
//=== 012 Collapse =============================
public class Skill012 : Skill
{
    public override void init()
    {
        skillName = "Collapse";
        skillDesc = "Warps space to pull in all nearby stars at a location. Fast executing skill.";
        skillType = SkillType.ControlType;
        subType = SubType.Force;
        skillNum = 12;

    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill012Attr>();
    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill012Attr : Interactable
{

    public List<GameObject> bullets = new List<GameObject>();
    public override void onEnable()
    {
        bullets = new List<GameObject>();
    }


    public override void mouseUpFire()
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 15);
        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Bullet")
            {
                bullets.Add(col.gameObject);
            }
        }

        StartCoroutine(Blast());

    }
    public override void mouseDownFire()
    {

        //StartCoroutine (Blast ());
    }


    void Start()
    {

    }

    IEnumerator Blast()
    {
        yield return new WaitForSeconds(.03f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.highForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.medForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.highForce);
        //Destroy (this.gameObject);
        yield return new WaitForSeconds(0.1f);
        OnDestroy();
    }
}
//=== 013 Void Explosion =============================
public class Skill013 : Skill
{
    public override void init()
    {
        skillName = "Void Explosion";
        skillDesc = "Pulls in, then violently explodes.";
        skillType = SkillType.ControlType;
        subType = SubType.Force;
        skillNum = 13;

    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill013Attr>();
    }
    public override void attachSpecialAttributes()
    {

    }
}
public class Skill013Attr : Interactable
{

    public List<GameObject> bullets = new List<GameObject>();
    public override void onEnable()
    {
        bullets = new List<GameObject>();
    }


    public override void mouseUpFire()
    {
        Timef.f.SpeedTime(2f);
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 20);
        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Bullet")
            {
                bullets.Add(col.gameObject);
            }
        }
        StartCoroutine(Blast());
    }

    public override void mouseDownFire()
    {
        Timef.f.SlowTime(2f);

    }


    void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(247 / 255.0F, 216 / 255.0F, 66 / 255.0F, 255f));
    }

    IEnumerator Blast()
    {

        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.highForce);
        yield return new WaitForSeconds(0.5f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds(0.5f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds(0.5f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.medForce);
        yield return new WaitForSeconds(0.5f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.medForce);
        yield return new WaitForSeconds(0.5f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.medForce);
        yield return new WaitForSeconds(0.2f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);
        yield return new WaitForSeconds(0.1f);
        Skillf.f.ForceTowardsPoint(bullets, targetPosition, Skillf.lowForce);

        yield return new WaitForSeconds(0.1f);
        Skillf.f.Freeze(bullets);
        Skillf.f.ExplosiveForceRandom50(bullets, targetPosition, Skillf.highForce * 7);

        yield return new WaitForSeconds(0.5f);
        //Destroy (this.gameObject);
        OnDestroy();
        yield return new WaitForSeconds(0.1f);
        //		
    }
}
//=== 014 Ebb and Flow =============================
public class Skill014 : Skill
{
    public override void init()
    {
        skillName = "Ebb and Flow";
        skillDesc = "Manipulate a constant torrent of stars. Small blast at the end.";
        skillType = SkillType.ControlType;
        subType = SubType.Flow;
        skillNum = 14;

    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill014Attr>();
    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill014Attr : Interactable
{

    public List<GameObject> bullets = new List<GameObject>();
    public override void onEnable()
    {
        bullets = new List<GameObject>();
    }


    public override void mouseUpFire()
    {


        //StartCoroutine(Blast());

    }
    public override void mouseDownFire()
    {

        StartCoroutine(Blast());
    }


    void Start()
    {

    }

    IEnumerator Blast()
    {
        List<GameObject> tempList = new List<GameObject>();
        float area = 3;
        while (true)
        {


            yield return new WaitForSeconds(.05f);
            Collider[] hitColliders = Physics.OverlapSphere(targetPosition, area);
            foreach (Collider col in hitColliders)
            {
                if (col.tag == "Bullet")
                {
                    if (!bullets.Contains(col.gameObject))
                    {
                        bullets.Add(col.gameObject);
                    }


                }
            }

            tempList = new List<GameObject>(bullets);
            foreach (GameObject bullet in bullets)
            {
                //Debug.Log(Vector3.Distance(bullet.transform.position, targetPosition));
                if (Vector3.Distance(bullet.transform.position, targetPosition) >= area * 3)
                {

                    if (tempList.Contains(bullet) && Random.value > .99f)
                    {
                        tempList.Remove(bullet);
                    }
                }
                if (Vector3.Distance(bullet.transform.position, targetPosition) >= 25)
                {

                    if (tempList.Contains(bullet) && Random.value > .1f)
                    {
                        tempList.Remove(bullet);
                    }
                }

            }
            Skillf.f.ForceTowardsPoint(tempList, targetPosition, Skillf.lowForce / 2);
            Skillf.f.ExplosiveForceRandom50(tempList, new Vector3(targetPosition.x + (Random.insideUnitCircle * 2).x, targetPosition.y + (Random.insideUnitCircle * 2).y, 0), Skillf.lowForce, area);
            //bullets = tempList;

            if (mouseUp)
            {
                //Skillf.f.ForceTowardsPoint(tempList, targetPosition, Skillf.lowForce / 2);
                Skillf.f.ExplosiveForceRandom50(tempList, targetPosition, Skillf.highForce * 2);
                break;
            }
            bullets = new List<GameObject>(tempList);
            tempList.Clear();

        }
        yield return new WaitForSeconds(0.3f);
        OnDestroy();
    }
}

//=== 031 Missile =============================
public class Skill031 : Skill
{
    public override void init()
    {
        skillName = "Missile";
        skillDesc = "Fires a missile that explodes into shards of hot plasma.";
        skillType = SkillType.BarrageType;
        subType = SubType.Missile;
        skillNum = 31;
    }
    public override void attachAttributes()
    {
        spawn.AddComponent<Skill031Attr>();
    }
    public override void attachSpecialAttributes()
    {
    }
}
public class Skill031Attr : Interactable
{
    //public List<GameObject> bullets = new List<GameObject>();
    public List<GameObject> missiles; // initialize all global lists -like things in OnEnable

    public GameObject bolt = Resources.Load("Projectiles/Bolt") as GameObject;

    public override void mouseUpFire()
    {
        Timef.f.SpeedTime(2f);
    }
    public override void mouseDownFire()
    {
        Timef.f.SlowTime(2f);
        StartCoroutine(fire());
    }


    public override void onEnable()
    {
        missiles = new List<GameObject>();
    }
    void Start()
    {
        this.projectile = Resources.Load("Projectiles/Missile") as GameObject;
    }


    IEnumerator fire()
    {
        yield return new WaitForSeconds(.03f);
        initialSpeed = 5;

        //while (true) {

        //bullets.Add(temp.gameObject);
        StartCoroutine(createMissiles());
        while (GetComponent<SpawnedWeapon>().towardsObject.activeSelf)
        {
            foreach (GameObject missile in missiles)
            {
                Vector3 targetVector = (GetComponent<SpawnedWeapon>().towardsObject.transform.position - missile.transform.position).normalized;
                //missile.GetComponent<Rigidbody>().AddForce(targetVector * 25);
                missile.GetComponent<Rigidbody>().velocity = targetVector * 20;
            }
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(.01f);
        foreach (GameObject missile in missiles)
        {
            missile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        yield return new WaitForSeconds(.01f);

        while (true)
        {
            for (int i = 0; i < 150; i++)//150
            {
                foreach (GameObject missile in missiles)
                {
                    //GameObject bolty= (GameObject)Instantiate (bolt, new Vector3(missile.transform.position.x+Random.insideUnitCircle.x, missile.transform.position.y+Random.insideUnitCircle.y,0), this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(0, 360)));
                    GameObject temp = ObjectPool.pool.GetPooledObject();
                    Vector3 randomcircle = Random.insideUnitCircle;
                    temp.transform.position = new Vector3(missile.transform.position.x + (randomcircle.x * 2), missile.transform.position.y + (randomcircle.y * 2), 0);
                    temp.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(0f, 360f));
                    //Skillf.f.ExplosiveForce (bolty,missile.transform.position);
                    //bolty.GetComponent<ProjectileCollision>().speed = Random.Range(initialSpeed*.9f, initialSpeed*1.1f);

                }
            }
            break;
        }
        yield return new WaitForSeconds(.01f);

        foreach (GameObject missile in missiles)
        {
            Collider[] hitColliders = Physics.OverlapSphere(missile.transform.position, 10);
            //Debug.Log(missile.transform.position);
            List<GameObject> bullets = new List<GameObject>();
            foreach (Collider col in hitColliders)
            {
                if (col.tag == "Bullet")
                {

                    //col.GetComponentInChildren<RotateTowards> ().towardsObject = this.gameObject;
                    bullets.Add(col.gameObject);

                }
            }
            //missile.GetComponent<MeshRenderer> ().enabled = false;
            StartCoroutine(Blast(missile.transform.position, bullets));
            yield return new WaitForSeconds(.1f);

            //missiles.Remove(missile);
            Destroy(missile);
            yield return new WaitForSeconds(.01f);
        }


        //StartCoroutine (Blast ());

        yield return new WaitForSeconds(1f);

        OnDestroy();
        //this.GetComponent<MeshRenderer> ().enabled = false;
        yield return new WaitForSeconds(5f);
        //Destroy (this.gameObject);

    }
    IEnumerator createMissiles()
    {

        for (int i = 0; i < 1; i++)
        {
            if (GetComponent<SpawnedWeapon>().towardsObject != null)
            {
                GameObject temp = (GameObject)Instantiate(projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(0f, 360f)));
                //temp.GetComponent<Mover> ().speed = Random.Range (initialSpeed * .9f, initialSpeed * 1.7f);

                missiles.Add(temp);

            }

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(.1f);
        //yield return new WaitForSeconds (.5f);
    }
    IEnumerator Blast(Vector3 blastPos, List<GameObject> bullets)
    {
        yield return new WaitForSeconds(Random.Range(.1f, .5f));


        Skillf.f.ExplosiveForce(bullets, new Vector3(blastPos.x + (Random.insideUnitCircle * 2).x, blastPos.y + (Random.insideUnitCircle * 2).y, 0), Skillf.lowForce);
        Skillf.f.ExplosiveForce(bullets, new Vector3(blastPos.x + (Random.insideUnitCircle * 2).x, blastPos.y + (Random.insideUnitCircle * 2).y, 0), Skillf.lowForce);
        Skillf.f.ExplosiveForce(bullets, new Vector3(blastPos.x + (Random.insideUnitCircle * 2).x, blastPos.y + (Random.insideUnitCircle * 2).y, 0), Skillf.lowForce);
        yield return new WaitForSeconds(.5f);
        Skillf.f.ExplosiveForceRandom50(bullets, blastPos, Skillf.highForce * 2, 10);
        yield return new WaitForSeconds(0.1f);

    }


}





//=== Skill Class ===============================
public class Skill : MonoBehaviour
{
    public SkillType skillType = SkillType.MaterialType; // this is the type of skill
    public SubType subType = SubType.Repeater;
    public Rarity rarity = Rarity.Common;
    public string skillDesc = "Nul";
    public string skillName = "Nul"; //skillname is set in the child
    public string slotAffinityDescription = "Nul";
    public int skillNum = 0;
    public int slotAffinity = 0; //randomized 0 1 2 3. slot positions. 
    public int slotPosition = 0;

    public float strength = 0;
    public float wisdom = 0;
    public float agility = 0;

    public Sprite sprite = Resources.Load<Sprite>("Skills/000Default");

    public GameObject projectile = null; //this is the projectile that the skill fires
    public PlayerController player; // this is the player object
    public float coolDown = 999f; // FLOAT this is the cooldown of the skill. initialized to 999f. if it is still 999 at the end of init process, default values are chosen.
    public GameObject interactable; //this is the spawned weapon

    //skills exp system
    public int skillLevel = 0;// skills start out at level 0. probably a good idea not to override this
    public int levelCap = 1; //the level cap. max level can be initialized to something higher. for every skilllevel, majorlevel++
    public float expInit = 5f;//exp per level is calculated by (expInit * Mathf.Pow(expRatio, x)); where x is the level
    public float expRatio = 1.618034f; //golden ratio curve. slower curves use a smaller value >1. negative curves use values <1. 
    protected float exp = 0; //current exp
    public int calcExpLimit()
    {
        return (int)(expInit * Mathf.Pow(expRatio, skillLevel));
    }
    public void addExp()
    {
        exp += 1;
        if (exp > calcExpLimit())
        {//if exp is greater that the current limit levelup()
            levelUp();
        }
    }
    public void addExp(int x)
    {
        for (int i = 0; i < x; i++)
        {
            exp += 1;
            if (exp >= calcExpLimit())
            {//if exp is greater that the current limit levelup()
                levelUp();
            }
        }
    }
    public virtual void levelUp()
    {
        exp = 0; //reset current exp
        skillLevel += 1;


    }
    public void rarityUP()
    {
        rarity += 1;
    }

    public virtual void init()
    {// called before initialization of spawner
    }
    public virtual void attachAttributes()
    { //done in child, assigns to the spawner script a script that determines what the skill will do when clicked
    }
    public virtual void attachSpecialAttributes()
    {
    }
    IEnumerator interactableCreator() //creates a spawn
    {
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        while (true)
        {
            yield return new WaitForSeconds(coolDown);
            //spawnInteractable();

        }
    }

    public GameObject spawn;//do not mess with this
    public bool isSpecialOn = false;
    
    public void initInteractable()
    { //spawner instatiation at the correct random coords
      //genXY ();
      //spawn = new GameObject();
        spawn = (GameObject)Instantiate(interactable);

        if (isSpecialOn)
        {
            attachSpecialAttributes();
            isSpecialOn = false;
        }
        else {
            attachAttributes();
        }


        //set the spawn texture shape
        if (this.subType == SubType.Blaster)
        {
            //spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(152 / 255.0F, 203 / 255.0F, 74 / 255.0F, 255f));
            sprite = Resources.Load<Sprite>("Skills/Blaster");
            
        }
        else if (this.subType == SubType.Repeater)
        {
            sprite = Resources.Load<Sprite>("Skills/Repeater");
        }
        else if (this.subType == SubType.Cannon)
        {
            sprite = Resources.Load<Sprite>("Skills/Cannon");
        }
        else if (this.subType == SubType.Force)
        {
            sprite = Resources.Load<Sprite>("Skills/Force");
            // spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(250 / 255.0F, 130 / 255.0F, 40 / 255.0F, 255f));
        }
        else if (this.subType == SubType.Flow)
        {
            sprite = Resources.Load<Sprite>("Skills/Flow");
        }
        else if (this.subType == SubType.Control)
        {
           sprite = Resources.Load<Sprite>("Skills/Control");
        }
        else if (this.subType == SubType.Bulwark)
        {
            // sprite = Resources.Load<Sprite>("Skills/Bulwark");
        }
        else if (this.subType == SubType.Melee)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Siege)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Missile)
        {
            sprite = Resources.Load<Sprite>("Skills/Missile");
        }

        else if (this.subType == SubType.Beam)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Elemental)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Buff)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Debuff)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Misc)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Turret)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        else if (this.subType == SubType.Auto)
        {
            // sprite = Resources.Load<Sprite>("Skills/");
        }

        spawn.GetComponent<Renderer>().material.SetTexture("_MainTex", sprite.texture);


        //initialize all known variables
        spawn.GetComponent<Interactable>().skillType = this.skillType;
        spawn.GetComponent<Interactable>().coolDown = this.coolDown;
        spawn.GetComponent<Interactable>().skillName = this.skillName;
        spawn.GetComponent<Interactable>().skillObject = this.gameObject;
        //spawn.GetComponent<Interactable>().skillDesc = this.skillDesc; //do not really need
        spawn.GetComponent<Interactable>().player = this.player;

        if (this.skillType == SkillType.MaterialType)
        {
            spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(152 / 255.0F, 203 / 255.0F, 74 / 255.0F, 255f));
        }
        else if (this.skillType == SkillType.ControlType)
        {
            spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(247 / 255.0F, 216 / 255.0F, 66 / 255.0F, 255f));

        }
        else if (this.skillType == SkillType.BarrierType)
        {

        }
        else if (this.skillType == SkillType.BarrageType)
        {
            spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(250 / 255.0F, 130 / 255.0F, 40 / 255.0F, 255f));
        }
        else if (this.skillType == SkillType.AuraType)
        {
        }



        //Debug.Log (spawn.GetComponent<Interactable> ().skillType);
    }

    void OnEnable()
    {

        Debug.Log("Created Skill: "+ this.skillName);
        initInteractable();

        SpawnPool.pool.addSpawn(this.spawn);
        //Texture texture = new Texture;
        GameObject touchEvent = spawn.GetComponent<SpawnedWeapon>().touchEvent;
        if (slotPosition == 0)
        {
            touchEvent.transform.eulerAngles = new Vector3(touchEvent.transform.rotation.eulerAngles.x, touchEvent.transform.rotation.eulerAngles.y, 0);
        }
        else if (slotPosition == 1)
        {
            touchEvent.transform.eulerAngles = new Vector3(touchEvent.transform.rotation.eulerAngles.x, touchEvent.transform.rotation.eulerAngles.y, 270);
        }
        else if (slotPosition == 2)
        {
            touchEvent.transform.eulerAngles = new Vector3(touchEvent.transform.rotation.eulerAngles.x, touchEvent.transform.rotation.eulerAngles.y, 90);
        }
        else if (slotPosition == 3)
        {
            touchEvent.transform.eulerAngles = new Vector3(touchEvent.transform.rotation.eulerAngles.x, touchEvent.transform.rotation.eulerAngles.y, 180);
        }
        //spawn.GetComponent<SpawnedWeapon>().touchEvent.GetComponent<Material>().mainTexture = texture


    }
    void OnDisable()
    {
        if (SpawnPool.pool != null)
        {
            SpawnPool.pool.removeSpawn(this.gameObject);
        }
    }

    void Awake()
    {


        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        else {
            Debug.LogWarning("Cannot Find Player");
        }
        interactable = Resources.Load("SpawnedWeapon") as GameObject;
        projectile = Resources.Load("Projectiles/Bolt") as GameObject;
        sprite = Resources.Load<Sprite>("Skills/003Accelerator");

        init();//anything else we need to do before creating spawns 
               //StartCoroutine (interactableCreator ());//creates spawns at intervals
        if (coolDown == 999f)
        {
            if (this.skillType == SkillType.MaterialType)
            {
                coolDown = 3f;
            }
            else if (this.skillType == SkillType.ControlType)
            {
                coolDown = 7f;
            }
            else if (this.skillType == SkillType.BarrierType)
            {
                coolDown = 7f;
            }
            else if (this.skillType == SkillType.BarrageType)
            {
                coolDown = 10f;
            }
            else if (this.skillType == SkillType.AuraType)
            {
                coolDown = 15f;
            }
        }

        //initInteractable();
    }
}
//=== Skill Database =============================

public class SkillSystem : MonoBehaviour
{

    public SkillMenuController mc;

    SkillType currentPanelSkillType;
    public GameObject skillMenu; //inventorypanel, skillequipmenu
    private GameObject panels;
    public GameObject materialSlots; //slotPanel

    public GameObject skillSlot; //inventoryslot
    public GameObject skill; //inventoryitem

    private int slotAmount;

    public List<GameObject> backpack = new List<GameObject>();
    public List<GameObject> slots = new List<GameObject>();

    public void setCurrentPanelSkillType(int i)
    {
        saveSlots();
        if (i == 0)
        {
            currentPanelSkillType = SkillType.MaterialType;
            mc.changeCurrentSkillType(SkillType.MaterialType);
            slotAmount = materialSlotCapacity;
        }
        else if (i == 1)
        {
            currentPanelSkillType = SkillType.ControlType;
            mc.changeCurrentSkillType(SkillType.ControlType);
            slotAmount = controlSlotCapacity;
        }
        else if (i == 2)
        {
            currentPanelSkillType = SkillType.BarrierType;
            mc.changeCurrentSkillType(SkillType.BarrierType);
            slotAmount = barrierSlotCapacity;
        }
        else if (i == 3)
        {
            currentPanelSkillType = SkillType.BarrageType;
            mc.changeCurrentSkillType(SkillType.BarrageType);
            slotAmount = barrageSlotCapacity;
        }
        else if (i == 4)
        {
            currentPanelSkillType = SkillType.AuraType;
            mc.changeCurrentSkillType(SkillType.AuraType);
            slotAmount = auraSlotCapacity;
        }
        refreshPanel();
        mc.clearInformation();
        mc.populateSlots();
        //  currentPanelSkillType = type;
        
    }


    //public Queue<GameObject> materialQueue = new Queue<GameObject>();
    //public Queue<GameObject> controlQueue = new Queue<GameObject>();
    //public Queue<GameObject> barrierQueue = new Queue<GameObject>();
    //public Queue<GameObject> barrageQueue = new Queue<GameObject>();
    //public Queue<GameObject> auraQueue = new Queue<GameObject>();
    public List<Skill> materialList = new List<Skill>();
    public List<Skill> controlList = new List<Skill>();
    public List<Skill> barrierList = new List<Skill>();
    public List<Skill> barrageList = new List<Skill>();
    public List<Skill> auraList = new List<Skill>();


    public void saveSlots()
    {
        wipeSlot();
        if (currentPanelSkillType == SkillType.MaterialType)
        {
            if (mc.skillSlots.Count >= 1)
            {
                foreach (Text skilltext in mc.skillSlots)
                {
                    materialList.Add(skilltext.GetComponent<SkillData>().skill);
                }
            }
        }
        else if (currentPanelSkillType == SkillType.ControlType)
        {
            if (mc.skillSlots.Count >= 1)
            {
                foreach (Text skilltext in mc.skillSlots)
                {
                    controlList.Add(skilltext.GetComponent<SkillData>().skill);
                }
            }
        }
        else if (currentPanelSkillType == SkillType.BarrierType)
        {
            if (mc.skillSlots.Count >= 1)
            {
                foreach (Text skilltext in mc.skillSlots)
                {
                    barrierList.Add(skilltext.GetComponent<SkillData>().skill);
                }
            }
        }
        else if (currentPanelSkillType == SkillType.BarrageType)
        {
            if (mc.skillSlots.Count >= 1)
            {
                foreach (Text skilltext in mc.skillSlots)
                {
                    barrageList.Add(skilltext.GetComponent<SkillData>().skill);
                }
            }
        }
        else if (currentPanelSkillType == SkillType.AuraType)
        {
            if (mc.skillSlots.Count >= 1)
            {
                foreach (Text skilltext in mc.skillSlots)
                {
                    auraList.Add(skilltext.GetComponent<SkillData>().skill);
                }
            }
        }

    }
    public void wipeSlot() {
        if (currentPanelSkillType == SkillType.MaterialType)
        {

            materialList.Clear();

        }
        else if (currentPanelSkillType == SkillType.ControlType)
        {

            controlList.Clear();

        }
        else if (currentPanelSkillType == SkillType.BarrierType)
        {

             barrierList.Clear();

        }
        else if (currentPanelSkillType == SkillType.BarrageType)
        {

             barrageList.Clear();

        }
        else if (currentPanelSkillType == SkillType.AuraType)
        {

             auraList.Clear();

        }
    }

    public List<Skill> getSlots()
    {
        if (currentPanelSkillType == SkillType.MaterialType)
        {

            return materialList;

        }
        else if (currentPanelSkillType == SkillType.ControlType)
        {

            return controlList;

        }
        else if (currentPanelSkillType == SkillType.BarrierType)
        {

            return barrierList;

        }
        else if (currentPanelSkillType == SkillType.BarrageType)
        {

            return barrageList;

        }
        else if (currentPanelSkillType == SkillType.AuraType)
        {

            return auraList;

        }
        else return null;
    }

    void createSlots()
    {
        //removeSlots();
        slots.ForEach(child => Destroy(child));
        slots.Clear();
        for (int i = 0; i < slotAmount; i++)
        {

            slots.Add(Instantiate(skillSlot));
            slots[i].transform.SetParent(materialSlots.transform);
            slots[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        }

    }
    void Start()
    {

        materialLimit = 3;
        materialSlotCapacity = 20;
        controlLimit = 2;
        controlSlotCapacity = 8;
        barrierLimit = 2;
        barrierSlotCapacity = 4;
        barrageLimit = 2;
        barrageSlotCapacity = 4;
        auraLimit = 2;
        auraSlotCapacity = 2;
        setCurrentPanelSkillType(0);
        createSlots();
        materialSlots.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (((int)(slotAmount - 20) / 4) + Mathf.Ceil(slotAmount % 4)) * 84);


        //skillMenu.SetActive(false);
    }

    public void clearPanels()
    {
        var children = new List<GameObject>();
        foreach (RectTransform singleSlot in materialSlots.GetComponent<RectTransform>())
        {
            //if (singleSlot.name.Equals("Slot(Clone)")) { children.Add(singleSlot.gameObject); }
            
            foreach (RectTransform singleSkill in singleSlot.GetComponent<RectTransform>())
            {

               // Debug.Log(singleSkill.name);
                children.Add(singleSkill.gameObject);

            }
        }
        //Debug.Log(children.ToArray());
        children.ForEach(child => Destroy(child));

    }
    public void refreshPanel()
    {
        clearPanels();
        createSlots();
        List<GameObject> currentBackpack = new List<GameObject>();
        foreach (GameObject obj in backpack)
        {
            if (obj.GetComponent<Skill>().skillType == currentPanelSkillType)
            {

                currentBackpack.Add(obj);

            }
        }
        for (int i = 0; i < currentBackpack.Count && i < slotAmount; i++)
        {

            GameObject itemObj = Instantiate(skill);
            itemObj.transform.SetParent(slots[i].transform);
            itemObj.GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, .8f);
            itemObj.transform.position = slots[i].transform.position;
            itemObj.GetComponent<SkillData>().skill = currentBackpack[i].GetComponent<Skill>();
            itemObj.GetComponent<SkillData>().mc = this.mc;
            itemObj.GetComponent<Image>().sprite = currentBackpack[i].GetComponent<Skill>().sprite;
            itemObj.name = currentBackpack[i].GetComponent<Skill>().skillName;



        }

        materialSlots.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (((int)(slotAmount - 20) / 4) + Mathf.Ceil(slotAmount % 4)) * 84);
    }










    //public static SkillSystem f = null;
    //void Awake() { f = this; }

    ////public GameObject[] activeSkills;
    //public Queue<GameObject> materialQueue = new Queue<GameObject>();
    //public Queue<GameObject> controlQueue = new Queue<GameObject>();
    //public Queue<GameObject> barrierQueue = new Queue<GameObject>();
    //public Queue<GameObject> barrageQueue = new Queue<GameObject>();
    //public Queue<GameObject> auraQueue = new Queue<GameObject>();

    public int materialLimit;
    public int materialSlotCapacity;
    public int controlLimit;
    public int controlSlotCapacity;
    public int barrierLimit;
    public int barrierSlotCapacity;
    public int barrageLimit;
    public int barrageSlotCapacity;
    public int auraLimit;
    public int auraSlotCapacity;

    public List<GameObject> activeSkills = new List<GameObject>();

    public void equipSkills(GameObject skill)
    {
        saveSlots();

        activeSkills.Add(skill);
        skill.transform.parent = this.transform;




    }
    public void equipSkills(List<GameObject> skills)
    {
        saveSlots();
        if (skills.Count >= 1)
        {
            foreach (GameObject skill in skills)
            {
                activeSkills.Add(skill);
                skill.transform.parent = this.transform;
            }
        }


    }

    public void removeSkills(SkillType type)
    {
        if (activeSkills.Count >= 1)
        {
            foreach (GameObject obj in activeSkills)
            {
                if (obj.GetComponent<Skill>().skillType == type)
                {
                    obj.transform.parent = transform.FindChild("Inactive");
                }
            }
        }
    }


    //public List<GameObject> getEquipped(SkillType type)
    //{
    //    List<GameObject> list = new List<GameObject>();
    //    if (type == SkillType.MaterialType)
    //    {
    //        foreach (GameObject obj in materialQueue)
    //        {
    //            list.Add(obj);
    //        }
    //    }
    //    return list;
    //}

    //logic for skill equipment. used for equipping skills and accessing skills in the storage.
    //public void equipSkill(GameObject obj)
    //{
    //    if (obj.GetComponent<Skill>().skillType == SkillType.MaterialType)
    //    {
    //        if (materialQueue.Count >= materialLimit)
    //        {

    //            GameObject temp = materialQueue.Dequeue();
    //            temp.transform.parent = transform.FindChild("Inactive");

    //        }
    //        materialQueue.Enqueue(obj);
    //        obj.transform.parent = transform.FindChild("Material");

    //    }
    //    else if (obj.GetComponent<Skill>().skillType == SkillType.ControlType)
    //    {

    //        if (controlQueue.Count >= controlLimit)
    //        {

    //            GameObject temp = controlQueue.Dequeue();
    //            temp.transform.parent = transform.FindChild("Inactive");

    //        }
    //        controlQueue.Enqueue(obj);
    //        obj.transform.parent = transform.FindChild("Control");
    //    }
    //    else if (obj.GetComponent<Skill>().skillType == SkillType.BarrierType)
    //    {
    //        if (barrierQueue.Count >= barrierLimit)
    //        {

    //            GameObject temp = barrierQueue.Dequeue();
    //            temp.transform.parent = transform.FindChild("Inactive");

    //        }
    //        barrierQueue.Enqueue(obj);
    //        obj.transform.parent = transform.FindChild("Barrier");
    //    }
    //    else if (obj.GetComponent<Skill>().skillType == SkillType.BarrageType)
    //    {
    //        if (barrageQueue.Count >= barrageLimit)
    //        {

    //            GameObject temp = barrageQueue.Dequeue();
    //            temp.transform.parent = transform.FindChild("Inactive");

    //        }
    //        barrageQueue.Enqueue(obj);
    //        obj.transform.parent = transform.FindChild("Barrage");
    //    }
    //    else if (obj.GetComponent<Skill>().skillType == SkillType.AuraType)
    //    {
    //        if (auraQueue.Count >= auraLimit)
    //        {

    //            GameObject temp = auraQueue.Dequeue();
    //            temp.transform.parent = transform.FindChild("Inactive");

    //        }
    //        auraQueue.Enqueue(obj);
    //        obj.transform.parent = transform.FindChild("Aura");
    //    }

    //}
    public void addSkill(GameObject obj)
    {
        backpack.Add(obj);
        obj.transform.parent = transform.FindChild("Inactive");
        refreshPanel();
    }
    public GameObject makeSkill(string id)
    {
        GameObject tempSkill = new GameObject();
        //skill.SetActive(false);
        tempSkill.AddComponent(System.Type.GetType("Skill" + id));
        tempSkill.name = tempSkill.GetComponent<Skill>().skillName;
        addSkill(tempSkill);
        return tempSkill;
    }
    public GameObject makeSkill(int id)
    {

        string skillid = id.ToString();
        if (id < 100)
        {
            skillid = "0" + skillid;
        }
        if (id < 10)
        {
            skillid = "0" + skillid;
        }

        return makeSkill(skillid);
    }

    GameObject material, control, guard, barrage, aura, inactive;
    public void init()
    {
        //activeSkills = new GameObject[6];
        material = new GameObject("Material");
        control = new GameObject("Control");
        guard = new GameObject("Barrier");
        barrage = new GameObject("Barrage");
        aura = new GameObject("Aura");
        inactive = new GameObject("Inactive");

        material.transform.parent = this.transform;
        //activeSkills[0] = material;
        control.transform.parent = this.transform;
        //activeSkills[1] = control;
        guard.transform.parent = this.transform;
        //activeSkills[2] = guard;
        barrage.transform.parent = this.transform;
        //activeSkills[3] = barrage;
        aura.transform.parent = this.transform;
        //activeSkills[4] = aura;
        inactive.transform.parent = this.transform;
        inactive.SetActive(false);
        //activeSkills[5] = inactive;






        makeSkill(1);
        makeSkill(2);
        makeSkill(3);
        makeSkill(4);
        makeSkill(5);
        makeSkill(6);
        //makeSkill(7);
        
        makeSkill("011");
        makeSkill("012");
        makeSkill("013");
        makeSkill("014");
        //equipSkill(makeSkill(1));

        

        makeSkill(31);

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
