namespace GeneralKnowledgeBot.Models
{
    public class Answer
    {
        public string[] questions { get; set; }
        public string answer { get; set; }
        public double score { get; set; }
        public string source { get; set; }
        public Context context { get; set; }
    }
}
