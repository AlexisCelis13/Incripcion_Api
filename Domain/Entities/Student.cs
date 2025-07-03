namespace Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; }
        public string StudentIdNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public Student(Guid id, string studentIdNumber, string firstName, string lastName, string email)
        {
            Id = id;
            StudentIdNumber = studentIdNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
} 