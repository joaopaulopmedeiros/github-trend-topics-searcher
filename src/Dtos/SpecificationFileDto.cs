using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class SpecificationFileDto
    {
        /// <summary>
        /// Search terms for building Report
        /// </summary>
        public List<string> SearchTerms { get; set; }
        /// <summary>
        /// People for sending e-mail with Report
        /// </summary>
        public List<string> Recipients { get; set; }
    }
}
