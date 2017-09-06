namespace DomainClasses.MailManagement
{
    /// <summary>
    /// Questa classe è un wrapper per i servizi di invio mail implementati dalle librerie .net.
    /// L'invio di una mail è sincrono ed immediato.
    /// </summary>
    public interface ISendMail
    {
        /// <summary>
        /// Invia una mail sincronamente ed immediatamente.
        /// </summary>
        /// <param name="email">L'email da inviare</param>
        void Send(Email email);
    }
}