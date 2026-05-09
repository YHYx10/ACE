import { Gui } from '../Bridge/Gui';
import { Client } from '../Bridge/Client';

class SoundPlayerImpl {
    constructor() {
        Client.on('guiPlaySound', (name) => {
            this.play(name);
        });
    }

    /**
     * @param {string} name
     * @param {number} volume
     * @param {boolean} loop
     */
    play(name, volume = 1, loop = false) {
        Gui.setData('sounds/play', JSON.stringify({
            name,
            volume,
            loop,
        }));
    }

    /**
     * @param {string} name
     * @param {number} volume
     * @param {boolean} loop
     */
    playLang(name, volume = 1, loop = false) {
        Gui.setData('sounds/playLang', JSON.stringify({
            name,
            lang: mp.storage.data.language,
            volume,
            loop,
        }));
    }

    stop() {
        Gui.setData('sounds/stop');
    }
}

export const SoundPlayer = new SoundPlayerImpl();
