// Email.cs
using System;

namespace CourseWork.PersonServices
{
    public sealed class Email
    {
        public string Address { get; private set; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Адрес не введён", nameof(address));

            Address = address;
        }
    }
}