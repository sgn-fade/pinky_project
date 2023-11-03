extends KinematicBody2D
var timer := Timer.new()
var light_timer := Timer.new()
var partciles_timer := Timer.new()
onready var damage_area = $damage_area
onready var pulls_tg_area = $pulls_tg
onready var light = $light
onready var floor_light = $light2

func _ready():
	damage_area.connect("body_entered", self, "_on_damage_area_body_entered")
	pulls_tg_area.connect("body_entered", self, "_on_pulls_tg_area_body_entered")
	
	timer.one_shot = false
	light_timer.one_shot = false
	partciles_timer.one_shot = false
	add_child(timer)
	add_child(light_timer)
	add_child(partciles_timer)
	emit_start_particles()
	global_position = get_global_mouse_position()
	timer.start(0.5)
	yield(timer,"timeout")
	light_transform()
	$S.visible = true
	$S.frame = 0
	$S.play("cast")
	timer.start(1.5)
	yield(timer,"timeout")
	queue_free()


func _process(delta):
	if $S.frame == 8:
		damage_area.monitoring = true
		pulls_tg_area.monitoring = false

func _on_pulls_tg_area_body_entered(body):
	EventBus.emit_signal("pulls_body", body, global_position)


func emit_start_particles():
	partciles_timer.start(0.2)
	yield(partciles_timer, "timeout")
	$start_particles1.emitting = true
	$start_particles2.emitting = true
	$start_particles3.emitting = true

func light_transform():
	for i in 5:
		floor_light.energy += 0.2
		floor_light.scale.x += 0.15
		floor_light.scale.y += 0.1
		light.energy += 0.4
		light.scale.x += 0.01
		light_timer.start(0.12)
		yield(light_timer, "timeout")
	for i in 5:
		floor_light.energy -= 0.2
		floor_light.scale.x -= 0.15
		floor_light.scale.y -= 0.1
		light.energy -= 0.4
		light.scale.x += 0.2
		light_timer.start(0.1)
		yield(light_timer, "timeout")


func _on_damage_area_body_entered(body):
	EventBus.emit_signal("damage_to_enemy", body, 8, "burn")
