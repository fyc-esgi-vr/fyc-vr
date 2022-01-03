using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFly : MonoBehaviour
{
    public float realDimensionSize = 3;
    public float littleDimensionSize = 1;

    public Material littleDimensionDefaultMat;
    public Material littleDimensionDangerMat;
    public GameObject flySoundObject;
    //object to track
    public GameObject flyPrefab;
    public GameObject characterModelPrefab;

    private GameObject _fly;
    public Vector3 littleDimensionBotLeftBackPoint;
    public Vector3 realDimensionBotLeftBackPoint;
    
    private bool _flyIsMoving;
    private float _proportion;
    private Vector3 _lastValidFlyPos;
    private MeshRenderer _littleDimensionCubeMeshRenderer;
    private void Awake()
    {

        var centerLittleDimension = new Vector3(littleDimensionBotLeftBackPoint.x + littleDimensionSize / 2,
            littleDimensionBotLeftBackPoint.y + littleDimensionSize / 2,
            littleDimensionBotLeftBackPoint.z + littleDimensionSize / 2);
        
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = centerLittleDimension;
        cube.transform.localScale = new Vector3(littleDimensionSize, littleDimensionSize, littleDimensionSize);
        _littleDimensionCubeMeshRenderer = cube.GetComponent<MeshRenderer>();
        _littleDimensionCubeMeshRenderer.material = littleDimensionDefaultMat;
        Mesh m = characterModelPrefab.GetComponent<MeshFilter>().sharedMesh;
        Bounds meshBounds = m.bounds;
        Vector3 meshSize = meshBounds.size;
        characterModelPrefab.transform.localScale = new Vector3(littleDimensionSize / meshSize.x * 0.3f,littleDimensionSize / meshSize.y * 0.5f,littleDimensionSize / meshSize.z * 0.3f);
        Instantiate(characterModelPrefab, centerLittleDimension, Quaternion.identity);
        
        m = flyPrefab.GetComponent<MeshFilter>().sharedMesh;
        meshBounds = m.bounds;
        meshSize = meshBounds.size;
        flyPrefab.transform.localScale = new Vector3(littleDimensionSize / meshSize.x * 0.1f,littleDimensionSize / meshSize.y* 0.1f,littleDimensionSize / meshSize.z* 0.1f);
        var flyPos = new Vector3(littleDimensionBotLeftBackPoint.x + littleDimensionSize / 4,
            littleDimensionBotLeftBackPoint.y + littleDimensionSize / 2,
            littleDimensionBotLeftBackPoint.z + littleDimensionSize / 1.25f);
        _fly = Instantiate(flyPrefab, flyPos, Quaternion.identity);
        _proportion = realDimensionSize / littleDimensionSize;
        Debug.Log(_proportion);
    }

    //called when the player is grabbing the fly
    //enables and disables the tracking of the fly
    public void SetFlyMoving(bool val)
    {
        _flyIsMoving = val;
    }

    private bool VerifyFlyPosition(Vector3 pos)
    {
        if (pos.x < littleDimensionBotLeftBackPoint.x ||
            pos.x > littleDimensionBotLeftBackPoint.x + littleDimensionSize)
            return false;
        if (pos.y < littleDimensionBotLeftBackPoint.y ||
            pos.y > littleDimensionBotLeftBackPoint.y + littleDimensionSize)
            return false;
        if (pos.z < littleDimensionBotLeftBackPoint.z ||
            pos.z > littleDimensionBotLeftBackPoint.z + littleDimensionSize)
            return false;
        return true;
    }
    // Update is called once per frame
    private void Update()
    {
        //if (!_flyIsMoving) return;
        var flyPos = _fly.transform.position;
        if (VerifyFlyPosition(flyPos))
        {
            _lastValidFlyPos = flyPos;
            _littleDimensionCubeMeshRenderer.material = littleDimensionDefaultMat;
        }
        else
        {
            _fly.transform.position = _lastValidFlyPos;
            _littleDimensionCubeMeshRenderer.material = littleDimensionDangerMat;
            return;
        }
        
        var difference = new Vector3(flyPos.x - littleDimensionBotLeftBackPoint.x,
            flyPos.y - littleDimensionBotLeftBackPoint.y, 
            flyPos.z - littleDimensionBotLeftBackPoint.z);
        flySoundObject.transform.position = new Vector3(realDimensionBotLeftBackPoint.x + difference.x * _proportion,
            realDimensionBotLeftBackPoint.y + difference.y * _proportion,
            realDimensionBotLeftBackPoint.z + difference.z * _proportion);
        flySoundObject.transform.rotation = _fly.transform.rotation;
        
    }
}
