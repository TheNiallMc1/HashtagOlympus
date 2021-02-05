using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Project.Scripts.AI.AiControllers
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIMovement : MonoBehaviour
    {
        public Animator animator;
        public NavMeshAgent nav;
        private AIBrain _aiBrain;

        public List<Waypoint> wayPoints;
        public List<Waypoint> path;

        public GameObject wP;

        public Waypoint spawn;
        public Waypoint closestWayPoint;

        public Vector3 currentPosition;
        protected Transform destination;
        protected float rDist = 2f;

        protected int wpNum;
        [SerializeField]
        protected int wpIndex;
        public int test;
        private int _currentPath = 1;
        public static readonly int VerticalF = Animator.StringToHash("Vertical_f");
        private bool _isCurrentAttackTargetNull;


        protected void Start()
        {
        
            _aiBrain = GetComponent<AIBrain>();
            animator = GetComponentInChildren<Animator>();
            nav = GetComponent<NavMeshAgent>();
            //wayPoints = wP.GetComponent<Waypoint>().wayPoints;

            if (gameObject.name == "Tourist_Defender") return;
            spawn = wayPoints[0];
            _isCurrentAttackTargetNull = _aiBrain.currentAttackTarget == null;
            wpNum = 0;

            if(gameObject.name != "Tourist_Defender")
            {
                FindNextWayPoint(spawn);
            }
        }

        protected void Update()
        {
            _isCurrentAttackTargetNull = !_aiBrain.isTargetNotNull;
            if (!_aiBrain._isDrunk)
            {
                var animSpeed = nav.velocity.magnitude / nav.speed;
                animator.SetFloat(VerticalF, animSpeed);
            }
            else
            {
                animator.SetFloat(VerticalF, nav.speed);
            }
        }

        protected void FixedUpdate()
        {



            if (gameObject.name == "Tourist_Defender") return;
            if(_isCurrentAttackTargetNull && !_aiBrain._isDrunk)
            {
                transform.LookAt(destination.position);
            }

        }

        public void MoveToWayPoint()
        {
            destination = wayPoints[wpIndex].transform;
            nav.SetDestination(wayPoints[wpIndex].Pos);
            _aiBrain.initMove = false;

        }



        public virtual void Moving()
        {
            if (!nav.pathPending && nav.remainingDistance < rDist && wpIndex != 12)
            {


                var num = Random.Range(0, 10);
                if (num > 7)
                {
                    _aiBrain.weightCheck = true;
                    FindNextWayPoint(wayPoints[wpIndex]);
                }
                else
                {
                    _aiBrain.weightCheck = false;
                    MoveToWayPoint();
                }
            }
            else
            {
                _aiBrain.weightCheck = false;
                MoveToWayPoint();

            }

        }

        public Waypoint GetPath()
        {
            return wayPoints[wpIndex];
        
        }

        /*
    Once a wayPoint has been found, Add that wayPoint to a list,
    that list is a track of visited wayPoints.
    Then add it to another list which is the way point path to follow.
    */
        protected Waypoint FindNextWayPoint(Waypoint obj)
        {
            var length = obj.neighbors.Count;     // list of a wayPoints neighboring wayPoints.

            var key = Random.Range(0, length);

            //  add a randomly selected wayPoint to the path list from the wayPoints neighbor list.
            if (key >= obj.neighbors.Count) return null;
            var next = obj.neighbors[key];
            wpIndex = next.index;
            closestWayPoint = next;

            if (test != 0) return null;
            test++;
            MoveToWayPoint();
            return null;

        }

        public void MoveToTarget(Combatant target)
        {
            nav.SetDestination(target.transform.position);
        }

        public IEnumerator Drunk()
        {
            _aiBrain._drunkCoroutineRunning = true;

            Vector3 drunkDestination;

            _currentPath = Random.Range(1, 5);

            float waitTime = Random.Range(1, 5);
            yield return new WaitForSeconds(waitTime);

            switch (_currentPath)
            {
                
                case 1:
                    drunkDestination = new Vector3(currentPosition.x + 2, currentPosition.y, currentPosition.z);
                    transform.LookAt(drunkDestination);
                    nav.SetDestination(drunkDestination);
                    if (!nav.pathPending && nav.remainingDistance < 0.05)
                        _currentPath = Random.Range(1, 5);
                    break;
                case 2:
                    drunkDestination = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 2);
                    transform.LookAt(drunkDestination);
                    nav.SetDestination(drunkDestination);
                    if (!nav.pathPending && nav.remainingDistance < 0.05)
                        _currentPath = Random.Range(1, 5);
                    break;
                case 3:
                    drunkDestination = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 2);
                    transform.LookAt(drunkDestination);
                    nav.SetDestination(drunkDestination);
                    if (!nav.pathPending && nav.remainingDistance < 0.05)
                        _currentPath = Random.Range(1, 5);
                    break;
                case 4:
                    drunkDestination = new Vector3(currentPosition.x - 2, currentPosition.y, currentPosition.z);
                    transform.LookAt(drunkDestination);
                    nav.SetDestination(drunkDestination);
                    if (!nav.pathPending && nav.remainingDistance < 0.05)
                        _currentPath = Random.Range(1, 5);
                    break;
                case 5:
                    drunkDestination = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 2);
                    transform.LookAt(drunkDestination);
                    nav.SetDestination(drunkDestination);
                    if (!nav.pathPending && nav.remainingDistance < 0.05)
                        _currentPath = Random.Range(1, 5);
                    break;
            }

            _aiBrain._drunkCoroutineRunning = false;
        }
    }

    
}
