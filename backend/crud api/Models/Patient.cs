namespace crud_api.Models
{
    public class Patient
    {
       

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; }=string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string Sex { get; set; } = string.Empty;

        public DateTime dob { get; set; } 
    

    }

    public class GetPatient
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 20;
        public string? firstName { get; set; } = null;
        public string? lastName { get; set; } = null;
        public string? gender { get; set; } = null;


        public DateTime? dateOfBirth { get; set; }
        public string? orderBy { get; set; } = "patient_id";

        public string? sortOrder { get; set; } = "asc";

        public int? patientId { get; set; } = null;

    }
}

/*public class getPatient
{
    public int PatientId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;



    public string Sex { get; set; } = string.Empty;

    public string dob { get; set; } = string.Empty;
    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 20;
    public string orderBy { get; set; } = "patient_id";

}
}*/