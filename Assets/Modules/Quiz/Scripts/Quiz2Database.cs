using System.Collections.Generic;
using UnityEngine;
namespace Modules.Quiz.Scripts
{
    [CreateAssetMenu(fileName = "2QuestionsDB", menuName = "QuestionsDatabase")]
    public class QuizDatabase2 : ScriptableObject
    {
        [SerializeField]
        public List<Question> questions;
    }
}