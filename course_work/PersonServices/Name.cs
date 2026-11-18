// Name.cs
using System;

namespace CourseWork.PersonServices
{
    public class Name
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string SurName { get; private set; }

        public Name(string lastName, string firstName, string surName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Фамилия не введена", nameof(lastName));

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Имя не введено", nameof(firstName));

            if (string.IsNullOrWhiteSpace(surName))
                throw new ArgumentException("Отчество не введено", nameof(surName));

            LastName = lastName;
            FirstName = firstName;
            SurName = surName;
        }
    }
}