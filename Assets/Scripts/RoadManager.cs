using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject road;
    private float speed = 2f;
    private float preSpeed;
    private PlayerHitDetector hitDetector;

    private float iS = 0.8f;
    private float iS2;
    // Start is called before the first frame update
    void Start()
    {
        hitDetector = GameObject.Find("Player").GetComponent<PlayerHitDetector>();
        preSpeed = speed;
        iS2 = iS;
        var roads = Instantiate(road);
        //roads.transform.position = new Vector3(0, 10f, 0);
        road.transform.position = transform.position;
        LeanTween.moveY(roads, -20f, 3f);
        Observable.Interval(TimeSpan.FromSeconds(iS))
            .Subscribe(_ =>
            {
                var roadss = Instantiate(road);
                //roads.transform.position = new Vector3(0, 10f, 0);
                road.transform.position = transform.position;
                if (hitDetector.IsStar) {
                    iS = 0.1f;
                    speed = 1.5f;
                }
                    
                if (hitDetector.IsStar == false) {
                    speed = preSpeed;
                    iS = iS2;
                }
                    
                LeanTween.moveY(roadss, -20f, speed);
                Destroy(roadss, 6f);
            }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
