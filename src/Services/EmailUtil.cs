using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string GetEmailBodyFromTopics(IEnumerable<TopicDto> topics)
        {
            string result = "<ul>";

            foreach (var topic in topics)
            {
                result += $"<li>Projeto: {topic.Title}";
            }

            result += "</ul>";

            return result;
        }
    }
}
