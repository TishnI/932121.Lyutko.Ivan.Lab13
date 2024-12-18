using Microsoft.AspNetCore.Mvc;
using Backend3.Models;


namespace Backend3.Controllers
{
    public class MockupsController : Controller
    {
        private QuizModel _quiz;
        private UserQuestionResponse _questionResponse;

        public MockupsController(QuizModel model, UserQuestionResponse questionResponse)
        {
            _quiz = model;
            _questionResponse = questionResponse;
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            int questionIndex = _quiz.CurrentQuestionIndex;
            ViewBag.Question = _quiz.Questions.ElementAt(questionIndex).Key;
            
            return View();
        }

        [HttpPost]
        public IActionResult Quiz(UserQuestionResponse userResponse, string action)
        {
            if(!ModelState.IsValid)
            {
                userResponse.Response = "Ответа нет";
            }

            _quiz.UserResponse.Add(userResponse);
            int questionNumber = ++_quiz.CurrentQuestionIndex;

            if (action == "Next" && questionNumber < _quiz.Questions.Count)
            {
                ViewBag.Question = _quiz.Questions.ElementAt(questionNumber).Key;
            }
            else
            {
                for(int i = questionNumber; i < _quiz.Questions.Count; i++)
                {
                    UserQuestionResponse newUserResponse = new UserQuestionResponse();
                    newUserResponse.Response = "Ответа нет";
                    _quiz.UserResponse.Add(newUserResponse);
                }
                CalculateResult(_quiz.UserResponse);
                
                _quiz.Reset();
                return View();
            }

            return View();
        }

        public IActionResult ShowPreviousResult()
        {
            if(_quiz.PreviousUserResponse.Count == 0)
            {
                int questionIndex = _quiz.CurrentQuestionIndex;
                ViewBag.Question = _quiz.Questions.ElementAt(questionIndex).Key;

                return View("Quiz");
            }
            CalculateResult(_quiz.PreviousUserResponse);
            return View("Quiz");
        }

        private void CalculateResult(List<UserQuestionResponse> userAnswer)
        {
            int correctAnswersCount = 0;
            string[] results = new string[_quiz.Questions.Count];

            for (int i = 0; i < _quiz.Questions.Count; i++)
            {
                var question = _quiz.Questions.ElementAt(i).Key;
                var answer = _quiz.Questions.ElementAt(i).Value;

                if (userAnswer[i].Response == answer)
                    correctAnswersCount++;

                results[i] = $"{i+1}. {question} = {userAnswer[i].Response}";
            }

            ViewBag.QuestionCount = _quiz.Questions.Count;
            ViewBag.CorrectAnswersCount = correctAnswersCount;
            ViewBag.Results = results;

            return;
        }

    }
}
