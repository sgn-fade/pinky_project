extends Node

#player take damage from any source
signal player_take_damage(player_offcet_dir, damage) 
#enemy's damage value close sprite to show player damage value
signal show_damage_value(damage_label_instance, damage)
#signals to start event with altar  
signal spheres_donated(time)
signal survive_event_started(room_size, room_center, time)
#to emit enemy attract to source
signal pulls_body(body, position)
signal push_away_enemy(body, velocity)
signal crosshair_switch(type)

signal player_cast_spell(animation_time, animation_name)
signal hands_play_animation(animation_time, animation_name)
signal spell_animation_ended()

signal player_body_entered(body)
signal dash_cooldown()
signal player_teleport(position)

signal show_module_stats_on_game_screen(module)
signal hide_module_stats_on_game_screen()

signal add_module_to_inventory(module, new)
signal set_spell_to_button(module, button)
signal set_spell_icon_to_game_ui(module_icon, button)
signal start_spell_cooldown(time, button)
signal damage_to_enemy(body, damage, status)

signal spell_slot_button_pressed(button)
signal remove_spell_icon_from_game_ui(button)
signal update_character_hp_bar_value(hp, max_hp)

