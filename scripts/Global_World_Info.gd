extends Node
@onready var world = get_node("/root/World")
@onready var player_camera = load("res://scenes/ui/camera_movement.tscn")
var camera_scene
var enemies = []
var liderboard = {
	22032005:"XXXivanmigunXXX",
	215463: "PRO100_lika",
	12415: "SEMYX",
	7475: "DIMCHICK",
}
func focus_camera():
	camera_scene.set_view(get_closer_enemy())
func get_world():
	return world.get_node("location")
func _ready():
	camera_scene = player_camera.instantiate()
	world.add_child(camera_scene)
	load_board()
func add_player_to_board(name):
	var score = Player.get_score()
	liderboard[str(score)] = name
	
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

func add_enemy(scene): 
	enemies.append(scene)
	
func get_closer_enemy():
	var closer_enemy = null
	var closer_distance = INF
	for enemy in enemies:
		var distance = (Player.get_position() - enemy.global_position).length()
		if distance < closer_distance:
			closer_enemy = enemy
			closer_distance = distance
	return closer_enemy
