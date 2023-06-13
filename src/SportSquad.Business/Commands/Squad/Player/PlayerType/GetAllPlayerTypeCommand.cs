using SportSquad.Business.Models.PlayerType;
using SportSquad.Core.Command;

namespace SportSquad.Business.Commands.Squad.Player.PlayerType;

public class GetAllPlayerTypeCommand : Command<IEnumerable<PlayerTypeResponse>>
{
}