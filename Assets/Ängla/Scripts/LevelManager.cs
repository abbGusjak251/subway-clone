using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { set; get;}

    private const bool Show_Collider = true;

    // Level spawning

    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 10;
    private const int MAX_SEGMENTS_ON_SCREEN = 15;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1, y2, y3;



    //List of pieces 
      public List<Piece> ramps = new List<Piece>();
      public List<Piece> pieces = new List<Piece>(); // All the pieces

      public Piece GetPiece(PieceType pt, int visualIndex);


    //List of segments
    //public List<Segment> availableSegments = new List<Segment>();
    //public List<Segment> availableTransitions = new List<Segment>();
    //public List<Segment> segments = new List<Segment>();

    // Gameplay
    //private bool isMoving=false;

    private void Awake()
    {
        Instance = this;
        CameraContainer = Camera.current.transform;    //current camera
        Current_SpawnZ = 0;
        Current_Level = 0;
    }

    private void Start()
    {
        for (int i = 0; i < Initial_Segments; i++)
            GenerateSegment();
    }

    private void GenerateSegment()
    {
        SpawnSegment();

        if (Random.Range(0f, 1f) < (Continious_Segments * 0.25f))
            
        {
            Continious_Segments++;
            //Spawn transition Segment
            Continious_Segments = 0;
            SpawnTransition();
        }
        else
        {
            Continious_Segments++;
        }
    }

    private void SpawnSegment()
    {
        List<Segment> possibleSegment = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSegment.Count);

        Segment s  = GetSegment(id,false);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * Current_SpawnZ;

        Current_SpawnZ += s.lenght;
        Amount_Of_Active_Segments++;
        s.Spawn();
    }
    private void SpawnTransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * Current_SpawnZ;

        Current_SpawnZ += s.lenght;
        Amount_Of_Active_Segments++;
        s.Spawn();
    }

    public Segment GetSegment(int id, bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegmentId == id && x.transition == transition && !x.gameObject.activeSelf);
        // SegmentId might be wrong !
        if (s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            // ternary operator
            s = go.GetComponent<Segment>();

            s.SegmentId = id;
            s.transition = transition;

            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }

        return s;
    }
    //  public Piece GetPiece(PieceType pt, int visualIndex);

}
