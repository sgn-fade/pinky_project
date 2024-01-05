extends StaticBody2D
var player_near
@onready var ui = get_node("/root/World/Ui")
@onready var world = get_node("/root")
var room_center
var room_height
var room_width
var current_altar


func _ready():
	EventBus.connect("spheres_donated", Callable(self, "_on_spheres_donated"))


func _on_area_area_exited(area):
	if area.name == "player_area":
		player_near = false
		ui.altar_ui.visible = false


func _on_area_area_entered(area):
	if area.name == "player_area":
		player_near = true


func _input(event):
	if event.is_action_pressed("pick_up"):
		if !ui.altar_ui.visible and player_near:
			ui.altar_ui.visible = true
			current_altar = self
			Player.set_inventory_state()
		elif player_near:
			Player.set_idle_state()
			ui.altar_ui.visible = false


func set_room_connection(center, height, width):
	self.room_center = center
	self.room_height = height
	self.room_width = width

func _on_spheres_donated(time):
	if current_altar == self:
		EventBus.emit_signal("survive_event_started", Vector2(room_width, room_height), room_center, time)
