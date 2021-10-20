using System.Collections.Generic;

namespace Application.Dtos
{
    public class SpecificationFileDto
    {
        /// <summary>
        /// Search term for building Report
        /// </summary>
        public string SearchTerm { get; set; }
        /// <summary>
        /// People for sending e-mail with Report
        /// </summary>
        public List<string> Recipients { get; set; }
    }
}
