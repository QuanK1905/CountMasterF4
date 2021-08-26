using System.Collections;
using UnityEngine;
using JetSystems;

public class SquadDetection : MonoBehaviour
{
    [Header(" Managers ")]
    [SerializeField] private RunnerFormation runnerFormation;

    [SerializeField] private Transform camera;
    [SerializeField] private Transform runnersParent;

    [Header(" Settings ")]
    [SerializeField] private LayerMask doorLayer3;
    [SerializeField] private LayerMask doorLayer2;
    [SerializeField] private LayerMask finishLayer;
    [SerializeField] private LayerMask cannonLayer;
    [SerializeField] private LayerMask bossLayer;
   
    [SerializeField] private LayerMask ladderLayer;
    [SerializeField] private LayerMask ladder2Layer;


    private Runner runner;
    public bool checkBoss = false; 
    public bool checkCannon = false;
    bool fightBoss = false;
    void Update()
    {
        
        if (UIManager.IsGame()) 
        {
            if (!checkBoss)
            
                DetectionBoss();

            if (!checkCannon) DetectCannon();
            
            DetectDoors3();
            DetectDoors2();
            DetectFinishLine();


            
            DetectionLadder2();

           }
        if (fightBoss)
            FrezePos();


    }
    int i;
    
    public void FrezePos()
    {
        
           
       
            Vector3 pos = transform.position;
            pos.x = 0;
            transform.position = pos;
        
    }
    private void DetectCannon()
    {
        if (Physics.OverlapSphere(transform.position, runnerFormation.GetSquadRadiusActive(), cannonLayer).Length > 0)
        {
            FindObjectOfType<CameraFollow>().ChangeViewCannon();
            FindObjectOfType<SquadController>().run = false;
            FindObjectOfType<RunnerFormation>().resetPosition = false;
            FrezePos();
            runnerFormation.HideText();
            for(int i = 0;i< runnersParent.childCount;i++)

                runnersParent.GetChild(i).GetComponent<Runner>().DetectCannon();
           
        }
       else FindObjectOfType<SquadController>().run = true;
    }

    private void DetectionBoss()
    {

        Vector3 distance = transform.position  ;
        Collider[] detectBoss = Physics.OverlapSphere(distance, runnerFormation.GetSquadRadius()+6f, bossLayer);
           if (detectBoss.Length <= 0) return;

        Collider colliderBoss = detectBoss[0];
        Boss boss = colliderBoss.GetComponent<Boss>();
 
        checkBoss = true;
        FindObjectOfType<SquadController>().RunStatus();
     
        for ( i = 0; i < runnersParent.childCount; i++)
        {
            runner = runnersParent.GetChild(i).GetComponent<Runner>();      
            runner.IsFighting();
            runner.transform.LookAt(boss.transform);
            runner.home = boss.transform;
            runner.agent.speed = 1;
        }
        
        runnerFormation.HideText();
        

    }

   
    private void DetectDoors3()
    {
        Collider[] detectedDoors = Physics.OverlapSphere(transform.position, runnerFormation.GetSquadRadius(), doorLayer3);

        if (detectedDoors.Length <= 0) return;

        Collider collidedDoorCollider = detectedDoors[0];
        Door3 collidedDoor = collidedDoorCollider.GetComponentInParent<Door3>();

        int runnersAmountToAdd = collidedDoor.GetRunnersAmountToAdd( runnersParent.childCount);


        if (runnersAmountToAdd > 0) runnerFormation.AddRunners(runnersAmountToAdd);
        else runnerFormation.DelRunners(-runnersAmountToAdd);
        
    }
    private void DetectDoors2()
    {
        Collider[] detectedDoors = Physics.OverlapSphere(transform.position, runnerFormation.GetSquadRadius(), doorLayer2);

        if (detectedDoors.Length <= 0) return;

        Collider collidedDoorCollider = detectedDoors[0];
        Door collidedDoor = collidedDoorCollider.GetComponentInParent<Door>();

        int runnersAmountToAdd = collidedDoor.GetRunnersAmountToAdd(collidedDoorCollider, runnersParent.childCount);
        

        if (runnersAmountToAdd > 0) runnerFormation.AddRunners(runnersAmountToAdd);
        else runnerFormation.DelRunners(-runnersAmountToAdd);

    }
    private void DetectFinishLine()
    {
        if (Physics.OverlapSphere(transform.position, 1, finishLayer).Length > 0)
        {
            FindObjectOfType<FinishLine>().PlayConfettiParticles();
            FindObjectOfType<CameraFollow>().ChangeViewE();
          
            for (int i = 0; i < runnersParent.childCount; i++)
            {
                
                runner = runnersParent.GetChild(i).GetComponent<Runner>();
                runner.IsDancing();
                runner.home = transform;
                runner.transform.LookAt(camera);
            }
            SetLevelComplete();
           
        }

    }

  

    private void DetectionLadder2()
    {
        Collider[] detectedLadder = Physics.OverlapSphere(transform.position, runnerFormation.GetSquadRadius(), ladder2Layer);
        if (detectedLadder.Length <= 0) return;
        checkCannon = true;
        fightBoss = true;
        FindObjectOfType<CameraFollow>().ChangeView();
    }



    public void SetLevelComplete()
    {
        UIManager.setLevelCompleteDelegate?.Invoke(3);
       
    }


    


}
