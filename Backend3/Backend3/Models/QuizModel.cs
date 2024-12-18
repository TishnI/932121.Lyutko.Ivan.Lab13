namespace Backend3.Models
{
    public class QuizModel
    {
        public OrderedDictionary<string, string> Questions { get; private set; }
        public List<UserQuestionResponse> UserResponse { get; private set; }
        public List<UserQuestionResponse> PreviousUserResponse { get; private set; }

        public int CurrentQuestionIndex = 0;

        public QuizModel()
        {
            Questions = new OrderedDictionary<string, string>()
            {
                {"1 - 6", "-5"},
                {"8 + 6", "14"},
                {"5 - 7", "-2"},
                {"5 - 2", "3"}
            };

            UserResponse = new List<UserQuestionResponse>();
            PreviousUserResponse = new List<UserQuestionResponse>();
        }

        public void Reset()
        {
            PreviousUserResponse.Clear();
            foreach (var item in UserResponse)
            {
                PreviousUserResponse.Add(item);
            }
            UserResponse.Clear();
            CurrentQuestionIndex = 0;
        }
    }
}
