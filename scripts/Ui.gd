extends CanvasLayer 
@onready var game_ui = $game_ui
@onready var inventory_ui = $inventory_ui
@onready var load_ui = $load_ui
@onready var load_bar = load_ui.get_node("TextureProgressBar")
@onready var pause_ui = $pause_ui
@onready var altar_ui = $altar_ui
@onready var menu_ui = $menu_ui
@onready var current_ui = menu_ui

func _ready():
	EventBus.connect("load_game", Callable(self, "load_animation"))

	switch_ui(game_ui, "game")

func load_animation():
	load_ui.visible = true
	for i in 1:
		load_bar.value = i
		await get_tree().create_timer(randf() / 100).timeout
	current_ui = load_ui
	Player.get_body().set_idle_state()
	switch_ui(game_ui, "game")


func _process(delta):
	if !Player.ready():
		return
	if Input.is_action_just_pressed("open_inventory"):
		if current_ui != inventory_ui:
			switch_ui(inventory_ui, "ui")
		else:
			switch_ui(game_ui, "game")


	if Input.is_action_just_pressed("ui_cancel"):
		if current_ui != pause_ui:
			switch_ui(pause_ui, "ui")
		else:
			switch_ui(game_ui, "game")


func switch_ui(ui_type, crosshair_type):
	EventBus.emit_signal("crosshair_switch", crosshair_type)
	current_ui.visible = false
	ui_type.visible = true
	current_ui = ui_type


