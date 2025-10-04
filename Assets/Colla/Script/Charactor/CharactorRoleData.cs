using UnityEngine;

[CreateAssetMenu(fileName = "CharactorRoleData", menuName = "Scriptable Objects/CharactorRoleData")]
public class CharactorRoleData : ScriptableObject
{
    [SerializeField] private CharactorRoleName roleName;
    [SerializeField] private EffectContext context;
    public CharactorRoleName RoleName => this.roleName;
    public EffectContext Context => this.context;

    private void OnValidate() => ClampStickers();
    private void OnEnable() => ClampStickers();

    private void ClampStickers()
    {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
