export default {
    namespaced: true,
    state: {
        radioList: [
            {
                link: "https://emgregion.hostingradio.ru:8064/moscow.europaplus.mp3",
                radioName: "Europa Plus"
            },
            {
                link: "https://www.radio.net/s/bigfmberlin",
                radioName: "Big fm"
            },
            {
                link: "https://www.radio.net/s/rockantenne",
                radioName: "Aufzeichnung von Trance Hits"
            },
            {
                link: "https://www.radio.net/s/rockantenne",
                radioName: "radio Netto"
            },
            {
                link: "http://air2.radiorecord.ru:805/mdl_320",
                radioName: "Irish Setter FM"
            },
            {
                link: "http://c6.radioboss.fm:8096/stream",
                radioName: "Capital Fm English trending songs"
            },
            {
                link: "http://chanson.hostingradio.ru:8041/chanson256.mp3",
                radioName: "Radio Chanson"
            },
            {
                link: "http://radio.radioshansonplus.com:8000/radio",
                radioName: "Radio Chanson Plus"
            },
            {
                link: "https://chanson.hostingradio.ru:8041/chanson-romantic256.mp3",
                radioName: "Radio Chanson: Romantic"
            },
            {
                link: "https://listen2.myradio24.com/8795",
                radioName: "ChilloutFM"
            },
            {
                link: "http://nashe1.hostingradio.ru:80/ultra-192.mp3",
                radioName: "Radio ULTRA"
            },
            {
                link: "http://air2.radiorecord.ru:805/sd90_320",
                radioName: "Superdian 90-х"
            },
            {
                link: "http://air2.radiorecord.ru:805/chil_320",
                radioName: "Record Chill-Out"
            },
            {
                link: "http://air2.radiorecord.ru:9003/deep_320",
                radioName: "Record Deep"
            },
            {
                link: "http://strm112.1.fm/atr_mobile_mp3",
                radioName: "1FM Amsterdam Trance Radio"
            },
            {
                link: "https://relay.radiotoolkit.com/rcmdeep",
                radioName: "Radio [RCMD]EEP"
            },
            {
                link: "https://zaycevfm.cdnvideo.ru/ZaycevFM_club_256.mp3",
                radioName: "Zaitsev FM: Club"
            },
            {
                link: "http://dorognoe.hostingradio.ru:8000/radio",
                radioName: "Road radio"
            },
            {
                link: "https://vocaltrance.fm/radio/deep_320",
                radioName: "Deep Vocal House Radio"
            },
            {
                link: "http://185.53.169.117:8000/320",
                radioName: "Lounge Radio atmosphere"
            },
            {
                link: "http://air2.radiorecord.ru:805/rus_320",
                radioName: "Record Russian Mix"
            },
            {
                link: "https://listen.megahit.online/megamp128",
                radioName: "Radio Megahit"
            },
            {
                link: "https://online.kissfm.ua/KissFM_HD",
                radioName: "Kiss FM"
            },
            {
                link: "https://relay.radiotoolkit.com/luxmusic",
                radioName: "LUXURY MUSIC"
            },
            {
                link: "https://zaycevfm.cdnvideo.ru/ZaycevFM_disco_256.mp3",
                radioName: "Zaitsev FM: DISCO"
            },
            {
                link: "https://online.kissfm.ua/KissFM_Deep_HD",
                radioName: "Kiss FM: Deep"
            },
            {
                link: "http://relay.radio.obozrevatel.com:8000/heavymetal128.mp3",
                radioName: "Heavy Metal"
            },
            {
                link: "http://daniel.torontocast.com:1690/stream",
                radioName: "RockMix Radio"
            },
            {
                link: "http://jfm1.hostingradio.ru:14536/rock90.mp3",
                radioName: "Rock FM: 90s"
            },
            {
                link: "http://jfm1.hostingradio.ru:14536/metal.mp3",
                radioName: "Rock FM: Heavy"
            },
            {
                link: "https://radio-stream-0.obozrevatel.com/Alternative_Rock128.mp3",
                radioName: "Alternative rock"
            },
            {
                link: "http://195.78.93.18:8000/rock",
                radioName: "More FM Rock"
            },
            {
                link: "https://sonicpanel.streaming10.net/8054/stream",
                radioName: "Rock Radio"
            },
            {
                link: "http://stream.hardrockfm.ru:8100/hardrockfm-320.mp3",
                radioName: "HardRockFm"
            },
            {
                link: "http://air2.radiorecord.ru:805/rock_320",
                radioName: "Record Rock"
            },
            {
                link: "https://a6.radioheart.ru:9046/RH1972",
                radioName: "Rock'N'Roll FM"
            },
            {
                link: "http://c6.radioboss.fm:8097/stream",
                radioName: "Donat FM:Russian rock"
            },
        ],
        audio: null,
        volume: 80,
        loop: true,
        index: 0,
        isPlaing: false
    },
    mutations: {
        playRadio(state) {
            if (!state.audio)
                state.audio = new Audio();
            state.audio.src = state.radioList[state.index].link;
            state.audio.loop = state.loop;
            state.audio.volume = state.volume / 100;
            state.audio.play();
            state.isPlaing = true;
        },
        stopRadio(state) {
            state.audio.pause();
            state.isPlaing = false;
        },
        nextIndex(state) {
            state.index++;
            if (state.radioList.length <= state.index)
                state.index = 0;
            if (state.audio && state.isPlaing) {
                state.audio.pause();
                state.audio.src = state.radioList[state.index].link;
                state.audio.play();
            }
        },
        prevIndex(state) {
            state.index--;
            if (state.index < 0)
                state.index = state.radioList.length - 1;
            if (state.audio && state.isPlaing) {
                state.audio.pause();
                state.audio.src = state.radioList[state.index].link;
                state.audio.play();
            }
        },
        setVolume(state, { value }) {
            state.volume = value
            if (state.audio)
                state.audio.volume = state.volume / 100;
        }
    },

    actions: {
    }
}