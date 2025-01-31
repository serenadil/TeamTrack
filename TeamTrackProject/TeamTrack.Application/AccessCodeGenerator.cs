
using System;
using System.Linq;
using TeamTrack.Application.Repositories;

namespace TeamTrack.Application.Services
{
    /// <summary>
    /// La classe AccessCodeGenerator si occupa di generare codici di accesso univoci per i progetti.
    /// </summary>
    public class AccessCodeGenerator
    {
        private readonly ProjectRepository _projectRepository;

        /// <summary>
        /// Inizializza una nuova istanza di AccessCodeGenerator con il repository dei progetti.
        /// </summary>
        /// <param name="projectRepository">Il repository dei progetti per verificare l'unicità del codice di accesso.</param>
        public AccessCodeGenerator(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Genera un codice di accesso univoco di 8 caratteri.
        /// </summary>
        /// <returns>Un codice di accesso univoco di 8 caratteri.</returns>
        public string GenerateUniqueAccessCode()
        {
            string codiceAccesso;
            bool isUnique;

            do
            {
                // Genera un codice di accesso casuale di 8 caratteri
                codiceAccesso = GenerateRandomString(8);

                // Verifica che il codice di accesso non esista già nel database
                isUnique = _projectRepository.GetAll().All(p => p.CodiceAccesso != codiceAccesso);

            } while (!isUnique); // Continua a generare finché non è unico

            return codiceAccesso;
        }

        /// <summary>
        /// Genera una stringa casuale di una lunghezza specificata.
        /// </summary>
        /// <param name="length">La lunghezza della stringa da generare.</param>
        /// <returns>Una stringa casuale di lunghezza specificata.</returns>
        private string GenerateRandomString(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] randomString = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomString[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(randomString);
        }
    }
}
