extends "res://scripts/gun.gd"

var ammo = 4
@onready var bullet = preload("res://scenes/shotgun_bullet.tscn")
enum Shotgun_States{
	BUTT_HIT,
}

func _process(delta):
	shoot_cooldown -= delta
	match current_state:
		Default_States.IDLE:
			rotating()
			shoot()
			reload()
		Default_States.SHOOT:
			rotating()
			shoot()
		Default_States.RELOAD:
			reload()
			shoot()
		Shotgun_States.BUTT_HIT:
			rotating()
	global_position = PlayerData.get_body().get_position()


func _ready():
	preparation_animation_time = 1.54
	creating()
	sprite.play("prep")
	timer.start(preparation_animation_time)
	await timer.timeout
	set_idle_state()


func shoot():
	if (
			ammo > 0 and 
			current_state != Default_States.SHOOT and
			Input.is_action_pressed("mouse_left_button") and 
			shoot_cooldown <= 0
	):
		current_state = Default_States.SHOOT
		Player.get_body().character_slowdown()
		ammo -= 1
		sprite.play("shoot")
		timer.start(0.27)
		await timer.timeout
		$pos/particles.emitting = true
		$sleeve_particle.emitting = true
		for i in 6:
			var bullets_instance = bullet.instance()
			world.add_child(bullets_instance)
		timer.start(0.27)
		await timer.timeout
		set_idle_state()
		shoot_cooldown = 0.5


func reload():
	if  (
		(Input.is_action_just_pressed("reload") or ammo == 0) 
		and current_state != Default_States.RELOAD
		and current_state != Default_States.SHOOT
	):
		current_state = Default_States.RELOAD
		while (ammo < 6 and 
		Player.get_hp() > 0 and 
		current_state == Default_States.RELOAD
		):
			sprite.play("reload")
			timer.start(0.4)
			await timer.timeout
			if current_state != Default_States.RELOAD:
				return
			ammo += 1
		set_idle_state()


