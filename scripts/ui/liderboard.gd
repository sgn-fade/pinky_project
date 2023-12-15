extends Label

func updateBoard(player_name):
	GlobalWorldInfo.add_player_to_board(player_name)
	
func print_board():
	text = ""
	var liders_int_keys = []
	for key in GlobalWorldInfo.liderboard.keys():
		liders_int_keys.append(int(key))
	liders_int_keys.sort()
	
	for i in range(liders_int_keys.size()):
		var highest_liders_score = liders_int_keys.pop_at(-1)
		var highest_liders_name = GlobalWorldInfo.liderboard[String(highest_liders_score)]
		text += highest_liders_name + " " + str(highest_liders_score) + "\n"

