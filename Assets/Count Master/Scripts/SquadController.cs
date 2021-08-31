using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class SquadController : MonoBehaviour
{
    [Header(" Managers ")]
    [SerializeField] private RunnerFormation runnerFormation;

    [Header(" Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveCoefficient;
    [SerializeField] private float platformWidth;
    
    private Vector3 clickedPosition;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    public bool run =true;
    float radius,maxX;
    void Update()
    {
        radius = runnerFormation.GetSquadRadiusActive();
        
        if (radius > platformWidth / 2) maxX = 0;
        else maxX = platformWidth / 2 - radius;
        
        
        if (UIManager.IsGame())
            if(run)
            MoveForward();

        if (!UIManager.IsGame()) return;

        UpdateProgressBar();

    }
    public void RunStatus()
    {
        run = !run;
    }

    public void StoreClickedPosition()
    {
        clickedPosition = transform.position;
        
    }
    public bool swipe = false;
    public void GetSlideValue(Vector2 slideInput)
    {
        if (!swipe) return;
       
        slideInput.x *= moveCoefficient;
        float targetX = clickedPosition.x + slideInput.x;

       

        targetX = Mathf.Clamp(targetX, -maxX, maxX);

        transform.position = transform.position.With(x: Mathf.Lerp(transform.position.x, targetX, 0.3f));
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
    

    private void UpdateProgressBar()
    {
        float initialDistanceToFinish = RoadManager.GetFinishPosition().z - initialPosition.z;
        float currentDistanceToFinish = RoadManager.GetFinishPosition().z - transform.position.z;
        float distanceLeftToFinish = initialDistanceToFinish - currentDistanceToFinish;

        float progress = distanceLeftToFinish / initialDistanceToFinish;
        UIManager.updateProgressBarDelegate?.Invoke(progress);
    }
}
