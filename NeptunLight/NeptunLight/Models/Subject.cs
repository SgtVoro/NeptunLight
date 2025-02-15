﻿using System.Collections.Generic;
using System.Linq;

namespace NeptunLight.Models
{
    public class Subject
    {
        public Subject(string code, string name, int creditCount, int attemptCount, IEnumerable<Course> courses)
        {
            Code = code;
            Name = name;
            CreditCount = creditCount;
            AttemptCount = attemptCount;
            Courses = courses.ToList();
        }

        public string Code { get; }

        public string Name { get; }

        public int CreditCount { get; }

        public int AttemptCount { get; }

        public IReadOnlyCollection<Course> Courses { get; }
    }
}