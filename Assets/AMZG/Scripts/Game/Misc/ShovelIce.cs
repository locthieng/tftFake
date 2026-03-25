/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelIce : MonoBehaviour
{
    [SerializeField] private MainCharacterController character;
    [SerializeField] private IcePlace icePlace;
    [SerializeField] private LayerMask materLayer;
    [SerializeField] private GameObject UIMaxIcePlace;
    RaycastHit raycastHitIce;
    RaycastHit raycastHitShowShovel;
    private bool hitIce;
    private bool hitShowShovel;
    public int numberIceInCharacter;
    public int maxIceInCharacter = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUp()
    {
        icePlace.ShowIcePlace(MainCharacterController.Instance.Data.ResourceAmounts[(int)ResourceMaterialType.Ice]);
        CheckWhenPourIce();
    }    

    public void CheckHitIce()
    {
        hitShowShovel = Physics.SphereCast(character.ray, 1.5f, out raycastHitShowShovel, 1.5f, materLayer);
        hitIce = Physics.SphereCast(character.ray, 0.7f, out raycastHitIce, 1f, materLayer);
        if (hitIce && raycastHitIce.collider.gameObject.CompareTag("Ice"))
        {
            int materialAmount = MainCharacterController.Instance.Data.ResourceAmounts[(int)ResourceMaterialType.Ice];

            if (materialAmount == maxIceInCharacter)
            {
                UIMaxIcePlace.SetActive(true);
                return;
            }
            UIMaxIcePlace.SetActive(false);
            IceShovel();
        }

        if (hitShowShovel && raycastHitShowShovel.collider.gameObject.CompareTag("Ice"))
        {
            character.Shovel();
        }
    }

    public void IceShovel()
    {
        int materialAmount = MainCharacterController.Instance.Data.ResourceAmounts[(int)ResourceMaterialType.Ice];

        iceLake.ShowColliderIce(true);

        iceLake.ShowHPBarIceLake();
        ResourcesController.Instance.UpdateResource(ResourceMaterialType.Ice, 1);
        icePlace.ShowIcePlace(materialAmount);
        iceLake.DeleteIce(raycastHitIce.collider.transform.parent.gameObject);
        raycastHitIce.collider.gameObject.layer = LayerMask.NameToLayer("Default");
        Ice i = raycastHitIce.collider.transform.parent.GetComponent<Ice>();
        i.MoveToIcePlace(raycastHitIce.collider.transform.parent,
            icePlace.listIcePlace[icePlace.CheckIcePlace(materialAmount)].transform, icePlace.transform);
    }

    public void CheckIcePlace(int materialAmount)
    {
        icePlace.ShowIcePlace(materialAmount);
    }

    public void CheckWhenPourIce()
    {
        int materialAmount = MainCharacterController.Instance.Data.ResourceAmounts[(int)ResourceMaterialType.Ice];

        if (materialAmount == maxIceInCharacter)
        {
            UIMaxIcePlace.SetActive(true);
            iceLake.ShowColliderIce(false);
            return;
        }
        UIMaxIcePlace.SetActive(false);
        iceLake.ShowColliderIce(true);
    }

    public Transform TransformIcePlace()
    {
        return icePlace.transform;
    }    

    internal void Save()
    {
        *//*if (character.Data == null) data = new PlayerData();
        data.IceCarring == ;
        PlayerDataManager.Save(data);*//*
    }
}
*/