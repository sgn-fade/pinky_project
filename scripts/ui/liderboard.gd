extends Label

func updateBoard(player_name):
	GlobalWorldInfo.add_player_to_board(player_name)
	
func print_board():
	text = ""
	var liders = {}
	for key in GlobalWorldInfo.liderboard.keys():
		liders[key] = GlobalWorldInfo.liderboard[key]
	var highest_liders_score = -1
	var highest_liders_name
	while liders.size() > 0:
		highest_liders_score = -1
		var max_score = -1
		highest_liders_score = liders.keys().max()
		highest_liders_name = liders[highest_liders_score]

		liders.erase(highest_liders_score)
		text += highest_liders_name + " " + str(highest_liders_score) + "\n"

