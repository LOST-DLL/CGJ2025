using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public string characterName;
    public int playerId;

    protected void Start() {
        this.LoadCharacter(characterName);
        HealthSystem.Instance.ResetLevelEvent += ResetCharacter;
    }
    void Update()
    {
        
    }

    void LoadCharacter(string name) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Character/" + name);

        if (prefab != null) {
            var go = Instantiate(prefab, transform.position, Quaternion.identity);
            BindController(go);
        }
        else {
            Debug.LogError("预制体不存在: " + name);
        }
    }

    void BindController(GameObject go) {
        if (playerId == 1) {
            go.AddComponent<Player1Controller>();
            go.GetComponent<CharacterBase>().Init(playerId);
        }
        else if (playerId == 2){
            go.AddComponent<Player2Controller>();
            go.GetComponent<CharacterBase>().Init(playerId);
        }
        
    }

    void ResetCharacter() {
        this.LoadCharacter(characterName);
    }
}
