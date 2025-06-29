using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public string characterName;
    public int num;

    protected void Start() {
        this.LoadCharacter(characterName);
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
        if (num == 1) {
            go.AddComponent<Player1Controller>();
            go.GetComponent<CharacterBase>().Init(1f);
        }
        else if (num == 2){
            go.AddComponent<Player2Controller>();
            go.GetComponent<CharacterBase>().Init(-1f);
        }
        
    }
}
