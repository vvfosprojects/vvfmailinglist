export class MailingListsInfo {
    constructor(
        public id: string,
        public nome: string) {}
}

export class SendMail {
    public idListeDestinatarie: string[];
    public Oggetto: string;
    public Corpo: string; 
}