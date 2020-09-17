using System.Collections.Generic;

namespace Bodoconsult.Core.ExpressionBuilder.Test.Models
{
    public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public PersonGender Gender { get; set; }
		public BirthData Birth { get; set; }
		public List<Contact> Contacts { get; private set; }
        public Company Employer { get; set; }

        public double Money { get; set; }

        public bool Checker { get; set; }
		
		public Person()
		{
			Contacts = new List<Contact>();
            Birth = new BirthData();
		}

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().ToString().GetHashCode();
                hash = (hash * 16777619) ^ Name.GetHashCode();
                hash = (hash * 16777619) ^ Gender.GetHashCode();

                if (Birth.Date != null)
                {
                    hash = (hash * 16777619) ^ Birth.Date.GetHashCode();
                }

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }


	}
}