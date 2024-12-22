using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Cria uma nova opção do menu. fileName (nome padrão do arquivo que será criado). menuName (caminho para acessar a opção a partir do menu).
[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
// Cria a classe DialogSettings que herda a classe ScriptableObject
public class DialogSettings : ScriptableObject
{
    // Cria uma divisão Settings, contendo a propriedade actor, que recebe um Objeto da cena. Nesse caso, um NPC.
    [Header("Settings")]
    public GameObject actor;

    // Cria uma divisão Dialog, contendo a propriedade speakerSprite, que irá receber um sprite (foto) do NPC. sentence irá receber uma frase para o NPC falar.
    [Header("Dialog")]
    public Sprite speakerSprite;
    public string sentence;

    // Cria uma lista de sentences, que será a lista de falas disponíveis para o diálogo
    public List<Sentences> dialogues = new List<Sentences>();
}

// É necessário serializar
[System.Serializable]
public class Sentences
{
    // A classe Sentences mantém as informações das falas.
    // actorName: Nome do personagem que irá falar
    // profile: Sprite (foto) do personagem que está falando
    // sentence: Variável do tipo Language que guarda a fala em diversas linguas
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[System.Serializable]
public class Languages
{
    // A classse Languages guarda o texto das falas em várias linguas diferentes
    public string portuguese;
    public string english;
    public string spanish;
}

// Cria um botão para a inserção de novas falas diretamente de dentro da Unity
#if UNITY_EDITOR
[CustomEditor(typeof(DialogSettings))]
// O BuilderEditor herda a classe Editor da Unity
public class BuilderEditor : Editor
{
    // Realiza a modificação do Inspector (barra de propriedades da Unity)
    public override void OnInspectorGUI()
    {
        // Cria um Inspector vazio
        DrawDefaultInspector();

        // Criação de um objeto do tipo DialogSettings para que ele apareça no Inspector
        // Isso irá gerar as propriedades no Inspector para que possamos editá-las
        DialogSettings ds = (DialogSettings)target;

        // Cria um objeto do tipo Languages, que irá receber as falas
        Languages l = new Languages();
        // Pega o texto escrito no campo 'sentence' do Inspector e passa para a variável Languages
        l.portuguese = ds.sentence;

        // Cria um objeto Sentences
        Sentences s = new Sentences();

        // Pega o Sprite do campo 'speakerSprite' do Inspector e passa para a variável Sentences
        s.profile = ds.speakerSprite;
        // Passa a variável Languages (que recebeu o texto da fala em portugues anteriormente) e aloca na sentença
        s.sentence = l;

        // Cria visualmente um botão de criar diálogo, que irá adicionar a sentença na lista de sentenças do DialogSettings
        if(GUILayout.Button("Create Dialog"))
        {
            // Verifica se tem algum texto digitado. Somente adiciona se tiver algo escrito.
            if(ds.sentence != "")
            {
                // Adiciona a sentença na lista de dialogos
                ds.dialogues.Add(s);

                // Reseta o valor do speakerSprite e do texto da fala
                ds.speakerSprite = null;
                ds.sentence = "";
            }
        }
    }
}

#endif