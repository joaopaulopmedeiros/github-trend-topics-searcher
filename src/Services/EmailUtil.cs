using Application.Dtos;
using System.Collections.Generic;

namespace Application.Services
{
    public static class EmailUtil
    {
        public static string GetDefaultEmailSubject()
        {
            return "New search delivered";
        }

        public static string GetTermNotFoundMessage(string term)
        {
            return $"No topic with the {term} term was found";
        }

        public static string GetEmailBodyFromTopics(string term, IEnumerable<TopicDto> topics)
        {
            string result = $"<p>Top popular projects on github related to {term}</p>";

            result += "<ul>";

            foreach (var topic in topics)
            {
                result += $"<li>Project: {topic.Title}";
            }

            result += "</ul>";

            return result;
        }
    }
}
