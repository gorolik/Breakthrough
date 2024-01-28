using System.Collections;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private Light _lightSource;
    [SerializeField] private float _lightRenderDistance = 30f;

    private Transform _player;
    private float _updateFrency = 1;


    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        StartCoroutine(LightOptimization());
    }

    private IEnumerator LightOptimization()
    {
        while (true)
        {
            if(_player != null)
            {
                if(_lightSource.enabled == false && Vector3.Distance(_player.position, transform.position) <= _lightRenderDistance)
                    _lightSource.enabled = true;
                else if(_lightSource.enabled == true && Vector3.Distance(_player.position, transform.position) > _lightRenderDistance)
                    _lightSource.enabled = false;
            }

            yield return new WaitForSeconds(_updateFrency);
        }
    }
}
