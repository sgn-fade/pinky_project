extends KinematicBody2D

var timer := Timer.new()
onready var damage_area = $damage_area
onready var fire_particles = $fire_particles
onready var eye_animation = $sprite
var spell_duration = 9
#в раудиусе находит противников и поджигает

func _ready():
	timer.one_shot = false
	add_child(timer)
	damage_area.connect("body_entered", self, "_on_damage_area_body_entered")
	global_position = get_global_mouse_position()
	eye_animation.frame = 0
	eye_animation.play("searching")
	
	timer.start(spell_duration)
	yield(timer,"timeout")
	queue_free()
	
	
func _process(delta):
	z_index = global_position.y / 2
	eye_animation.position.y = sin(OS.get_ticks_msec()  * 0.003) * 3


func _on_damage_area_body_entered(body):
	EventBus.emit_signal("damage_to_enemy", body, 2, "burn")
