using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
namespace WebSite.Model
{
    public class QueryParameters
    {
        [BindRequired]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int CompanyId { get; set; }
        [BindRequired]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-mm-dd}")]
        public DateTime StartDate { get; set; }
        [BindRequired]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-mm-dd}")]
        public DateTime EndDate { get; set; }
    }
}
