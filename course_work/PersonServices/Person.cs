// Person.cs
using System;

namespace CourseWork.PersonServices
{
    public class Person
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public Person(Name name, Email email, string phoneNumber)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "ФИО не введено");
            Email = email ?? throw new ArgumentNullException(nameof(email), "Почта не введена");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Номер не введён", nameof(phoneNumber));

            PhoneNumber = phoneNumber;
        }
    }
}