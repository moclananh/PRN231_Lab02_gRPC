using System;
using System.Collections.Generic;

#nullable disable

namespace GrpcService.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AId { get; set; }
        public string AName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
