extends Node
@onready var world = get_node("/root")
var liderboard = {
	22032005:"XXXivanmigunXXX",
	215463: "PRO100_lika",
	12415: "SEMYX",
	7475: "DIMCHICK",
}
func get_world_3d():
	return world
func _ready():
	load_board()
func add_player_to_board(name):
	var score = Player.get_score()
	liderboard[String(score)] = name
	
	save_board()

func save_board():
	var file_path = "res://my_config.json"
	var file = FileAccess.open(file_path, FileAccess.WRITE)
	if file != null:
		var json_string = JSON.stringify(liderboard)
		file.store_line(json_string)
		file.close()
		print("Данные успешно записаны в JSON файл.")
	else:
		print("Не удалось открыть файл для записи.")

func load_board():
	var file_path = "res://my_config.json"
	var file = FileAccess.open(file_path, FileAccess.READ)
	if file != null:
		var test_json_conv = JSON.new()
		test_json_conv.parse(file.get_as_text())
		var json_result = test_json_conv.get_data()
		liderboard = json_result
		file.close()
	else:
		print("Не удалось открыть файл для чтения.")

