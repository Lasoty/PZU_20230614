namespace PzuCwiczenia.WebApi.ViewModel
{
    /// <summary>
    /// Definicja wypożyczającego
    /// </summary>
    public class CustomerViewModel
    {
        /// <summary>
        /// Identyfikator wypożyczającego
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        public string LastName { get; set; }
    }
}