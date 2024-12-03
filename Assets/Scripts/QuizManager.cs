using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour
{
    public List<Question> allQuestions = new List<Question>();
    public List<Question> selectedQuestions = new List<Question>();
    public GameObject quizPanel;
    public GameObject startQuizButton;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI resultText;

    public float timeLimit = 10f;
    public TextMeshProUGUI timerText;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private string playerName;
    private HashSet<int> answeredCorrectly = new HashSet<int>();

    private float timer;
    private Coroutine timerCoroutine;

    // Panel al final del cuestionario
    public GameObject endQuizPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI congratulationsText;
    public Button nextLevelButton;

    private void Start()
    {
        if (GameData.Instance == null)
        {
            Debug.LogError("GameData instance no est� inicializado.");
            return;
        }

        playerName = GameData.Instance.playerName;

        quizPanel.SetActive(false);
        endQuizPanel.SetActive(false);

        allQuestions = new List<Question>
        {
            new Question { questionText = "�Qui�nes fueron las Hero�nas de la Coronilla?", answers = new string[] { "Un grupo de mujeres revolucionarias", "Un ej�rcito espa�ol", "Un grupo de mercaderes", "Soldados de la independencia" }, correctAnswerIndex = 0 },
            new Question { questionText = "�En qu� a�o ocurri� la batalla de la Coronilla?", answers = new string[] { "1825", "1812", "1809", "1821" }, correctAnswerIndex = 1 },
            new Question { questionText = "�D�nde ocurri� la batalla de la Coronilla?", answers = new string[] { "En Sucre", "En La Paz", "En Cochabamba", "En Potos�" }, correctAnswerIndex = 2 },
            new Question { questionText = "�Qu� fue la Coronilla en Cochabamba?", answers = new string[] { "Una colina utilizada para la defensa", "Un fuerte militar espa�ol", "Una iglesia", "Una escuela" }, correctAnswerIndex = 0 },
            new Question { questionText = "�Qui�n fue la l�der de las Hero�nas de la Coronilla?", answers = new string[] { "Manuela Gandarillas", "Simona Manzaneda", "Juana Azurduy", "Bartolina Sisa" }, correctAnswerIndex = 0 },
            new Question { questionText = "�Qu� motiv� a las mujeres a defender la Coronilla?", answers = new string[] { "La falta de apoyo militar", "La ocupaci�n de sus tierras por los espa�oles", "La injusticia y opresi�n espa�olas", "La promesa de recibir tierras" }, correctAnswerIndex = 2 },
            new Question { questionText = "�C�mo estaban armadas las Hero�nas de la Coronilla?", answers = new string[] { "Con armas de fuego modernas", "Con piedras, palos y herramientas de trabajo", "Con lanzas y espadas", "Con ca�ones y armas de p�lvora" }, correctAnswerIndex = 1 },
            new Question { questionText = "�Qu� sucedi� despu�s de la batalla de la Coronilla?", answers = new string[] { "Las hero�nas ganaron y expulsaron a los espa�oles", "Los espa�oles vencieron y ocuparon Cochabamba", "Las mujeres fueron premiadas por el virrey", "Las hero�nas firmaron un tratado con los espa�oles" }, correctAnswerIndex = 1 },
            new Question { questionText = "�En qu� fecha se conmemora el D�a de la Madre en Bolivia, en honor a las Hero�nas de la Coronilla?", answers = new string[] { "10 de mayo", "14 de abril", "27 de mayo", "25 de junio" }, correctAnswerIndex = 2 },
            new Question { questionText = "�Qu� hizo especial a la batalla de la Coronilla?", answers = new string[] { "Fue la primera batalla de la independencia", "Fue protagonizada casi exclusivamente por mujeres", "Fue la �ltima batalla contra los espa�oles", "Fue una batalla en la que participaron militares" }, correctAnswerIndex = 1 },
            new Question { questionText = "�C�mo reaccion� el pueblo de Cochabamba ante la derrota en la Coronilla?", answers = new string[] { "Abandonaron la ciudad", "Se rindieron ante los espa�oles", "Continuaron resistiendo en otras formas", "Firmaron un tratado de paz" }, correctAnswerIndex = 2 },
            new Question { questionText = "�Qu� edad ten�a aproximadamente Manuela Gandarillas durante la batalla?", answers = new string[] { "30 a�os", "50 a�os", "M�s de 60 a�os", "20 a�os" }, correctAnswerIndex = 2 },
            new Question { questionText = "�C�mo eran conocidas las defensoras de Cochabamba?", answers = new string[] { "Guerreras del Altiplano", "Soldaderas del Sur", "Hero�nas de la Coronilla", "Amazonas de los Andes" }, correctAnswerIndex = 2 },
            new Question { questionText = "�Qu� caracter�sticas definieron a las Hero�nas de la Coronilla?", answers = new string[] { "Su entrenamiento militar", "Su valent�a y sacrificio", "Su riqueza y poder", "Su alianza con otros ej�rcitos" }, correctAnswerIndex = 1 },
            new Question { questionText = "�Qu� hicieron los espa�oles despu�s de vencer en la Coronilla?", answers = new string[] { "Permitieron a las mujeres retirarse", "Quemaron partes de Cochabamba", "Invitaron a los ciudadanos a unirse a su ej�rcito", "Prohibieron la agricultura en la zona" }, correctAnswerIndex = 1 },
            new Question { questionText = "�C�mo fueron vistas las Hero�nas de la Coronilla por la poblaci�n boliviana posteriormente?", answers = new string[] { "Como traidoras", "Como m�rtires y s�mbolos de libertad", "Como invasoras", "Como aliadas del ej�rcito espa�ol" }, correctAnswerIndex = 1 },
            new Question { questionText = "�Qu� tipo de lucha llevaron a cabo las Hero�nas de la Coronilla?", answers = new string[] { "Una batalla naval", "Una batalla a�rea", "Una resistencia armada terrestre", "Una protesta pac�fica" }, correctAnswerIndex = 2 },
            new Question { questionText = "�Qu� ense�anzas dej� el sacrificio de las Hero�nas de la Coronilla?", answers = new string[] { "La importancia de la paz con los invasores", "La capacidad de resistencia y valent�a ante la opresi�n", "La ventaja de tener armas modernas", "La necesidad de alianzas internacionales" }, correctAnswerIndex = 1 },
            new Question { questionText = "�Qui�n lideraba el ej�rcito espa�ol en la batalla de la Coronilla?", answers = new string[] { "Pedro Antonio Ola�eta", "Sebasti�n de Segurola", "Goyeneche", "Jos� de San Mart�n" }, correctAnswerIndex = 0 }

        };
    }

    public void StartQuiz()
    {
        score = 0;
        currentQuestionIndex = 0;
        SelectRandomQuestions();
        timer = timeLimit;
        startQuizButton.SetActive(false);
        quizPanel.SetActive(true);
        ShowQuestion();
    }

    private void SelectRandomQuestions()
    {
        List<Question> unaskedQuestions = allQuestions.Where((q, index) => !answeredCorrectly.Contains(index)).ToList();
        selectedQuestions = unaskedQuestions.OrderBy(q => Random.value).Take(5).ToList();
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex < selectedQuestions.Count)
        {
            timer = timeLimit;
            Question currentQuestion = selectedQuestions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].onClick.RemoveAllListeners();
                    int index = i;
                    answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            timerCoroutine = StartCoroutine(TimerCoroutine());
        }
        else
        {
            Debug.LogError("No hay preguntas para mostrar en la lista seleccionada.");
        }
    }

    private IEnumerator TimerCoroutine()
    {
        float timer = timeLimit;

        while (timer > 0)
        {
            timerText.text = "Tiempo: " + Mathf.Ceil(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }

        resultText.text = "Tiempo agotado.";
        resultText.color = Color.yellow;
        StartCoroutine(HideResultTextAfterSeconds(3f));

        currentQuestionIndex++;

        if (currentQuestionIndex < selectedQuestions.Count)
        {
            ShowQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    private void OnAnswerSelected(int index)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        Question currentQuestion = selectedQuestions[currentQuestionIndex];

        if (index == currentQuestion.correctAnswerIndex)
        {
            resultText.text = "�Correcto!";
            resultText.color = Color.green;
            score += 5;
            int questionId = allQuestions.IndexOf(currentQuestion);
            answeredCorrectly.Add(questionId);
        }
        else
        {
            resultText.text = "Incorrecto.";
            resultText.color = Color.red;
        }

        StartCoroutine(HideResultTextAfterSeconds(3f));

        currentQuestionIndex++;

        if (currentQuestionIndex < selectedQuestions.Count)
        {
            ShowQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    private IEnumerator HideResultTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        resultText.text = "";
        resultText.color = Color.clear;
    }

    private void EndQuiz()
    {
        quizPanel.SetActive(false);
        SavePlayerScore();

        endQuizPanel.SetActive(true);
        congratulationsText.text = $"�Felicidades, {playerName}! Obtuviste un puntaje de {score}.";
        finalScoreText.text = "�Listo para el siguiente nivel?";

        Debug.Log($"Jugador: {playerName}, Puntaje final: {score}");

        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(LoadNextLevel);
    }

    private void SavePlayerScore()
    {
        string profilesFolder = Path.Combine(Application.persistentDataPath, "Profiles");
        if (!Directory.Exists(profilesFolder))
        {
            Directory.CreateDirectory(profilesFolder);
        }

        string filePath = Path.Combine(profilesFolder, playerName.Replace(" ", "_"));

        ProfileData playerData;

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProfileData));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                playerData = (ProfileData)serializer.Deserialize(stream);
            }
        }
        else
        {
            playerData = new ProfileData(playerName, false, 1, 0);
        }

        playerData.score += score;
        playerData.currentLevel += 1;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProfileData));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            xmlSerializer.Serialize(stream, playerData);
        }
    }

    private void LoadNextLevel()
    {
        string profilesFolder = Path.Combine(Application.persistentDataPath, "Profiles");
        string filePath = Path.Combine(profilesFolder, playerName.Replace(" ", "_"));

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProfileData));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                ProfileData playerData = (ProfileData)serializer.Deserialize(stream);
                string nextLevelSceneName = $"Level{playerData.currentLevel}";
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelSceneName);
            }
        }
    }
}
