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
    public Vector3 realDimensionBotLeftBackPoint;
    public Vector3 littleDimensionBotLeftBackPoint;

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
        Vector3 centerLittleDimension = new Vector3(littleDimensionBotLeftBackPoint.x + littleDimensionSize / 2,
            littleDimensionBotLeftBackPoint.y + littleDimensionSize / 2,
            littleDimensionBotLeftBackPoint.z + littleDimensionSize / 2);
        
        CreateDimensionBox(centerLittleDimension);
        
        InstantiateModelInDimension(characterModelPrefab, centerLittleDimension, characterScale);
        _fly = InstantiateModelInDimension(flyPrefab, centerLittleDimension, flyScale);
        
        _fly.transform.position = new Vector3(littleDimensionBotLeftBackPoint.x + littleDimensionSize / 4,
            littleDimensionBotLeftBackPoint.y + littleDimensionSize / 2,
            littleDimensionBotLeftBackPoint.z + littleDimensionSize / 1.25f);
        
        _proportion = realDimensionSize / littleDimensionSize;
    }

    //Create prefab in the center of the little dimension with a scaled based on the little dimension.
    private GameObject InstantiateModelInDimension(GameObject prefab, Vector3 dimensionCenter, Vector3 modelScaleInDimension)
    {
        Mesh m = prefab.GetComponent<MeshFilter>().sharedMesh;
        Bounds meshBounds = m.bounds;
        Vector3 meshSize = meshBounds.size;
        float x = littleDimensionSize / meshSize.x * modelScaleInDimension.x;
        float y = littleDimensionSize / meshSize.y * modelScaleInDimension.y;
        float z = littleDimensionSize / meshSize.z * modelScaleInDimension.z;
        prefab.transform.localScale = new Vector3(x,y,z);
        return Instantiate(prefab, dimensionCenter, Quaternion.identity);
    }
    
    private void CreateDimensionBox(Vector3 dimensionCenter)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = dimensionCenter;
        cube.transform.localScale = new Vector3(littleDimensionSize, littleDimensionSize, littleDimensionSize);
        _littleDimensionCubeMeshRenderer = cube.GetComponent<MeshRenderer>();
        _littleDimensionCubeMeshRenderer.material = littleDimensionDefaultMat;
    }
    
    //Check if the fly is outside of the little dimension
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

    private void MoveSoundRealDimension(Vector3 flyPos)
    {
        var difference = new Vector3(flyPos.x - littleDimensionBotLeftBackPoint.x,
            flyPos.y - littleDimensionBotLeftBackPoint.y, 
            flyPos.z - littleDimensionBotLeftBackPoint.z);
        flySoundObject.transform.position = new Vector3(realDimensionBotLeftBackPoint.x + difference.x * _proportion,
            realDimensionBotLeftBackPoint.y + difference.y * _proportion,
            realDimensionBotLeftBackPoint.z + difference.z * _proportion);
        flySoundObject.transform.rotation = _fly.transform.rotation;
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
