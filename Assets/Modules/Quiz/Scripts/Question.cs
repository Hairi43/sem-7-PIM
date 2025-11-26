using System;

namespace Modules.Quiz.Scripts {
    [Serializable]
    public class Question
    {
        
        public string question;
        public string[] answers;
        public char correctAnswer;
    }
}