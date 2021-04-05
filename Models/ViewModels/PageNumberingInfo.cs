using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalReview.Models.ViewModels
{
    // class of Page information
    public class PageNumberingInfo
    {
        public int itemsPerPage { get; set; }
        public int currentPage { get; set; }
        public int totalItems { get; set; }
        public int Pages => (int)(Math.Ceiling((float)totalItems / itemsPerPage));
    }
}
