using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    #region Public Variables
    public bool FlaggedForDelete => _flaggedForDelete;
    public bool IsOverlapping => _isOverlapping;
    public BuildingData AssignedData => _assignedData;
    #endregion

    #region Private Variables
    private BuildingData _assignedData;
    private BoxCollider _boxCollider;
    private GameObject _graphic;
    private Transform _colliders; // TODO: bad variable's name
    private bool _isOverlapping;
    private Renderer _renderer;
    private Material _defaultMaterial;
    private bool _flaggedForDelete;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        GameObject.Find("Game_manager").GetComponent<RuntimeBakeNavMesh>().BakeNavMesh();
    }
    #endregion
    #region Public Methods
    public void Init(BuildingData data)
    {
        _assignedData = data;

        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.size = _assignedData.BuildingSize;
        _boxCollider.center = new Vector3(0, (_assignedData.BuildingSize.y + 0.2f) * 0.5f, 0);
        _boxCollider.isTrigger = true;

        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        _graphic = Instantiate(data.Prefab, transform);
        _renderer = _graphic.GetComponentInChildren<Renderer>();
        _defaultMaterial = _renderer.material;

        // TODO: do the "Colliders" in help script 
        _colliders = _graphic.transform.Find("Colliders");
        if (_colliders != null) _colliders.gameObject.SetActive(false);
    }
    public void PlaceBuilding()
    {
        _boxCollider.enabled = false;
        if(_colliders != null) _colliders.gameObject.SetActive(true);
        UpdateMaterial(_defaultMaterial);
        // TODO: layer should be in help script as well
        gameObject.layer = 7;
        gameObject.name = _assignedData.DisplayName + " - " + transform.position;
    }

    public void UpdateMaterial(Material newMaterial)
    {
        if (_renderer.material != newMaterial) _renderer.material = newMaterial;
    }
    public void FlagForDelete(Material deleteMat)
    {
        UpdateMaterial(deleteMat);
        _flaggedForDelete = true;
    }
    public void RemoveDeleteFlag()
    {
        UpdateMaterial(_defaultMaterial);
        _flaggedForDelete = false;
    }
    #endregion

    #region Private Methods
    private void OnTriggerStay(Collider other)
    {
        _isOverlapping = true;
    }
    private void OnTriggerExit(Collider collision)
    {
        _isOverlapping = false;
    }
    #endregion
}
