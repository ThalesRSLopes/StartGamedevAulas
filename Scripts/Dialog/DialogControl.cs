using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        en,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogObj; // Objeto da janela do diálogo
    public Image profileSprite; // Sprite (imagem) de quem está falando
    public Text speechText; // Texto da fala
    public Text actorNameText; // Nome de quem fala

    [Header("Settings")]
    public float typingSpeed; // Velocidade da fala

    // Variáveis de controle
    public bool isShowing; // Se a janela está visível
    private int index; // Index das palavras
    private string[] sentences;

    public static DialogControl instance;

    // Awake é chamado antes de todos os Start() na hierarquia de execução de todos os scripts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Um enumerator é um método que utiliza um controle de tempo para executar
    IEnumerator TypeSentence()
    {
        // Para cada letra na sentença
        foreach(char letter in sentences[index].ToCharArray())
        {
            // Adiciona a letra no Speach Text, que armazena o texto que está sendo exibido em tela
            speechText.text += letter;
            // É necessário utilizar esse controle de tempo para a função do tipo IEnumerator
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Pula para a próxima fala
    public void NextSentence()
    {
        // Verifica se o texto que está sendo escrito em tela está completo
        if(speechText.text == sentences[index]){
            if(index < sentences.Length - 1){ // enquanto houverem falas para o NPC dizer
                index++; // incrementa o index para falar a próxima fala
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // quando termina todas as falas
            {
                speechText.text = ""; // Reseta o texto que está sendo falado
                index = 0; // Reseta o index
                dialogObj.SetActive(false); // Para de exibir a janela de diálogo
                sentences = null; // Reseta as sentenças
                isShowing = false; // Reseta a variável que indica se alguém está falando
            }
        }
    }

    // Chamar a fala do NPC
    public void Speech(string[] txt)
    {
        // Verifica se a tela NÃO está aparecendo. Somente executa esse código se a tela não estiver aparecendo, para evitar de sobrepor outra fala.
        if (!isShowing)
        {
            dialogObj.SetActive(true); // Mostra o campo de diálogo em tela
            sentences = txt; // Obtém as falas a serem ditas pelo NPC
            StartCoroutine(TypeSentence()); // Inicia o código de escrever o texto
            isShowing = true; // Indica que o texto está sendo falado
        }
    }
}
