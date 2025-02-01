using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Application.Repositories;
using TeamTrack.Application.Services;
using TeamTrack.Domain.Entity;

namespace TeamTrack.Application.Servicies
{
    public class ProjectTaskService
    {
        private readonly ProjectTaskRepository _projectTaskRepository;
        private readonly ProjectService _projectService;
        private readonly ProjectRepository _projectRepository;
        private readonly UserRepository _userRepository;

        public ProjectTaskService(ProjectTaskRepository projectTaskRepository, ProjectService projectService, ProjectRepository projectRepository, UserRepository userRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _projectService = projectService;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }


        public ProjectTask CreateProjectTask(string projectId, string nome, string descrizione, Priorità prioritàTask, DateTime dataInizioTask, DateTime dataFineTask, Priorità? prioritàTask, Stato? statoTask, int adminId)
        {
            var admin = _userRepository.GetById(adminId);
            if (admin == null || admin.Role != Role.Admin)
            {
                throw new ArgumentException("L'utente che sta creando la task deve essere un Admin.", nameof(adminId));
            }
            Project project = _projectService.GetProject(projectId);
            if (project == null)
            {
                throw new ArgumentException("Nessun progetto trovato");
                return false;
            }
            var projectTask = new ProjectTask(nome, descrizione, prioritàTask, dataInizioTask, dataFineTask, prioritàTask, statoTask, adminId);
            project.Tasks.Add(projectTask);

            _projectTaskRepository.Add(projectTask);
            _projectRepository.Update(project);
            return projectTask;

        }

        public Boolean deleteProjectTask(int projectId, int projectTaskId)
        {
            Project project = _projectService.GetProject(projectId);
            if (project == null)
            {
                throw new ArgumentException("Nessun progetto trovato.");
                return false;
            }
            IEnumerable<ProjectTask> projectTasks = _projectTaskRepository.GetByProjectId(project.Id);
            foreach (ProjectTask task in projectTasks)
            {
                if (task == null)
                {
                    throw new ArgumentException("Nessuna task trovata.");
                    return false;
                }
                _projectTaskRepository.Delete(projectTaskId);
                return true;
            }
        }

        public Boolean addUserToProjectTask(int projectId, int userId)
        {
            Project project = _projectService.GetProject(projectId);
            if (project == null)
            {
                throw new ArgumentException("Nessun progetto trovato.");
                return false;
            }

            IEnumerable<ProjectTask> projectTasks = _projectTaskRepository.GetByProjectId(project.Id);
            foreach (ProjectTask task in projectTasks)
            {
                if (task == null)
                {
                    throw new ArgumentException("Nessuna task trovata.");
                    return false;
                }
                User utente = _userRepository.GetById(userId);
                if (utente == null)
                {
                    throw new ArgumentException("L'utente non esiste", nameof(userId));
                }
                if (!project.Users.Contains(utente))
                {
                    throw new ArgumentException("L'utente non fa parte del progetto e non può essere assegnato alla task.", nameof(userId));
                }
                if (!task.Utenti.Contains(utente))
                {
                    projectTask.Utenti.Add(user);
                    _projectTaskRepository.Update(projectTask);
                    return true;
                }
                return false;
            }
        }
    }