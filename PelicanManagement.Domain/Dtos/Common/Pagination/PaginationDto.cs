using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dtos.Common.Pagination
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public string? searchkey { get; set; }
        public FilterType? filterType { get; set; }
    }
}
