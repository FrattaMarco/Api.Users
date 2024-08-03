﻿namespace Users.Application.Responses
{
    public record UserResponseModel
    {
        public int IdUtente { get; init; }
        public string Email { get; init; } = null!;
        public string Nome { get; init; } = null!;
        public string Cognome { get; init; } = null!;
        public string CodiceFiscale { get; init; } = null!;
        public string Indirizzo { get; init; } = null!;
        public DateTime DataNascita { get; init; }
    }
}