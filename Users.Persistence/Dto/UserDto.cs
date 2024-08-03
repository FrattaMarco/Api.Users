namespace Users.Persistence.Dto
{
    public record UserDto
    {
        public int IdUtente { get; init; }
        public string Email { get; init; } = null!;
        public string Nome { get; init; } = null!;
        public string Cognome { get; init; } = null!;
        public string CodiceFiscale { get; init; } = null!;
        public string Indirizzo { get; init; } = null!;
        public DateTime DataNascita { get; init; }
        public byte[] Password { get; init; } = null!;
    }
}
