﻿namespace TodoApp.Models
{
    public class Todoitem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public bool IsCompleted { get; set; }
    }
}
