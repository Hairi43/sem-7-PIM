using System.Collections.Generic;

namespace Modules.Quiz.Scripts {
    public static class QuizState {
        public static List<int> usedQuestions = new List<int>(); // Stores indexes of used questions
        public static bool questionLoaded = false; // Tracks if question is already loaded
    }
}