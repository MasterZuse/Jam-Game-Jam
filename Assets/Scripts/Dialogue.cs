using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {
    public TextMeshProUGUI textComponent;
    [SerializeField] string[] lines; 
    [SerializeField] float textSpeed;
    private int index;
    void Start(){
        textComponent.text = string.Empty;
    }
    void Update(){
        if(Input.GetButtonDown("Jump")){
            if(textComponent.text == lines[index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine(){
        if(index < lines.Length-1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            Debug.Log(index);
        }
        else{
            textComponent.text = string.Empty;
            GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
            playerObj.GetComponent<PlayerController>().ToggleSignInteract();
            Debug.Log(index);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartDialogue();
            collision.GetComponent<PlayerController>().ToggleSignInteract();
        }
    }
}