extends Node

onready var world = get_node("/root")
var liderboard = {
	22032005:"XXXivanmigunXXX",
	215463: "PRO100_lika",
	12415: "SEMYX",
	7475: "DIMCHICK",
}
func get_world():
	return world
func _ready():
	load_board()
func add_player_to_board(name):
	var score = Player.get_score()
	liderboard[String(score)] = name
	
	save_board()

func save_board():
	var file_path = "res://my_config.json"
	var file = File.new()
	if file.open(file_path, File.WRITE) == OK:
		var json_string = JSON.print(liderboard)
		file.store_line(json_string)
		file.close()
		print("Данные успешно записаны в JSON файл.")
	else:
		print("Не удалось открыть файл для записи.")

func load_board():
	var file_path = "res://my_config.json"
	var file = File.new()
	if file.open(file_path, File.READ) == OK:
		var json_result = JSON.parse(file.get_as_text())
		if json_result.error == OK and json_result.result is Dictionary:
			liderboard = json_result.result
			print("Данные успешно прочитаны с JSON файла.")
		else:
			print("Формат JSON файла не соответствует ожидаемому словарю.")
		file.close()
	else:
		print("Не удалось открыть файл для чтения.")

