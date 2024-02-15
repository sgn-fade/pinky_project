
extends CharacterBody2D
var acceleration = 20
var dash_cooldown = 0

#stats
var speed = 20
var max_speed = 80
var magic_damage = 1


@onready var _animated_sprite = get_node("aSprite")

var input = Vector2.ZERO

var timer := Timer.new()

var dash_speed_const = 1
var move_position = null
var direction = 1
var current_state = States.IDLE

enum States{
	NONE,
	IDLE,
	MOVE,
	DASH,
	BUTT_HIT_DASH,
	SPELL,
	DEAD,
	INVENTORY
}
func _process(delta):
	$Label.text = str(current_state)
	match current_state:
		States.NONE:
			pass
		States.IDLE:
			play_animation("idle")
			move()
			rotating()
		States.MOVE:
			rotating()
			play_animation("move")
			move()
		States.DASH:
			dash(delta)
			#move_player(delta)
		States.BUTT_HIT_DASH:
			pass
		States.SPELL:
			pass
		States.DEAD:
			pass

func rotating():
	if get_local_mouse_position().x >= 0:
		scale.x = 1
	else:
		direction *= -1
		scale.x = -1


func _ready():
	#stats
	current_state = States.IDLE
	set_hide_state(true)
	EventBus.emit_signal("hands_play_animation",0, "idle")
	#$teleport_ray.add_exception($player_area)
	EventBus.connect("player_take_damage", Callable(self, "_on_player_take_damage"))
	EventBus.connect("player_teleport", Callable(self, "teleport"))
	EventBus.connect("load_game", Callable(self, "load_game"))
	EventBus.connect("player_cast_spell", Callable(self, "set_cast_state"))
	timer.one_shot = false
	add_child(timer)
	Input.set_mouse_mode(1)

func _input(event):
	if Input.is_action_just_pressed("F"):
		GlobalWorldInfo.focus_camera()
	if Player.get_weapon() != null:
		Player.get_weapon().input(event)


func move():
	var dash = get_node("/root/World/Ui/game_ui/dash_indicator")
	if Input.is_action_just_pressed("Shift") && dash.ready:
		disable_collision()
		$dash_particles1.emitting = true
		$dash_particles2.emitting = true
		EventBus.emit_signal("dash_cooldown")
		set_velocity(velocity * 5)
		current_state = States.DASH
		await get_tree().create_timer(0.09).timeout
		current_state = States.IDLE
		set_velocity(velocity / 5)
		character_slowdown()
		
	
	input = Vector2.ZERO
	var animation = "move"
	current_state = States.MOVE
	if Input.is_action_pressed("ui_right"):
		input.x += 1
	if Input.is_action_pressed("ui_left"):
		input.x += -1
	if Input.is_action_pressed("ui_up"):
		input.y += -1
	if Input.is_action_pressed("ui_down"):
		input.y += 1
	
	
	if input.length() == 0:
		speed = 30
		animation = "idle"
		current_state = States.IDLE
	elif speed < max_speed:
		speed += 5
	if speed > max_speed:
		speed = max_speed
	input = input.normalized()
	
	velocity = velocity.lerp(input * speed, acceleration * 0.016)
	set_velocity(velocity)
	move_and_slide()


func set_speed(new_speed):
	max_speed = new_speed


func play_animation(animation):
	_animated_sprite.play(animation)
	#EventBus.emit_signal("hands_play_animation",0, animation)

func move_player(delta):
	var t = 0.05
	velocity = velocity.lerp(move_position, 0.016 / t)
	if velocity.length() >= move_position.length() - 1:
		current_state = States.IDLE
	set_velocity(velocity)
	move_and_slide()
	dash_speed_const += 1


func dash(delta):
	move_and_slide()


func change_state(state):
	current_state = state

func die():
	current_state = States.DEAD
	EventBus.emit_signal("player_dead")


func set_idle_state():
	visible = true
	current_state = States.IDLE

func set_hide_state(state):
	visible = !state
	if state:
		current_state = States.NONE

func load_game():
	set_hide_state(false)
func character_slowdown():
	speed = 0
	while speed < 20:
		speed += 5
		timer.start(0.05)
		await timer.timeout


func disable_collision():
	set_collision_mask_value(2, false)
	$player_area.set_collision_mask_value(2, false)


func enable_collision():
	$player_area.set_collision_mask_value(2, true)
	set_collision_mask_value(2, true)


func _on_player_take_damage(player_offcet_dir, enemy_damage):
	Player.update_hp(-enemy_damage)
	var hp = Player.get_hp() 
	var max_hp = Player.get_max_hp() 
	disable_collision()
	velocity = velocity.lerp(player_offcet_dir * 1000, 0.40)
	set_velocity(velocity)
	move_and_slide()
	if Player.get_hp() <= 0:
		die()

	enable_collision()


func c_shotgun_recoil():
	var player_offcet_dir = (-(get_global_mouse_position() - global_position).normalized())
	for i in 8:
			velocity = velocity.lerp(player_offcet_dir * 20, 0.40)
			set_velocity(velocity)
			move_and_slide()
			timer.start(0.005)
			await timer.timeout

func push_body():
	_animated_sprite.play("idle")
	var player_offcet_dir = (get_global_mouse_position() - global_position).normalized()
	for i in 8:
			velocity = velocity.lerp(player_offcet_dir * 50, 0.40)
			set_velocity(velocity)
			move_and_slide()
			timer.start(0.005)
			await timer.timeout


func set_cast_state(animation_time, animation_name):
	current_state = States.SPELL
	_animated_sprite.play(animation_name)
	timer.start(animation_time)
	await timer.timeout
	current_state = States.IDLE


func teleport(pos):
	current_state = States.SPELL
	$teleport_ray.target_position = pos
	var global_mouse_pos = get_global_mouse_position()
	_animated_sprite.play("teleport_start")
	await _animated_sprite.animation_finished
	
	disable_collision()
	if $teleport_ray.is_colliding():
		global_position = $teleport_ray.get_collision_point() 
		global_position += Vector2(-8 * direction, -4 -4 * pos.sign().y)
	else:
		global_position = global_mouse_pos
	enable_collision()
	_animated_sprite.play("teleport_end")
	await _animated_sprite.animation_finished
	current_state = States.IDLE
	
func set_inventory_state():
	_animated_sprite.play("idle")
	current_state = States.INVENTORY


