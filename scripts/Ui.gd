extends CanvasLayer 
onready var game_ui = $game_ui
onready var inventory_ui = $inventory_ui
onready var load_ui = $load_ui
onready var load_bar = load_ui.get_node("TextureProgress")
onready var pause_ui = $pause_ui
onready var altar_ui = $altar_ui
var current_ui = game_ui


func _ready():
	current_ui = game_ui
	game_ui.visible = true


func show_ui():
	current_ui.visible = false
	game_ui.visible = true


func load_ui():
	load_ui.visible = true
	for i in 1:
		load_bar.value = i
		yield(get_tree().create_timer(randf() / 10), "timeout")
	current_ui = load_ui
	Player.set_idle_state()
	show_ui()


func _process(delta):
	if Input.is_action_just_pressed("open_inventory"):
		if current_ui != inventory_ui :
			current_ui.visible = false
			inventory_ui.visible = true
			current_ui = inventory_ui
			get_tree().paused = true
		else:
			current_ui.visible = false
			game_ui.visible = true
			current_ui = game_ui
			get_tree().paused = false


	if Input.is_action_just_pressed("ui_cancel"):
		if current_ui != pause_ui:
			EventBus.emit_signal("crosshair_switch", "ui")
			altar_ui.visible = false
			current_ui.visible = false
			pause_ui.visible = true
			current_ui = pause_ui
		else:
			EventBus.emit_signal("crosshair_switch", "game")
			current_ui.visible = false
			game_ui.visible = true
			current_ui = game_ui
