using System;
using System.Collections.Generic;

using TeamTrack.Application.Repositories;

using TeamTrack.Domain.Entity;
namespace TeamTrack.Application.Services
{
    public class ProjectService
    {
        private readonly ProjectRepository _projectRepository;
        private readonly ProjectTaskRepository _projectTaskRepository;
        private readonly UserRepository _userRepository; 
        private readonly AccessCodeGenerator _accessCodeGenerator;

      
        public ProjectService(ProjectRepository projectRepository, UserRepository userRepository, AccessCodeGenerator accessCodeGenerator, ProjectTaskRepository projecttaskRepository)
        {
            _projectRepository = projectRepository;
            _projectTaskRepository = projecttaskRepository;
            _userRepository = userRepository;
            _accessCodeGenerator = accessCodeGenerator;
        }

        /// <summary>
        /// Crea un nuovo progetto verificando i parametri e chiamando il costruttore della classe Project.
        /// </summary>
        /// <param name="name">Il nome del progetto.</param>
        /// <param name="password">La password del progetto.</param>
        /// <param name="dataInizioProgetto">La data di inizio del progetto.</param>
        /// <param name="dataFineProgetto">La data di fine del progetto.</param>
        /// <param name="adminId">L'ID dell'amministratore del progetto.</param>
        /// <returns>Il progetto appena creato.</returns>
        /// <exception cref="ArgumentException">Se uno dei parametri non è valido.</exception>
        public Project CreateProject(string name, string password, DateTime dataInizioProgetto, DateTime dataFineProgetto, int adminId)
        {
            var admin = _userRepository.GetById(adminId);
            if (admin == null || admin.Role != Role.Admin)
            {
                throw new ArgumentException("L'utente che sta creando il progetto deve essere un Admin.", nameof(adminId));
            }
            string codiceAccesso = _accessCodeGenerator.GenerateUniqueAccessCode();
            var project = new Project(name, password, dataInizioProgetto, dataFineProgetto, codiceAccesso, adminId);
            return _projectRepository.Add(project);
        }



        public Boolean deleteProject(int id)
        {
            Project project = this.GetProject(id);
            if (project == null)
            {
                throw new ArgumentException("Nessun progetto trovato");
                return false;
            }
            _projectRepository.Delete(id);
            return true;        
        }


        public Boolean addUserToProject(User user, string codiceAccesso, string password)
        {
            Project project = this.GetProject(accessCode: codiceAccesso);
            if(project == null)
            {
                throw new ArgumentException("Nessun progetto trovato per il codice di accesso");
            }
            if (project.Password == password)
            {
                project.AddUser(user);
                _projectRepository.Update(project);
                return true;
            }return false;
        }


        public Project GetProject(string accessCode)
        {
            return _projectRepository.GetByAccessCode(accessCode);
        }

        public Project GetProject(int id)
        {
            return _projectRepository.GetById(id);
        }



    }
}