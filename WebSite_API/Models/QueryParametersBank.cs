﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class QueryParametersBank
    {
        [BindRequired]
        public string Name { get; set; }
    }
}
