﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAdress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;

        protected Address() { }
        private Address(string firstName, string lastName,string emailAddress,
            string addressLine,string country,string state,string zipCode)
        {
            FirstName=firstName;
            LastName=lastName;
            EmailAdress = emailAddress;
            AddressLine=addressLine;
            Country=country;
            State=state;
            ZipCode=zipCode;

        }
    }

}
