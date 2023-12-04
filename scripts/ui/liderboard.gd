extends Label

func createBoard():
	text = ""
	var liders = {}
	for i in GlobalWorldInfo.liderboard.keys():
		liders[i] = GlobalWorldInfo.liderboard[i]
	var highest_liders_score = -1
	var highest_liders_name
	while liders.size() > 0:
		highest_liders_score = liders.keys().max()
		print(liders[liders.keys().max()])
		highest_liders_name = liders[highest_liders_score]

		liders.erase(highest_liders_score)
		text += highest_liders_name + " " + str(highest_liders_score) + "\n"
