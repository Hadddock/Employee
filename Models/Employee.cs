using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Employees
{
	public class Employee
	{

		[BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [BsonElement("first_name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Last Name")]
        [BsonElement("last_name")]
		public string LastName { get; set; }
		[DataType(DataType.Date)]
        [DisplayName("Hire Date")]
        [BsonElement("hire_date")]
		public DateTime HireDate { get; set; }
		[DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        [BsonElement("date_of_birth")]
		public DateTime DateOfBirth { get; set; }
        [BsonElement("email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(254, MinimumLength = 3)]
        public string Email {  get; set; }
        [DataType(DataType.PhoneNumber)]
		[BsonElement("phone")]
        [StringLength(50, MinimumLength = 4)]
        public string Phone { get; set; }
		[BsonElement("is_administrator")]
        [DisplayName("Administrator Privileges")]
        public bool IsAdministrator { get; set; }
	}

	

	
}
