using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Employees
{
	public class Employee
	{

		[BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
		[BsonElement("first_name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
		[BsonElement("last_name")]
		public string LastName { get; set; }
		[DataType(DataType.Date)]
		[BsonElement("hire_date")]
		public DateTime HireDate { get; set; }
		[DataType(DataType.Date)]
		[BsonElement("date_of_birth")]
		public DateTime DateOfBirth { get; set; }
		[BsonElement("email")]
		public string Email {  get; set; }
		[BsonElement("phone")]
		public string Phone { get; set; }
		[BsonElement("is_administrator")]
		public bool IsAdministrator { get; set; }
		[BsonElement("salary")]
		[BsonRepresentation(BsonType.Decimal128)]
		public decimal Salary {  get; set; }	
	}

	

	
}
