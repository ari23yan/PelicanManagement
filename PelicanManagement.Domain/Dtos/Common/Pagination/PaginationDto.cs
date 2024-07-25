using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace PelicanManagement.Domain.Dtos.Common.Pagination
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Searchkey { get; set; }
        [EnumDataType(typeof(FilterType))]
        public FilterType? FilterType { get; set; }  = Enums.FilterType.Desc;
    }
}
