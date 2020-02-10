using System;

namespace NetApi.Models
{
    public class TodoItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isComplete { get; set; }
    }
}