﻿using System.Collections.Generic;
using System.Text.Json;

namespace JobPortal.Api.Filters
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<Errors> Errors { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class Errors
    {
        public string PropertyName { get; set; }

        public string[] ErrorMessages { get; set; }
    }
}