using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Servizi.Repository;

namespace TeamTrack.Servizi
{
    /// <summary>
    /// La classe AccessCodeGenerator si occupa di generare codici di accesso univoci per i progetti.
    /// </summary>
    public class GeneratoreCodiciAccesso
    {
        private readonly RepositoryProgetto _repositoryProgetto;

        /// <summary>
        /// Inizializza una nuova istanza di AccessCodeGenerator con il repository dei progetti.
        /// </summary>
        /// <param name="projectRepository">Il repository dei progetti per verificare l'unicità del codice di accesso.</param>
        public GeneratoreCodiciAccesso(RepositoryProgetto projectRepository)
        {
            _repositoryProgetto = projectRepository;
        }

        /// <summary>
        /// Genera un codice di accesso univoco di 8 caratteri.
        /// </summary>
        /// <returns>Un codice di accesso univoco di 8 caratteri.</returns>
        public string GeneraCodiceUnico()
        {
            string codiceAccesso;
            bool unico;
            do
            {
                codiceAccesso = this.GeneraStringaRandom(8);
                unico = _repositoryProgetto.GetAll().All(p => p.CodiceAccesso != codiceAccesso);

            } while (!unico);
            return codiceAccesso;
        }

        /// <summary>
        /// Genera una stringa casuale di una lunghezza specificata.
        /// </summary>
        /// <param name="length">La lunghezza della stringa da generare.</param>
        /// <returns>Una stringa casuale di lunghezza specificata.</returns>
        private string GeneraStringaRandom(int length)
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
