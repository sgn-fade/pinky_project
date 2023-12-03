extends "res://scripts/gun.gd"

onready var bullet = preload("res://scenes/continuum_shotgun_bullet.tscn")
onready var orb = preload("res://scenes/c_shotgun_orb.tscn")

var ammo = 6

enum C_Shotgun_States{
	HAND = 4
}
var shoot_charge = 0
func _process(delta):
	
	shoot_cooldown -= delta
	match current_state:
		Default_States.IDLE:
			rotating()
			shoot(delta)
			reload()
		Default_States.SHOOT:
			rotating()
			shoot(delta)
		Default_States.RELOAD:
			reload()
			shoot(delta)
		C_Shotgun_States.HAND:
			pass
	global_position = Player.get_position()
	global_position.y += 2


func _ready():
	sprite.play("the hand")
	current_state = C_Shotgun_States.HAND
	var shoot_cooldown = 1
	creating()
	set_idle_state()



func shoot(delta):
	if (ammo > 0 and shoot_cooldown <= 0):
		
		if Input.is_action_pressed("mouse_left_button"):
			if shoot_charge > 1.83:
				sprite.play("fullcharged")
			else:
				sprite.play("charging")
			shoot_charge += delta
			current_state = Default_States.SHOOT
			Player.get_body().character_slowdown()
			
		
		elif shoot_charge > 0:
			shoot_cooldown = 1
			sprite.stop()
			sprite.frame = 0
			if shoot_charge < 1:
				shoot_stage_1()
			elif shoot_charge < 1.75:
				shoot_stage_2()
			else:
				shoot_stage_3()


func shoot_stage_1():
	Player.get_body().c_shotgun_recoil()
	sprite.play("shoot_1")
	for i in 6:
			var bullets_instance = bullet.instance()
			world.add_child(bullets_instance)
	shoot_charge = 0
	timer.start(0.583)
	yield(timer, "timeout")
	set_idle_state()


func shoot_stage_2():
	shoot_charge = 0	
	set_idle_state()


func shoot_stage_3():
	sprite.play("shoot_1")
	var orb_instance = orb.instance()
	world.add_child(orb_instance)
	shoot_charge = 0
	timer.start(0.583)
	yield(timer, "timeout")
	set_idle_state()


func reload():
	pass


