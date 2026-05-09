--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
--https://discord.gg/THVKHkgsgw
fx_version 'cerulean'
game 'gta5'
name 'GenScripts Loading Screen'
scriptname 'genscripts_loading'
version '1.0.1'
description 'GENSCRIPTS LOADING SCREEN'

tutorial 'https://genscripts.gitbook.io/gen-scripts/'
lua54 'yes'

client_script 'client.lua'

server_script 'version_check.lua'

loadscreen 'ui/index.html'
loadscreen_cursor 'yes'

files {
    'config.js',
    'ui/*.*',
    'ui/**/*.*',
}

escrow_ignore {
	"client.lua",
    "version_check.lua",
}

dependency '/assetpacks'