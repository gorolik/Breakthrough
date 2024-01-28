using UnityEngine;

public class EnemyDieAnimation : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject[] _meshParts;
    [SerializeField] private Vector3 _partsForce;
    [SerializeField] private AudioSource _destroySoundSource;
    [SerializeField] private AudioClip _destroySound;
    [SerializeField] private float _destroyPartsDelay = 1;


    private void OnEnable()
    {
        _enemy.ProceedDie += OnDie;
    }

    private void OnDisable()
    {
        _enemy.ProceedDie -= OnDie;
    }

    private void OnDie()
    {
        gameObject.transform.parent = null;
        _destroySoundSource.transform.parent = transform;

        _destroySoundSource.PlayOneShot(_destroySound);

        foreach (var part in _meshParts) 
        {
            Rigidbody rigidbody = part.GetComponent<Rigidbody>();
            Collider collider = part.GetComponent<Collider>();

            rigidbody.isKinematic = false;
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            collider.enabled = true;

            rigidbody.AddForce(new Vector3(Random.Range(-_partsForce.x, _partsForce.x), Random.Range(-_partsForce.y, _partsForce.y), Random.Range(-_partsForce.z, _partsForce.z)));
            Destroy(part, _destroyPartsDelay);
        }

        Destroy(_enemy.gameObject);
    }
}
