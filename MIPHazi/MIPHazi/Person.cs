using System;
using System.Collections.Generic;
using System.Text;

namespace MIPHazi
{
    public abstract class Person
    {
        private string name;

        public Person(string name)
        {
            SetName(name);
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            if(name == null || name.Length < 2)
            {
                throw new ArgumentException("Name must be at least 2 characters long.");
            }
            this.name = name;
        }
    }
}
