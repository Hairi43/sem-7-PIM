using System.Collections.Generic;
using UnityEngine;
namespace Modules.Quiz.Scripts
{
    [CreateAssetMenu(fileName = "QuestionsDB", menuName = "QuestionsDatabase")]
    public class QuizDatabase : ScriptableObject
    {
        [SerializeField]
        public List<Question> questions;
    }
}