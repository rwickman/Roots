using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverMech : MonoBehaviour
{

    public GameObject projectile;
    
    public float[] fireRateEachStage = new float[3]{0.6f, 0.6f, 0.3f};
    public float lookDirOffset = 90.0f;
    public float shootNoiseMagnitude = 1.0f;
    
    private int curStage = 0;
    private StageTracker stageTracker;


    private float timeSinceLastFire = 1000.0f;
    private GameObject player;
    private float[] burstAngles = new float[3]{35.0f, 0.0f, -35.0f};
    private int numDirsForAllDirs = 8;
    private int lastShotCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        stageTracker = GetComponent<StageTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        curStage = stageTracker.GetStage();
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire >= fireRateEachStage[curStage])
        {
            
            if (curStage == 0)
            {
                ShootAtPlayer();
            }
            else if(curStage == 1){
                BurstShootAtPlayer();
            }
            else
            {
                if (lastShotCounter == 0)
                {
                    ShootInAllDirections();
                }
                else if(lastShotCounter == 1)
                {
                    ShootAtPlayer();
                }
                
                lastShotCounter = (lastShotCounter + 1) % 2;
            }
            
            
            timeSinceLastFire = 0.0f;
        }
        
    }


    void ShootAtPlayer()
    {
        Vector3 shootDir =  player.transform.position - transform.position;
        Vector3 shootNoise = new Vector3(
            Random.Range(-shootNoiseMagnitude, shootNoiseMagnitude),
            Random.Range(-shootNoiseMagnitude, shootNoiseMagnitude),
            0.0f);
        
        
        shootDir = shootDir + shootNoise;
        float lookAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion projectileRot = Quaternion.Euler(0, 0, lookAngle + lookDirOffset);
        GameObject shotProjectileGO = Instantiate(projectile, transform.position, projectileRot);
        Projectile shotProjectile = shotProjectileGO.GetComponent<Projectile>();

        shotProjectile.Fire(shootDir);
        
    }

    void BurstShootAtPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 shootDir =  player.transform.position - transform.position;
            shootDir = Quaternion.AngleAxis(burstAngles[i], Vector3.forward) * shootDir;
            float lookAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            
            Quaternion projectileRot = Quaternion.Euler(0, 0, lookAngle + lookDirOffset);
            GameObject shotProjectileGO = Instantiate(projectile, transform.position, projectileRot);
            Projectile shotProjectile = shotProjectileGO.GetComponent<Projectile>();

            shotProjectile.Fire(shootDir);    
        }
    }

    void ShootInAllDirections()
    {
        float angleFactor = 360 / numDirsForAllDirs;
        for(int i = 0; i < 8; i++)
        {
            Vector3 shootDir = transform.position;
            shootDir = Quaternion.AngleAxis(angleFactor * i, Vector3.forward) * shootDir;
            float lookAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            
            Quaternion projectileRot = Quaternion.Euler(0, 0, lookAngle + lookDirOffset);
            GameObject shotProjectileGO = Instantiate(projectile, transform.position, projectileRot);
            Projectile shotProjectile = shotProjectileGO.GetComponent<Projectile>();

            shotProjectile.Fire(shootDir);
        }
    }
}
