extends Node

onready var world = get_node("/root")
var liderboard = {
	22032005: "XXXivanmigunXXX",
	78455: "LIKICH",
	47588: "DIMA",
	14250: "SEMYX",
}
func get_world():
	return world

func add_player_to_board(name):
	var score = Player.get_score()
	liderboard[score] = name
