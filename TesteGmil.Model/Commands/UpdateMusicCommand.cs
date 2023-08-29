using MediatR;
using System.Text.Json.Serialization;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class UpdateMusicCommand : IRequest<ApiResult<MusicDto>>
    {
        public UpdateMusicCommand()
        {
            
        }
        public UpdateMusicCommand(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }  
    }
}
