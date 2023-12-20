using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected RectTransform lifeBarTransform;

    public EarthTreeType enemyType;
    public float enemySpeed;
    public float enemyAceleration;
    public Vector3 destination;

    public virtual void Awake()
    {
        navMeshAgent.speed = enemySpeed;
        navMeshAgent.acceleration = enemyAceleration;
        destination = Vector3.zero;
    }
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void  Update()
    {
        lifeBarTransform.LookAt(Camera.main.transform.position);
    }

    public virtual void OnEnable()
    {
        Move();
    }

    public virtual void Move()
    {
    }
}
