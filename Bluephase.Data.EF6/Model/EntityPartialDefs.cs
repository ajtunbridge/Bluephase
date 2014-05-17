namespace Bluephase.Data.EF6.Model
{
    public partial class Customer
    {
        public override string ToString()
        {
            return Name;
        }
    }

    public partial class Person
    {
        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}