using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Application.Repositories;
using TeamTrack.Application.Services;
using TeamTrack.Domain.Entity;

namespace TeamTrack.Application.Servicies
{
    public class UserService
    {
        private readonly UserRepository _userRepository;



        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
  
        }


        public User Registrazione(string email, string password, Ruolo ruolo)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var utente = new User(email, passwordHash, ruolo);
            return _userRepository.Add(utente);
        
        }

        public bool Autenticazione(string email, string password)
        {
            var utente = this.GetUser(email);
            if (utente == null || !BCrypt.Net.BCrypt.Verify(password, utente.Password))
            {
                return false;
            }
            return true;
        }



        public User GetUser (int id)
        {
            return _userRepository.GetById(id);
        }


        public User GetUser(string email)
        {
            return _userRepository.GetByEmail(email);
        }

    }
}
