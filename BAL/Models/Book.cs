using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Publisher { get; set; }
        public string? Title { get; set; }
        public string? AuthorLastName { get; set; }
        public string? AuthorFirstName { get; set; }
        public decimal Price { get; set; }

        public int YearPublished { get; set; }
        public string? CityPublished { get; set; }

        public string MLACitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. {Title}. {Publisher}, {YearPublished}.";
            }
        }

        public string ChicagoCitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. {Title}. {CityPublished}: {Publisher}, {YearPublished}.";
            }
        }
    }
}
