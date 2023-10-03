using System;
using System.Collections.Generic;

#nullable disable

namespace GrpcService.Models
{
    public partial class Book
    {
        public int BId { get; set; }
        public string BName { get; set; }
        public int BVersion { get; set; }
        public int BPages { get; set; }
        public int BPrice { get; set; }
        public int BYear { get; set; }
        public int? AId { get; set; }

        public virtual Author AIdNavigation { get; set; }
    }
}
