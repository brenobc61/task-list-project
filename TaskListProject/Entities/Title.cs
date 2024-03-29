﻿namespace TaskListProject.Entities
{
    internal class Title
    {
        public string? Content { get; set; }
        public static int TitleMaximumLength { get; set; } = 55;

        public Title()
        {
        }

        public Title(string content)
        {
            Content = content;
        }

        public override string? ToString()
        {
            return Content;
        }
    }
}
