using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFly : MonoBehaviour
{
    [Tooltip("Size of the dimension of the real player.")]
    public float realDimensionSize = 3;
    [Tooltip("Size of the dimension of the player model.")]
    public float littleDimensionSize = 1;

    [Space]
    [Tooltip("Material to display on the little dimension when the fly is in the dimension.\nThe color should be transparent")]
    public Material littleDimensionDefaultMat;
    [Tooltip("Material to display on the little dimension when the fly is no longer in the dimension.\nThe color should be transparent")]
    public Material littleDimensionDangerMat;
    
    [Space]
    [Tooltip("Ojbect that has the audio source to move around the real dimension.")]
    public GameObject flySoundObject;
    [Tooltip("The movable fly.")]
    public GameObject flyPrefab;
    
    [Tooltip("The player model to show.")]
    public GameObject characterModelPrefab;
    
    [Space]
    public Vector3 realDimensionCenter;
    public Vector3 littleDimensionCenterPoint;

    [Space]
    public Vector3 characterScale;
    public Vector3 flyScale;
    
    //Instantiated prefab of the fly.
    private GameObject _fly;
    //proportion of the little dimension based on the real dimension.
    private float _proportion;
    //Teleport fly to last valid position when put out of the little dimension.
    private Vector3 _lastValidFlyPos;
    //mesh renderer of the little dimension to change the material.
    private MeshRenderer _littleDimensionCubeMeshRenderer;
    private void Awake()
    {
        CreateDimensionBox();
        
        InstantiateModelInDimension(characterModelPrefab, characterScale);
        _fly = InstantiateModelInDimension(flyPrefab, flyScale);
        
        _fly.transform.position = new Vector3(littleDimensionCenterPoint.x + littleDimensionSize / 4,
            littleDimensionCenterPoint.y + littleDimensionSize / 4,
            littleDimensionCenterPoint.z + littleDimensionSize / 4);
        
        _proportion = realDimensionSize / littleDimensionSize;
    }

    //Create prefab in the center of the little dimension with a scaled based on the little dimension.
    private GameObject InstantiateModelInDimension(GameObject prefab, Vector3 modelScaleInDimension)
    {
        Mesh m = prefab.GetComponent<MeshFilter>().sharedMesh;
        Bounds meshBounds = m.bounds;
        Vector3 meshSize = meshBounds.size;
        float x = littleDimensionSize / meshSize.x * modelScaleInDimension.x;
        float y = littleDimensionSize / meshSize.y * modelScaleInDimension.y;
        float z = littleDimensionSize / meshSize.z * modelScaleInDimension.z;
        prefab.transform.localScale = new Vector3(x,y,z);
        return Instantiate(prefab, littleDimensionCenterPoint, Quaternion.identity);
    }
    
    private void CreateDimensionBox()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = littleDimensionCenterPoint;
        cube.transform.localScale = new Vector3(littleDimensionSize, littleDimensionSize, littleDimensionSize);
        _littleDimensionCubeMeshRenderer = cube.GetComponent<MeshRenderer>();
        _littleDimensionCubeMeshRenderer.material = littleDimensionDefaultMat;
    }
    
    //Check if the fly is outside of the little dimension
    private bool VerifyFlyPosition(Vector3 pos)
    {
        if (pos.x < littleDimensionCenterPoint.x - littleDimensionSize / 2||
            pos.x > littleDimensionCenterPoint.x + littleDimensionSize /2)
            return false;
        if (pos.y < littleDimensionCenterPoint.y - littleDimensionSize / 2||
            pos.y > littleDimensionCenterPoint.y + littleDimensionSize / 2)
            return false;
        if (pos.z < littleDimensionCenterPoint.z - littleDimensionSize / 2 ||
            pos.z > littleDimensionCenterPoint.z + littleDimensionSize / 2)
            return false;
        return true;
    }

    private void MoveSoundRealDimension(Vector3 flyPos)
    {
        var difference = new Vector3(flyPos.x - littleDimensionCenterPoint.x,
            flyPos.y - littleDimensionCenterPoint.y, 
            flyPos.z - littleDimensionCenterPoint.z);
        flySoundObject.transform.position = new Vector3(realDimensionCenter.x + difference.x * _proportion,
            realDimensionCenter.y + difference.y * _proportion,
            realDimensionCenter.z + difference.z * _proportion);
    }
    // Update is called once per frame
    private void Update()
    {
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
        
        MoveSoundRealDimension(flyPos);
        
    }
}
