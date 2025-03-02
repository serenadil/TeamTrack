
namespace TeamTrack.MVC.Controllers
{
	public class ProgettoModel
	{
        public int Id { get; set; }
        
        [Display(Name = "Codice Accesso")]
        public string CodiceAccesso { get; set; }
        
        [Required(ErrorMessage = "Il nome del progetto è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome del progetto non può superare i 100 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La data di inizio è obbligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Inizio")]
        public DateTime DataInizioProgetto { get; set; }

        public int AdminId { get; set; }

        public string AdminNome { get; set; }

        public ICollection<TaskProgettoModel> Tasks { get; set; }

        public ICollection<UtenteModel> Users { get; set; }


        public ProgettoModel()
		{
            Tasks = new List<TaskProgettoModel>();
            Users = new List<UtenteModek>();
        }
    }
}
