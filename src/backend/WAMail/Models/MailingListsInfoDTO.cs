namespace WAMail.Models
{
    public class MailingListsInfoDTO
    {
        public MailingListsInfoDTO(string id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public string Id { get; private set; }
        public string Nome { get; private set; }
    }
}