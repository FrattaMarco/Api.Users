using Users.Application.Responses;

namespace Users.Application.Models
{
    public class UserModel
    {
        public int IdUtente { get; set; }
        public string Email { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public string CodiceFiscale { get; set; } = null!;
        public string Indirizzo { get; set; } = null!;
        public DateTime DataNascita { get; set; }
        public byte[] Password { get; set; } = null!;

        public static implicit operator UserResponseModel?(UserModel? userResponseModel)
        {
            return new UserResponseModel
            {
                IdUtente = userResponseModel!.IdUtente,
                Email = userResponseModel.Email,
                Nome = userResponseModel.Nome,
                Cognome = userResponseModel.Cognome,
                CodiceFiscale = userResponseModel.CodiceFiscale,
                Indirizzo = userResponseModel.Indirizzo,
                DataNascita = userResponseModel.DataNascita
            } ?? null;
        }
    }
}
