extends KinematicBody2D

var timer := Timer.new()
var damage_timer := Timer.new()
onready var damage_area = $damage_area
onready var fire_particles = $fire_particles
onready var eye_animation = $sprite
var spell_duration = 9
var tick_time = 1
var enemy_inside = []


func _ready():
	timer.one_shot = false
	add_child(timer)
	damage_timer.one_shot = false
	add_child(damage_timer)
	damage_area.connect("body_entered", self, "_on_damage_area_body_entered")
	damage_area.connect("body_exited", self, "_on_damage_area_body_exited")
	global_position = get_global_mouse_position()
	eye_animation.frame = 0
	eye_animation.play("searching")
	
	timer.start(spell_duration)
	yield(timer,"timeout")
	queue_free()


func _process(delta):
	z_index = global_position.y / 2
	eye_animation.position.y = sin(OS.get_ticks_msec()  * 0.003) * 3


func deal_damage(body):
	while enemy_inside.has(body):
		EventBus.emit_signal("damage_to_enemy", body, 2, null)
		damage_timer.start(tick_time)
		yield(damage_timer,"timeout")


func _on_damage_area_body_entered(body):
	enemy_inside.append(body)
	deal_damage(body)


func _on_damage_area_body_exited(body):
	enemy_inside.erase(body)
	EventBus.emit_signal("damage_to_enemy", body, 0, "burn")
	

