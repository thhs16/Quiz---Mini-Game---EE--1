using UnityEngine;
using UnityEngine.UI; // text
using System.Collections;
using System.Collections.Generic;   // to access the list of questions from unity
using System.Linq; // easy access to method like ToList
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    [SerializeField]
    private Animator animator;

    void Start ()
    {
        if( unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        
    }

    void SetCurrentQuestion ()
    {
        // FIX 1: Prevent crash when questions run out
        // if (unansweredQuestions.Count == 0)
        // {
        //     Debug.Log("GAME OVER");
        //     factText.text = "You finished all questions!";
        //     return;
        // }

        int randomQuestionIndex = Random.Range (0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;
    
        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT!";
            falseAnswerText.text = "WRONG";
        } else
        {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT!";
        }
    }

    
    IEnumerator TransitionToNextQuestion ()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void UserSelectTrue ()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
        } else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
        // for IEnumerator method
    }


    public void UserSelectFalse ()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
        } else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
        // for IEnumerator method
    }
}
