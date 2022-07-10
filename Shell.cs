using UnityEngine;

public class Shell : WarEntity
{
    private Vector3 _launchPoint;
    private Vector3 _targetPoint;
    private Vector3 _launchVeclocity;

    private float _blastRadius;
    private float _damage;

    private float _age;

    public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVeclocity, float blastRadius, float damage)
    {
        _launchPoint = launchPoint;
        _targetPoint = targetPoint;
        _launchVeclocity = launchVeclocity;
        _blastRadius = blastRadius;
        _damage = damage;
    }

    public override bool GameUpdate()
    {
        _age += Time.deltaTime;
        Vector3 p = _launchPoint + _launchVeclocity * _age;
        p.y -= 0.5f * 9.81f * _age * _age;

        if (p.y < 0)
        {
            Game.SpawnExplosion().Initialize(_targetPoint, _blastRadius, _damage);
            OriginFactory.Reclaim(this);
            return false;
        }
        transform.localPosition = p;

        Vector3 d = _launchVeclocity;
        d.y -= 9.81f * _age;
        transform.localRotation = Quaternion.LookRotation(d);
        Game.SpawnExplosion().Initialize(p, 0.03f);

        return true;
    }
}
